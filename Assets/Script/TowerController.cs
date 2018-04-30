using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Camera MainCamera;

    public float Blend1Start = -16.0f;

    public float Blend1End = -20.0f;

    public float Blend2Start = -190.0f;

    public float Blend2End = -200.0f;

    private GameObject TowerLod0;

    private GameObject TowerLod1;

    private GameObject TowerLod2;

    private GameObject Sky;

    private GameObject GameMaster;

    void Start ()
    {
        TowerLod0 = GameObject.Find("TowerLod0");

        TowerLod1 = GameObject.Find("TowerLod1");

        TowerLod2 = GameObject.Find("TowerLod2");

        Sky = GameObject.Find("Sky");

        GameMaster = GameObject.Find("SceneController");
    }
	
	void Update ()
    {
        float CameraZ = MainCamera.transform.position.z;

        float ControlProp;

        int CurrentFlag = GameMaster.GetComponent<GameController>().GetRunTimeFlag();

        if ((CurrentFlag & 1) == 0)
        {
            ControlProp = Mathf.Exp(-Mathf.Max((Blend1Start - CameraZ) / Mathf.Abs(Blend1End - Blend1Start), 0.0f));

            if (ControlProp < 0.1f)
            {
                if (TowerLod0)
                    GameObject.Destroy(TowerLod0);
                return;
            }

            if (TowerLod0)
                TowerLod0.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaBlend", ControlProp);

            if (TowerLod1)
                TowerLod1.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaBlend", 1 - ControlProp);
        }
        else if ((CurrentFlag>>1 & 1) == 0)
        {
            ControlProp = Mathf.Exp(-Mathf.Max((Blend2Start - CameraZ) / Mathf.Abs(Blend2End - Blend2Start), 0.0f));

            if (ControlProp < 0.15f)
            {
                if (TowerLod1)
                {
                    GameObject.Destroy(TowerLod1);
                    GameObject.Destroy(Sky);
                }
                return;
            }

            if (TowerLod1)
                TowerLod1.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaBlend", ControlProp);

            if (TowerLod2)
                TowerLod2.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaBlend", 1 - ControlProp);
        }
        

	}
}
