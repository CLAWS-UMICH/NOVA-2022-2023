using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWaypoints : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject regPrefab;
    [SerializeField] GameObject geoPrefab;
    [SerializeField] GameObject dangerPrefab;
    [SerializeField] float offset;

    public void CreateWaypoint(string type, string title)
    {
        // Get the player's position
        Vector3 playerPos = player.transform.position;

        // Calculate the position for the new object (offset by the player's height)
        Vector3 objectPos = new Vector3(playerPos.x, playerPos.y - offset, playerPos.z + 1f);

        // Instantiate the new object at the calculated position
        switch (type)
        {
            case "danger":
                Instantiate(dangerPrefab, objectPos, Quaternion.identity);
                break;
            case "geosample":
                Instantiate(geoPrefab, objectPos, Quaternion.identity);
                break;
            case "regular":
                Instantiate(regPrefab, objectPos, Quaternion.identity);
                break;
            default:
                Debug.Log("Unknown waypoint type");
                break;
        }

        // Create class oject with the specific type of waypoint that was created
        Waypoint newWaypoint = new Waypoint(objectPos, title, (Type)System.Enum.Parse(typeof(Type), type));
    }

}
