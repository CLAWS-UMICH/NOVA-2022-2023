using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateWaypoints : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject regPrefab;
    [SerializeField] GameObject geoPrefab;
    [SerializeField] GameObject dangerPrefab;
    [SerializeField] float offset;
    [SerializeField] float yOffset = 0f;
    Vector3 objectPos;
    
    public Waypoint CreateWaypoint(string type, string title)
    {
        // Get the player's position
        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward;
        

        // Calculate the position for the new object (offset by the player's height)
        //Vector3 objectPos = new Vector3(playerPos.x, playerPos.y - offset, playerPos.z + 1f);
        objectPos = playerPos + playerForward * offset;
        objectPos.y = playerPos.y + yOffset;

        // Instantiate the new object at the calculated position
        Transform objectPosTransform;
        GameObject newObject = null;
        TextMeshPro titleTextSign = null;
        TextMeshPro letterTextSign = null;
        switch (type)
        {
            case "danger":
                newObject = Instantiate(dangerPrefab, objectPos, Quaternion.identity);
                break;
            case "geosample":
                newObject = Instantiate(geoPrefab, objectPos, Quaternion.identity);
                titleTextSign = newObject.transform.Find("WaypointSign/Plate/Backplate/IconAndText/Letter").GetComponent<TextMeshPro>();

                break;
            case "regular":
                newObject = Instantiate(regPrefab, objectPos, Quaternion.identity);
                titleTextSign = newObject.transform.Find("WaypointSign/Plate/Backplate/IconAndText/Letter").GetComponent<TextMeshPro>();
        

                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

        objectPosTransform = newObject.transform;

        // Create class oject with the specific type of waypoint that was created
        Waypoint newWaypoint = new Waypoint(objectPosTransform, title, (Type)System.Enum.Parse(typeof(Type), type));

        if (type == "geosample" || type == "regular")
        {
            letterTextSign = newObject.transform.Find("Icons/Letter/LetterText").GetComponent<TextMeshPro>();
            letterTextSign.text = newWaypoint.GetLetter();
            titleTextSign.text = newWaypoint.GetLetter();
        }
        return newWaypoint;
    }


}
