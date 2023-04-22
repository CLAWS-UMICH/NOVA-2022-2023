using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleNotif : MonoBehaviour
{
    public GameObject notifWindow;
    private IEnumerator countDown;

    void Start()
    {
        notifWindow.SetActive(false);
        countDown = DismissNotif();
        EventBus.Subscribe<GeoSampleUpdatedEvent>(notify);
    }

    private void notify(GeoSampleUpdatedEvent e)
    {
        StartCoroutine(countDown);
    }

    IEnumerator DismissNotif()
    {
        notifWindow.SetActive(true);
        yield return new WaitForSeconds(3f);
        notifWindow.SetActive(false);
    }
}
