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
    // Start is called before the first frame update
    void Start() {
        EnterLunaMode();
    }
        
    IEnumerator LunaMove() {
        for(int i = 0; i < 100; i++) {
            yield return new WaitForSeconds(timeDelay);
            gameObject.transform.rotation = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y, 0);
        }
    }
    public void EnterLunaMode() {
        lunaCoroutine = StartCoroutine(LunaMove());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerCam.transform.position;
    }
}
