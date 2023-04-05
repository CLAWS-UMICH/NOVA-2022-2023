using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] GameObject sign;
    [SerializeField] float minScale = 1f;
    [SerializeField] float distanceAway = 5f;
    [SerializeField] float coneAngle = 90f;

    float distance;
    bool isVisible;
    float updateDistance;
    Transform childObject;
    GameObject childObject2;
    GameObject player;

    

    void Awake()
    {
        player = GameObject.Find("Main Camera");
        distance = Vector3.Distance(sign.transform.position, player.transform.position);
        isVisible = !(distance > distanceAway);
    }

    // Start is called before the first frame update
    void Start()
    {

        childObject = transform.Find("WaypointSign/Plate");
        childObject2 = GameObject.Find("WaypointSign/Bottom");
        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            distance = Vector3.Distance(sign.transform.position, player.transform.position);

            // Get the direction from the player to the sign object
            Vector3 directionToSign = sign.transform.position - player.transform.position;

            // Get the dot product between the direction to the sign and the player's forward direction
            float dotProduct = Vector3.Dot(directionToSign.normalized, player.transform.forward);

            if (distance <= distanceAway || (dotProduct > Mathf.Cos(coneAngle * Mathf.Deg2Rad * 0.5f)))
            {
                isVisible = true;
                sign.SetActive(true);
                if (distance / 5f < minScale)
                {
                    childObject2.SetActive(true);
                } else
                {
                    childObject2.SetActive(false);
                }
            } else
            {
                isVisible = false;
                sign.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isVisible)
        {
            updateDistance = Vector3.Distance(sign.transform.position, player.transform.position);
            sign.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);
            float scale = updateDistance / 10f;
            scale = Mathf.Max(scale, minScale);
            childObject.localScale = Vector3.one * scale;
        }
    }
}
