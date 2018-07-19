using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	void Start () {

	}
	
	void Update () {
        // Use Left and Right Arrow Keys to move Camera
        //this.transform.LookAt(board);
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, -50.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 50.0f * Time.deltaTime);
        }

	}
}
