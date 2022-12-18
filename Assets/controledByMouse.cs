using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controledByMouse : MonoBehaviour
{

    bool skipTheFristMovement = false;
    private float x;
    private float y;
    private enableDragging instance = new enableDragging();


    // GameObject g;
    // 451, 252 the middle point
    public void moveObjectByMouse () {
        float moveSpeed = 10;
        //Define the speed at which the object moves.

        Vector3 mousePos = Input.mousePosition;
        // Debug.Log("Click");
//  * moveSpeed * Time.deltaTime
        // button values are 0 for left button, 1 for right button, 2 for the middle button
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
            Debug.Log(Input.GetMouseButton(1));
            

            // Debug.Log("hold");
            // Debug.Log(mousePos);
            // need to turn the mousePos to the pos in game
            // deal with the diff between mouse pos and object pos in the game coordinate
            if (skipTheFristMovement) {
                transform.position = new Vector3((mousePos[0] - x) / 572f, (mousePos[1]-y) / 355f, 0);
                Debug.Log("move");
            }
            else {
                skipTheFristMovement = true;
                x = mousePos[0];
                y = mousePos[1];
                Debug.Log("skip");
            }
        }
        
        //Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log(instance.getMode());
        // if (instance.getMode()) moveObjectByMouse ();
        moveObjectByMouse ();
        
    }


}
