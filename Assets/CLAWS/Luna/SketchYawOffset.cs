using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SketchYawOffset : MonoBehaviour
{
    Coroutine lunaCoroutine;
    [SerializeField]
    private float timeDelay;
    [SerializeField]
    GameObject playerCam;
    public float offset;

    void Start() {
        EnterLunaMode();
    }
        
    IEnumerator LunaMove() {
        while (true) {
            yield return new WaitForSeconds(timeDelay);
            gameObject.transform.rotation = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y + offset, 0);
        }
    }
    [ContextMenu("Luna Fic")]
    public void EnterLunaMode() {
        lunaCoroutine = StartCoroutine(LunaMove());
    }
    [ContextMenu("Fix Luna")]
    public void StopLunaMode() {
        StopCouroutine(lunaCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerCam.transform.position;
    }
}
