using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsStayInMap : MonoBehaviour
{
    Transform MinimapCam;
	float MinimapSize = 16.5f;
	Vector3 TempV3;
    float iconEdgeSize = 0.1f;
	float regularIconSize = 0.3f;

	void Awake() {

		MinimapCam = Camera.main.gameObject.transform;
		// Regular Icon Size
		regularIconSize = transform.localScale.x * 1.25f;
		iconEdgeSize = transform.localScale.x;

		// Center of Minimap
		Vector3 centerPosition = MinimapCam.transform.localPosition;

		// Just to keep a distance between Minimap camera and this Object (So that camera don't clip it out)
		centerPosition.y -= 0.5f;
	}

	void Start() {
		transform.localScale = new Vector3(0f, 0f, 0f);
	}

	void Update () {
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;
	}

	void LateUpdate () {
		

		// Center of Minimap
		Vector3 centerPosition = MinimapCam.transform.localPosition;

		// Just to keep a distance between Minimap camera and this Object (So that camera don't clip it out)
		centerPosition.y -= 2.1f;

		// Distance from the gameObject to Minimap
		float Distance = Vector3.Distance(transform.position, centerPosition);

		// If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
		// But if the Distance is greater than the MinimapSize, then do this
		if (Distance > MinimapSize)
		{
			// Gameobject - Minimap
			Vector3 fromOriginToObject = transform.position - centerPosition;

			// Multiply by MinimapSize and Divide by Distance
			fromOriginToObject *= MinimapSize / Distance;

			// Minimap + above calculation
			transform.position = centerPosition + fromOriginToObject;

             // Shrink size
            transform.localScale = new Vector3(iconEdgeSize, iconEdgeSize, iconEdgeSize);

		} else {
            transform.localScale = new Vector3(regularIconSize, regularIconSize, regularIconSize);
        }
        
	}
}
