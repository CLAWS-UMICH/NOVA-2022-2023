using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWhenReached : MonoBehaviour
{
    GameObject player;
    private bool played = false;
    private float radius = 2f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Camera");
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (!played && distance <= radius)
        {
            GetComponent<AudioSource>().Play();
            PopUpManager.MakePopup("Destination reached");
            played = true;
        }
    }
}
