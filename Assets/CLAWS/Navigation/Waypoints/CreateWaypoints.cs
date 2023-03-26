using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWaypoints : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject waypointPrefab;
    [SerializeField] float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateWaypoint()
    {
        // Get the player's position
        Vector3 playerPos = player.transform.position;

        // Calculate the position for the new object (offset by the player's height)
        Vector3 objectPos = new Vector3(playerPos.x, playerPos.y - offset, playerPos.z + 1f);

        // Instantiate the new object at the calculated position
        Instantiate(waypointPrefab, objectPos, Quaternion.identity);
    }
}
