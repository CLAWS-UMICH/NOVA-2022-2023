
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;


public class OrientationHandler : MonoBehaviour
{
    bool originSet = false;

    public GameObject mainCamera;
    public GameObject mainCameraHolder;
    public GameObject mrtkSceneContent;

    void Start()
    {
        //EventBus.Subscribe<TSRegisterSuccessEvent>(OnTSRegisterSuccess);
        EventBus.Subscribe<UpdatedGPSEvent>(OnTSLocation);
    }

    /*
    void OnTSRegisterSuccess(TSRegisterSuccessEvent e)
    {
        user = e.user;

        Debug.LogWarning("Starting to fetch user location");

        StartCoroutine(FetchLocationCoroutine());
    }
    */
    /*
    public void FetchLocation()
    {
        //calibrate
        mrtkSceneContent.transform.position = Vector3.zero;
        mrtkSceneContent.transform.eulerAngles = Vector3.zero;
        TelemetryServerHandler.instance.GetLocationForUser(user.id);
    }

    IEnumerator FetchLocationCoroutine()
    {
        TelemetryServerHandler.instance.GetLocationForUser(user.id);
        yield return new WaitForSeconds(10f);
        
        while (true)
        {
            TelemetryServerHandler.instance.GetLocationForUser(user.id);

            yield return new WaitForSeconds(10f);
        }
        
    }
    */

    void OnTSLocation(UpdatedGPSEvent e)
    {
        Debug.LogWarning("Recieved user location.");

        NewUserLocation(Simulation.User.GPS);

        // To display the angle between coordinates
        // Debug.Log(LocationUtilities.DistanceAndAngleBetweenCoords(new Coords(locations.list[0].latitude, locations.list[0].latitude), new Coords(locations.list[1].latitude, locations.list[1].latitude)));
    }

    void NewUserLocation(GPSMsg location)
    {
        GPSCoords user_coordinates = new GPSCoords(
                        location.lat,
                        location.lon
                    );
        // TODO try getting rid of the if condition
        if (!originSet)
        {
            // set the initial origin
            GPSUtils.ChangeOriginGPSCoords(user_coordinates);

            Debug.Log("Recalculating user location.\n" +
            "Origin is: " + GPSUtils.originGPSCoords.latitude + ' ' + GPSUtils.originGPSCoords.longitude +
            "\nUser location is: " + location.lat + ", " + location.lon);

            Vector3 NewCameraAppPosition = GPSUtils.GPSCoordsToAppPosition(user_coordinates);
            NewCameraAppPosition.y = 0f;

            Debug.Log(NewCameraAppPosition.ToString());

            mainCameraHolder.transform.position = NewCameraAppPosition;
        }

        if (!originSet)
        {
            EventBus.Publish<UpdatedGPSOriginEvent>(new UpdatedGPSOriginEvent());
            originSet = true;
        }
    }

    public void Recalibrate()
    {
        GPSMsg location = Simulation.User.GPS;
        GPSCoords user_coordinates = new GPSCoords(
                        location.lat,
                        location.lon
                    );

        Debug.Log("Recalculating user location.\n" +
            "Origin is: " + GPSUtils.originGPSCoords.latitude + ' ' + GPSUtils.originGPSCoords.longitude +
            "\nUser location is: " + location.lat + ", " + location.lon);

        Vector3 NewCameraAppPosition = GPSUtils.GPSCoordsToAppPosition(user_coordinates);
        NewCameraAppPosition.y = 0f;

        Debug.Log(NewCameraAppPosition.ToString());

        mainCameraHolder.transform.position = NewCameraAppPosition;
    }

    public void SetNorth()
    {
        Debug.LogWarning("Recalculating North.");

        Vector3 mainCameraWorldPosition = mainCamera.transform.position;

        float mainCameraY = mainCamera.transform.localRotation.eulerAngles.y;

        mainCameraHolder.transform.eulerAngles = new Vector3(0f, mainCameraY, 0f);

        Vector3 mainCameraNewWorldPosition = mainCamera.transform.position;

        mainCameraHolder.transform.position = mainCameraHolder.transform.position + -(mainCameraNewWorldPosition - mainCameraWorldPosition);

        // mainCamera.transform.position = mainCameraWorldPosition;


        // mainCamera.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x, 0f, mainCamera.transform.eulerAngles.z);
    }


    // shifts everything around the player North
    public void ShiftNorth(int magnitude)
    {
        Vector3 change = mainCamera.transform.forward * magnitude;
        change.y = 0;
        mainCameraHolder.transform.position -= change;
    }

    public void ShiftSouth(int magnitude)
    {
        Vector3 change = mainCamera.transform.forward * magnitude;
        change.y = 0;
        mainCameraHolder.transform.position += change;
    }

    public void ShiftEast(int magnitude)
    {
        Vector3 change = mainCamera.transform.right * magnitude;
        change.y = 0;
        mainCameraHolder.transform.position += change;
    }

    public void ShiftWest(int magnitude)
    {
        Vector3 change = mainCamera.transform.right * magnitude;
        change.y = 0;
        mainCameraHolder.transform.position -= change;
    }

    public void ShiftRotationClockwise(int magnitude)
    {
        mainCameraHolder.transform.eulerAngles = mainCameraHolder.transform.eulerAngles - new Vector3(0, magnitude, 0);
    }

    public void ShiftRotationCounterClockwise(int magnitude)
    {
        mainCameraHolder.transform.eulerAngles = mainCameraHolder.transform.eulerAngles + new Vector3(0, magnitude, 0);
    }


}
