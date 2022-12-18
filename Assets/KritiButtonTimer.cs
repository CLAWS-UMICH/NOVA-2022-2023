using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KritiButtonTimer : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    public GameObject panel;

    public AudioClip clip;
   
    void Start()
    {
        
    }
    public void startButton(){
        coroutine = UpdateTime();
        StartCoroutine(coroutine);
    }

    public void endButton(){
        StopCoroutine(coroutine);
    }
    

    private IEnumerator UpdateTime(){
        while (true){
            yield return new WaitForSeconds(2);
            panel.SetActive(true);
            Debug.Log("hi");
            gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
