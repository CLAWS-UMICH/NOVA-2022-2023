using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTest : MonoBehaviour
{
    Transform player;
    public Transform topPart;
    public Transform bottomPart;
    public float scaleMultiplier = 1.0f;
    public float maxScale = 4.0f;
    public float minScale = 0.5f;
    public float bottomPartDistanceThreshold = 2.5f;
    public float topPartDistanceThreshold = 7.5f;
    public float viewAngle = 30.0f;

    private float initialTopPartScale;
    bool topIsOn = true;

    private void Awake()
    {
        player = Camera.main.transform;
    }

    private void Update()
    {
        while (topIsOn)
        {
            // Calculate the distance and direction between the player and the waypoint
            Vector3 playerDirection = player.position - transform.position;
            float distanceToPlayer = playerDirection.magnitude;

            // Scale the top part based on the distance from the player
            float targetScale = Mathf.Lerp(minScale, maxScale, distanceToPlayer * scaleMultiplier);
            topPart.localScale = Vector3.one * targetScale;
        }
    }

    private void Start()
    {
        topIsOn = true;
        initialTopPartScale = topPart.localScale.x;
        StartCoroutine(CheckStuffRepeatedly());
    }

    private IEnumerator CheckStuffRepeatedly()
    {
        while (true)
        {
            yield return StartCoroutine(CheckStuff());
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator CheckStuff()
    {
        // Calculate the distance and direction between the player and the waypoint
        Vector3 playerDirection = player.position - transform.position;
        float distanceToPlayer = playerDirection.magnitude;

        // Face the top part of the waypoint towards the player
        topPart.LookAt(player);

        // Check the distance to hide/show the bottom part
        bool isBottomPartActive = distanceToPlayer > bottomPartDistanceThreshold;
        bottomPart.gameObject.SetActive(!isBottomPartActive);
        

        // Check the distance and view angle to hide/show the top part
        Vector3 directionToSign = transform.position - player.transform.position;
        float dotProduct = Vector3.Dot(directionToSign.normalized, player.transform.forward);
        bool angleOfPlayer = (dotProduct > Mathf.Cos(viewAngle * Mathf.Deg2Rad * 0.5f));
        bool isTopPartActive = distanceToPlayer <= topPartDistanceThreshold || angleOfPlayer;
        topPart.gameObject.SetActive(isTopPartActive);
        topIsOn = isTopPartActive;

        yield return null;
    }
}
