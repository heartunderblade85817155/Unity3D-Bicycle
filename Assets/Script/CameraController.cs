using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;

    public float CameraZSpeed = 0.5f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (MainCamera)
        {
            Vector3 CameraCurrentPosition = MainCamera.transform.position;
            MainCamera.transform.position = new Vector3(CameraCurrentPosition.x, CameraCurrentPosition.y, CameraCurrentPosition.z - CameraZSpeed * Time.deltaTime);
        }
	}
}
