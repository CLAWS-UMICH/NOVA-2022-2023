using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFlash : MonoBehaviour
{
    bool flashing = false;

    SpriteRenderer sr;

    // Colors for flashing
    Color32 onColor = new Color32(255, 255, 0, 85);
    Color32 offColor = new Color32(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        StartCoroutine(FlashCoroutine());
    }

    // Used to disable and enable flashing
    public void SetFlashing(bool f)
    {
        flashing = f;
    }

    IEnumerator FlashCoroutine()
    {
        bool isOn = false;
        while (true)
        {
            if (!flashing)
            {
                sr.color = offColor;
                yield return null;
            }

            else
            {
                isOn = !isOn;
                if (isOn)
                    sr.color = onColor;
                else
                    sr.color = offColor;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
