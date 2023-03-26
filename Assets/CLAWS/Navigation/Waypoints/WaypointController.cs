using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] GameObject sign;
    [SerializeField] GameObject player;
    Transform childObject;
    [SerializeField] float minScale = 1f;
    [SerializeField] float distanceAway = 5f;

    private float distance;
    private bool isVisible;
    private float updateDistance;

    void Awake()
    {
        distance = Vector3.Distance(sign.transform.position, player.transform.position);
        isVisible = !(distance > distanceAway);
    }

    // Start is called before the first frame update
    void Start()
    {
        childObject = transform.Find("WaypointSign/Plate");
        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            distance = Vector3.Distance(sign.transform.position, player.transform.position);
            if (distance > distanceAway)
            {
                isVisible = false;
                sign.SetActive(false);
            }
            else if (distance <= distanceAway)
            {
                isVisible = true;
                sign.SetActive(true);
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
