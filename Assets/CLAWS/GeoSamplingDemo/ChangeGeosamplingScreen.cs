using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGeosamplingScreen : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu1;
    [SerializeField] private GameObject taskMenu2;
    [SerializeField] private GameObject sampleMenu1;
    [SerializeField] private GameObject sampleMenu2;
    [SerializeField] private GameObject largeSampleMenu;
    [SerializeField] private GameObject cameraMenu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton1()
    {
        taskMenu1.SetActive(true);
        taskMenu2.SetActive(false);
        sampleMenu1.SetActive(true);
        sampleMenu2.SetActive(false);
        largeSampleMenu.SetActive(false);
        cameraMenu.SetActive(false);
    }

    public void ClickButton2()
    {
        taskMenu1.SetActive(false);
        taskMenu2.SetActive(true);
        sampleMenu1.SetActive(false);
        sampleMenu2.SetActive(true);
        largeSampleMenu.SetActive(false);
        cameraMenu.SetActive(false);
    }

    public void ClickButton3()
    {
        taskMenu1.SetActive(false);
        taskMenu2.SetActive(false);
        sampleMenu1.SetActive(true);
        sampleMenu2.SetActive(false);
        largeSampleMenu.SetActive(false);
        cameraMenu.SetActive(false);
    }

    public void ClickButton4()
    {
        taskMenu1.SetActive(false);
        taskMenu2.SetActive(false);
        sampleMenu1.SetActive(false);
        sampleMenu2.SetActive(false);
        largeSampleMenu.SetActive(true);
        cameraMenu.SetActive(false);
    }

    public void ClickButton5()
    {
        taskMenu1.SetActive(false);
        taskMenu2.SetActive(false);
        sampleMenu1.SetActive(false);
        sampleMenu2.SetActive(false);
        largeSampleMenu.SetActive(false);
        cameraMenu.SetActive(true);
    }
}
