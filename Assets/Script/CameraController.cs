using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Vector3 CameraSpeed1;    //初始到标题的摄像机移动速度
    public Vector3 CameraSpeed2;    //标题到SecondStop的摄像机移动速度
    public Vector3 CameraSpeed3;    //从SecondStop到电视机全貌的摄像机移动速度
    public Vector3 CameraSpeed4;    //关闭电视后的移动速度
    public Vector3 CameraSpeed5;

    public float FirstStop = -150.0f;   //出现标题时的停顿
    public float SecondStop = -300.0f;  //为了保证能够显示电视机全貌而进行二阶段camera移动速度的调整
    public float ThirdStop = -1300.0f;  //出现电视机全貌后的停顿
    public float ForthStop = -1450.0f;  //关闭电视后需要的停顿
    public float FifthStop = -3600.0f;  //主角房间出现后的停顿（个人另加上的）

    public float SpeedScale1 = 1.0f;     //控制摄像机的移动倍率
    public float SpeedScale2 = 1.0f;     //控制摄像机的移动倍率
    public float SpeedScale3 = 1.0f;     //控制摄像机的移动倍率
    public float SpeedScale4 = 1.0f;     //控制摄像机的移动倍率
    public float SpeedScale5 = 1.0f;     //控制摄像机的移动倍率

    private bool MoveCamera = false;

    private GameObject GameMaster;

    public float MoveMouseSpeed = 1.0f;
    private Vector3 PreMousePos;
    private bool MoveMouse;
    private bool EventFlag = false;
    private float EventTimeTrigger = 0.0f;
    public float StopTime = 3.0f;           //玩家没有任何操作的持续时间
    private GameObject MoveMouseTitle;

    public AudioClip TVAudioClip;

    void Start ()
    {
        GameMaster = GameObject.Find("SceneController");

        PreMousePos = new Vector3(0.0f, 0.0f, 0.0f);

        MoveMouse = false;

        EventFlag = false;

        EventTimeTrigger = 0.0f;

        MoveMouseTitle = GameObject.Find("MoveMouse");
        MoveMouseTitle.SetActive(false);
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

                    GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                    return;
                }
                this.transform.position = CameraCurrentPosition - CameraSpeed1 * Time.deltaTime * SpeedScale1;
            }
            else if ((CurrentFlag>>1 & 1) == 0)
            {
                if(CameraCurrentPosition.z <= SecondStop)
                {
                    if (CameraCurrentPosition.z <= ThirdStop)
                    {
                        MoveCamera = false;
                        CurrentFlag |= 1<<1;
                        
                        GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                        return;
                    }
                    GameMaster.GetComponent<GameController>().PlayLoopAudio(TVAudioClip);
                    this.transform.position = CameraCurrentPosition - CameraSpeed3 * Time.deltaTime * SpeedScale3;
                }
                else
                {
                    this.transform.position = CameraCurrentPosition - CameraSpeed2 * Time.deltaTime * SpeedScale2;
                }
            }
            else if ((CurrentFlag>>5 & 1) == 0 && (CurrentFlag>>4 & 1) == 1)
            {
                if (CameraCurrentPosition.z <= ForthStop)
                {
                    MoveCamera = false;
                    CurrentFlag |= 1<<5;

                    GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                    return;
                }
                this.transform.position = CameraCurrentPosition - CameraSpeed4 * Time.deltaTime * SpeedScale4;
            }
            else if ((CurrentFlag>>6 & 1) == 0 && (CurrentFlag>>5 & 1) == 1)
            {
                if (!EventFlag)
                {
                    if (EventTimeTrigger <= StopTime)
                    {
                        EventTimeTrigger += Time.deltaTime;
                    }
                    else
                    {
                        MoveMouseTitle.SetActive(true);
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 CurrentMousPos = Input.mousePosition;
                    if (!MoveMouse)
                    {
                        MoveMouse = true;
                        PreMousePos = CurrentMousPos;
                        return ;
                    }
                    else
                    {
                        float DeltaY = this.transform.position.y - (CurrentMousPos - PreMousePos).normalized.y * MoveMouseSpeed;

                        if ((CurrentMousPos - PreMousePos).normalized.y * MoveMouseSpeed >= 20.0f && !EventFlag)
                        {
                            EventFlag = true;
                        }

                        if (DeltaY >= -360.0f)
                        {
                            DeltaY = -360.0f;
                        }

                        if (DeltaY <= -2460.0f)
                        {
                            DeltaY = -2460.0f;
                            CurrentFlag |= 1<<6;

                            GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                        }

                        this.transform.position = new Vector3(this.transform.position.x, DeltaY, this.transform.position.z);

                        PreMousePos = CurrentMousPos;
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    PreMousePos = new Vector3(0.0f, 0.0f, 0.0f);
                    MoveMouse = false;
                }
            }
            else if ((CurrentFlag>>9 & 1) == 0 && (CurrentFlag>>8 & 1) == 1)
            {
                if (CameraCurrentPosition.z >= FifthStop)
                {
                    MoveCamera = false;
                    CurrentFlag |= 1<<9;

                    GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
                }
                this.transform.position = CameraCurrentPosition - CameraSpeed5 * Time.deltaTime * SpeedScale5;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveCamera = true;
                if ( GameMaster.GetComponent<GameController>().GetRunTimeFlag() < 0)
                {
                    GameMaster.GetComponent<GameController>().PlayBgm();
                }
            }
        }
	}
}
