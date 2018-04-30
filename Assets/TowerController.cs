using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Camera MainCamera;

    public float BlendStart = -16.0f;

    public float BlendEnd = -20.0f;

    private Material TowerLod0;

    private Material TowerLod1;

	void Start ()
    {
        TowerLod0 = GameObject.Find("TowerLod0").GetComponent<SpriteRenderer>().material;

        TowerLod1 = GameObject.Find("TowerLod1").GetComponent<SpriteRenderer>().material;
    }
	
	void Update ()
    {
        float CameraZ = MainCamera.transform.position.z;

        float ControlProp = Mathf.Exp(-Mathf.Max((BlendStart - CameraZ) / Mathf.Abs(BlendEnd - BlendStart)));

        Debug.Log(ControlProp);

        TowerLod0.SetFloat("_AlphaBlend", ControlProp);
        TowerLod1.SetFloat("_AlphaBlend", 1 - ControlProp);

	}
}
