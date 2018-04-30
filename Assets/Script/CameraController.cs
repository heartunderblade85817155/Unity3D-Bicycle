using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Vector3 CameraSpeed1;

    public Vector3 CameraSpeed2;

    public float FirstStop = -150.0f;

    private bool MoveCamera = false;

    private GameObject ScreenButton;

    private GameObject GameMaster;

    public void SetMoveCamera(bool flag)
    {
        MoveCamera = true;

        ScreenButton.SetActive(false);
    }

    void Start ()
    {
        ScreenButton = GameObject.Find("ScreenButton");

        GameMaster = GameObject.Find("SceneController");
	}
	
	void Update ()
    {
        if (MoveCamera)
        {
            int CurrentFlag = GameMaster.GetComponent<GameController>().GetRunTimeFlag();
            Vector3 CameraCurrentPosition = this.transform.position;

            if ((CurrentFlag & 1) == 0)
            {
                if (CameraCurrentPosition.z <= FirstStop)
                {
                    MoveCamera = false;
                    CurrentFlag |= 1;
                    ScreenButton.SetActive(true);
                    GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                    return;
                }
                this.transform.position = CameraCurrentPosition - CameraSpeed1 * Time.deltaTime;
            }
            else if ((CurrentFlag>>1 & 1) == 0)
            {
                this.transform.position = CameraCurrentPosition - CameraSpeed2 * Time.deltaTime;
            }
        }
	}
}
