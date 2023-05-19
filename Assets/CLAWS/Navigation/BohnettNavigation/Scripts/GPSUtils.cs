using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GPSCoords
{
    public double latitude;
    public double longitude;

    public GPSCoords(double lat, double lon)
    {
        latitude = lat;
        longitude = lon;
    }

}

// Assumes the earth is a spheroid with a longer major axis than minor
// Will increase our accuracy of geodesic lengths but perhaps maybe more computationally expensive than last year
public class GPSUtils : MonoBehaviour
{
    static public GPSCoords originGPSCoords; // Origin coordinates of our player

    static double earthMajorAxisLengthInMeters = 6378137.0; // Semi-major axis length of the earth. This was the value used last year when assuming the earth was a sphere
    static double flatteningValue = 1 / 298.257223563; // Value describing the compression of a circle with semi major axis radius to form a ellipse
    static double earthMinorAxisLengthInMeters = (1 - flatteningValue) * earthMajorAxisLengthInMeters; // Semi-minor axis of the earth
    static double iterationDifferenceThreshold = 1e-12; // This will give us accuracy of distance of the geodesic up to +-0.06mm!
    static int iterationCountThreshold = 1000; // Amount of iterations before we abort --> probably wont converge --> will run last year's function in this case


    private void Start()
    {
        
    }
    static public void ChangeOriginGPSCoords(GPSCoords newOrigin)
    {
        originGPSCoords = newOrigin;
    }

    // Vincenty's algorithm implemented taking into account the antipodal point case (lambda > pi)
    // Variable names will follow the below documentation exactly, so for any questions of what they are, refer to those

    // Credit and documentation:
    //  1. https://en.wikipedia.org/wiki/Vincenty%27s_formulae
    //  2. https://www.movable-type.co.uk/scripts/latlong-vincenty.html
    //  3. https://geographiclib.sourceforge.io/geodesic-papers/vincenty75b.pdf

    static public (double, double) GPSCoordsAndAngleBetweenCoords(GPSCoords coords1, GPSCoords coords2)
    {
        // Degrees to Radians constant
        double degToRad = Math.PI / 180;
        // Latitude variables
        double phi1 = coords1.latitude * degToRad;
        double phi2 = coords2.latitude * degToRad;

        // Longitude variables
        double L1 = coords1.longitude * degToRad;
        double L2 = coords2.longitude * degToRad;

        // Difference in longitudinal variables
        double L = L2 - L1;

        // Reduced latitude variables
        double U1 = Math.Atan((1 - flatteningValue) * Math.Tan(phi1)); 
        double U2 = Math.Atan((1 - flatteningValue) * Math.Tan(phi2));


        // Difference of longitude of the points on the auxillary spehere
        // Iteration starts with this being set equal to the difference straight up
        double lambda = L;

        // Variables to remember the values of the previous iterations to compare if subsequent iterations need to occur
        double prevLambda = L;
        double prevSigma = 0;

        // Checks for antipodal ranged points
        bool antipodal = Math.Abs(L) > Math.PI / 2 || Math.Abs(phi2 - phi1) > Math.PI / 2;

        // Variables needed for after the iterations are completed
        double sinAlpha;
        double cosAlphaSquared;
        double cos2SigmaM;
        double sigma = 0;
        double sinSigma;
        double cosSigma;
        // Number of iterations before exiting with error if the algorithm doesn't converge
        int iterations = 0;

        // Helper variables to not repeat calulations of the iterated or final equations
        double cosU1 = Math.Cos(U1);
        double sinU1 = Math.Sin(U1);
        double cosU2 = Math.Cos(U2);
        double sinU2 = Math.Sin(U2);

        do
        {

            double sinLambda = Math.Sin(lambda);
            double cosLambda = Math.Cos(lambda);

            double sinSigmaFirstTerm = Math.Pow(cosU2 * sinLambda, 2f);
            double sinSigmaSecondTerm = Math.Pow((cosU1 * sinU2) -
                                        (sinU1 * cosU2 * cosLambda), 2f);

            sinSigma = Math.Sqrt(sinSigmaFirstTerm + sinSigmaSecondTerm);
            cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda;

            prevSigma = sigma;
            sigma = Math.Atan2(sinSigma, cosSigma);

            sinAlpha = (cosU1 * cosU2 * sinLambda) / sinSigma; // TODO: May divide by zero at some point, handle this case

            cosAlphaSquared = (1 - sinAlpha * sinAlpha);

            cos2SigmaM = (cosAlphaSquared != 0) ? cosSigma - ((2 * sinU1 * sinU2) / cosAlphaSquared) : 0;

            double C = (flatteningValue / 16) * cosAlphaSquared * (4 + flatteningValue * (4 - 3 * cosAlphaSquared));

            prevLambda = lambda;
            lambda = L + (1 - C) * flatteningValue * sinAlpha * (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));




            //double iterationCheck = antipodal ? Math.Abs(lambda) - Math.PI : Math.Abs(lambda);
            if (Math.Abs(lambda) > Math.PI)
            {
                Debug.LogError("Lambda value was greater than PI, running alternate algorithm");
                return DistanceAndAngleBetweenCoords(coords1, coords2);

            }
            
        } while (Math.Abs((lambda - prevLambda)) > iterationDifferenceThreshold && ++iterations < iterationCountThreshold);

        if (iterations >= iterationCountThreshold)
        {
            Debug.LogError("Algorithm failed to converge, running alternate algorithm");
            return DistanceAndAngleBetweenCoords(coords1, coords2);
        }

        double uSquared = cosAlphaSquared * ((earthMajorAxisLengthInMeters * earthMajorAxisLengthInMeters -
                                            earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters) /
                                            (earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters));

        double A = 1 + (uSquared / 16384) * (4096 + uSquared * (-768 + uSquared * (320 - 175 * uSquared)));
        double B = (uSquared / 1024) * (256 + uSquared * (-128 + uSquared * (74 - 47 * uSquared)));
        double deltaSigma = B * sinSigma * (cos2SigmaM + 0.25 * B * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)
                                            - (B / 6) * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));

        double s = earthMinorAxisLengthInMeters * A * (sigma - deltaSigma);
        double alpha1 = Math.Atan2(cosU2 * Math.Sin(lambda), cosU1 * sinU2 - sinU1 * cosU2 * Math.Cos(lambda));
        //double alpha2 = Math.Atan2(cosU1 * Math.Sin(lambda), -1 * sinU1 * cosU2 + cosU1 * sinU2 * Math.Cos(lambda));

        return (s, alpha1 * (1 / degToRad));

    }








    /*

    // Similar to the main Vincenty's equation iteration, this will run if the initial computation of lambda is above pi --> Apparently when the creator of the function said the old iteration would not converge
    // Variable names come from the paper written by Vincenty and is linked above in the 'Credits and Documentation' section       
    static public (double, double) AlternateDistanceAndAngleCalculationForAntipodalPoints(GPSCoords coords1, GPSCoords coords2)
    {
        // ===== Same as the previous iteration function =========

        // Degrees to Radians constant
        double degToRad = Math.PI / 180;
        // Latitude variables
        double phi1 = coords1.latitude * degToRad;
        double phi2 = coords2.latitude * degToRad;

        // Longitude variables
        double L1 = coords1.longitude * degToRad;
        double L2 = coords2.longitude * degToRad;

        // Difference in longitudinal variables
        double L = L2 - L1;

        // Reduced latitude variables
        double U1 = Math.Atan((1 - flatteningValue) * Math.Tan(phi1));
        double U2 = Math.Atan((1 - flatteningValue) * Math.Tan(phi2));

        

        // Helper variables to not repeat calulations of the iterated or final equations
        double cosU1 = Math.Cos(U1);
        double sinU1 = Math.Sin(U1);
        double cosU2 = Math.Cos(U2);
        double sinU2 = Math.Sin(U2);

        // =======================================================


        // New/changed iteration variables as outlined in: https://geographiclib.sourceforge.io/geodesic-papers/vincenty75b.pdf

        double Lprime = (L > 0) ? Math.PI - L : -1 * Math.PI - L;
        double lambdaPrime = 0;
        double cosAlphaSquared = 0.5;
        double cos2SigmaM = 0;
        double sigma = Math.PI * -1 * Math.Abs(U1 + U2);
        double cosSigma = Math.Cos(sigma);
        double sinSigma = Math.Sin(sigma);

        // New variable to check upon iteration
        double sinAlpha = 0;

        // Variable to remember the values of the previous iterations to compare if subsequent iterations need to occur
        double prevSinAlpha = 0;

        // Iteration Count --> Not sure if this is needed for this functino but just in case
        int iterationCount = 0;

        // Special case that doesn't involve iteration
        if (Math.Abs(U1 + U2) == 0)
        {
            double Q = Lprime / (flatteningValue * Math.PI);
            double b1 = 1 + (flatteningValue / 4) + (flatteningValue * flatteningValue / 8);
            double b3 = 1 - b1;

            sinAlpha = b1 * Q + b3 * Q * Q * Q;

            double sinAlphaOne = sinAlpha / cosU1;
            double angle = Math.Asin(sinAlphaOne);

            double episilon = (earthMajorAxisLengthInMeters * earthMajorAxisLengthInMeters -
                               earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters) /
                               (earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters);

            double E = Math.Sqrt(1 + episilon * cosAlphaSquared);
            double F = (E - 1) / (E + 1);
            double A = (1 + 0.25 * F * F) / (1 - F);




            double s = earthMinorAxisLengthInMeters * A * Math.PI;
            return (s, angle);
        }
        else
        {
            do
            {
                double C = (1.0 / 16.0) * flatteningValue * cosAlphaSquared * (4 + flatteningValue * (4 - 3 * cosAlphaSquared));
                cos2SigmaM = cosSigma - (2 * sinU1 * sinU2 / cosAlphaSquared);
                double D = (1 - C) * flatteningValue * (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));


                prevSinAlpha = sinAlpha;
                sinAlpha = (Lprime - lambdaPrime) / D;
                
                cosAlphaSquared = 1 - sinAlpha * sinAlpha;

                double sinLambdaPrime = sinAlpha * sinSigma / (cosU1 * cosU2);
                lambdaPrime = Math.Asin(sinLambdaPrime);
                

                sigma = Math.Asin(Math.Sqrt(Math.Pow(cosU2 * sinLambdaPrime, 2) + Math.Pow(cosU1 * sinU2 + sinU1 * cosU2 * Math.Cos(lambdaPrime), 2)));

                sinSigma = Math.Sin(sigma);
                cosSigma = Math.Cos(sigma);


            } while (Math.Abs(sinAlpha - prevSinAlpha) > iterationDifferenceThreshold && ++iterationCount < iterationCountThreshold);

            if (iterationCount >= iterationCountThreshold)
            {
                Debug.LogError("Algorithm failed to converge");
            }

            double sinAlphaOne = sinAlpha / cosU1;
            double angle = Math.Asin(sinAlphaOne);

            double episilon = (earthMajorAxisLengthInMeters * earthMajorAxisLengthInMeters -
                               earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters) /
                               (earthMinorAxisLengthInMeters * earthMinorAxisLengthInMeters);

            double E = Math.Sqrt(1 + episilon * cosAlphaSquared);
            double F = (E - 1) / (E + 1);
            double A = (1 + 0.25 * F * F) / (1 - F);
            double B = F * (1 - (3.0 / 8.0) * F * F);

            double deltaSigma = B * sinSigma * (cos2SigmaM + 0.25 * B * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) -
                                (1.0 / 6.0) * B * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));

            double s = earthMinorAxisLengthInMeters * A * (sigma - deltaSigma);

            Debug.Log(A + " A value");
            Debug.Log(deltaSigma + " deltaSigma value");
            Debug.Log(sigma + " sigma value");

            return (s, angle);
        }


        


    }



    */






    // Function used last year with the haversine formula, not as accurate but is more computationally efficient
    // Ran when Vincenty's algorithm doesn't converge in 1000 iterations.
    static private (double, double) DistanceAndAngleBetweenCoords(GPSCoords coords1, GPSCoords coords2)
    {
        Debug.Log("Vincenty's failed, running Haversine");

        // Credit to https://www.movable-type.co.uk/scripts/latlong.html

        double theta_1 = coords1.latitude * Math.PI / 180;
        double theta_2 = coords2.latitude * Math.PI / 180;
        double delta_theta = (coords2.latitude - coords1.latitude) * Math.PI / 180;
        double delta_lambda = (coords2.longitude - coords1.longitude) * Math.PI / 180;

        double a = Math.Sin(delta_theta / 2) * Math.Sin(delta_theta / 2) +
                    Math.Cos(theta_1) * Math.Cos(theta_2) *
                    Math.Sin(delta_lambda / 2) * Math.Sin(delta_lambda / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double d = earthMajorAxisLengthInMeters * c;

        double y = Math.Sin(delta_lambda) * Math.Cos(theta_2);
        double x = Math.Cos(theta_1) * Math.Sin(theta_2) -
                    Math.Sin(theta_1) * Math.Cos(theta_2) * Math.Cos(delta_lambda);
        double theta = Math.Atan2(y, x);

        double angle = theta * 180 / Math.PI;

        return (d, angle + 180);
    }



    static public Vector3 GPSCoordsToAppPosition(GPSCoords coords)
    {
        // (double distanceFromOrigin, double angleFromOrigin) = GPSCoordsAndAngleBetweenCoords(coords, originGPSCoords);
        (double distanceFromOrigin, double angleFromOrigin) = DistanceAndAngleBetweenCoords(coords, originGPSCoords);
        double distanceFromOriginX = distanceFromOrigin * Math.Sin(angleFromOrigin * Math.PI / 180);
        double distanceFromOriginZ = distanceFromOrigin * Math.Cos(angleFromOrigin * Math.PI / 180);

        return new Vector3((float)distanceFromOriginX, 0f, (float)distanceFromOriginZ);
    }
}
