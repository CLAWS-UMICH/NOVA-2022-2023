using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompassScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public float numberOfPixelsNorthToNorth;
    public Vector3 NorthDir;
    public Transform Player; // C
    //public GameObject NorthLayer;
    // public GameObject target;
    // Vector3 v3Pos;
    //float rationAngleToPixel;
    [SerializeField] private TextMeshPro compass;
    void Start()
    {
        compass = gameObject.GetComponentInChildren<TextMeshPro>();
        //startPosition = transform.position;
        //rationAngleToPixel = numberOfPixelsNorthToNorth / 360f;
    }

    public void ChangeNorthDir()
    {
        NorthDir.z = Player.eulerAngles.y; //May need to change
        compass.text = NorthDir.z.ToString() + " degrees";
        //NorthLayer.transform.eulerAngles = NorthDir;

    }
    // void AngleBetweenMouseAndPlayer()
    //  {
    //      float fAngle;
 
    //      //Convert the player to Screen coordinates
    //      v3Pos = target.transform.position;
         
    //      fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
    //      if (fAngle < 0.0f) fAngle += 360.0f;
    //      compass.text = (fAngle - 90f).ToString();
    //  }
    void Update()
    {
        ChangeNorthDir();
        //AngleBetweenMouseAndPlayer();
        //Vector3 perp = Vector3.Cross(Vector3.forward, target.transform.forward);
        //float dir = Vector3.Dot(perp, Vector3.up);
        //startPosition  = transform.position + (new Vector3(Vector3.Angle(target.transform.forward, Vector3.forward) * Mathf.Sign(dir) * rationAngleToPixel, 0, 0));
        //compass.text = transform.rotation.ToString();
        //ompass.text = transform.rotation.ToString();
    }
}
