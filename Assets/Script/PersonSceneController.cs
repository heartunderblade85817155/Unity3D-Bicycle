using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSceneController : MonoBehaviour 
{
	private GameObject GameMaster;

	private GameObject SmallSun;
	public float SmallSunAppearTime = 1.0f;
	private float SmallSunAppearTotalTime = 0.0f;

	private bool ThreePicture = false;

	private GameObject WholeRoom;
	public float WholeRoomAppearTime = 1.0f;
	private float WholeRoomAppearTotalTime = 0.0f;

	public Vector3 CameraMove = new Vector3(0.0f, 0.0f, 0.0f);
	public float FinalPosZ = -1800.0f;
	public float CameraMoveScale = 1.0f;

	public float OtherDisAppearTime = 1.0f;
	private float OtherDisAppearTotalTime = 0.0f;
	private GameObject[] LeftScenes = new GameObject[32];
	private GameObject TheTower;

	private Vector3 PreMousePos;
	private bool MoveMouse;
	public float MoveMouseSpeed = 1.0f;
	private bool EventFlag;
	private float TotalX = 0.0f;

    private GameObject BlackGround;
    public float BecomeBlack = 2.0f;
    private float BecomeBlackTotal = 0.0f;

	void Start () 
	{
		GameMaster = GameObject.Find("SceneController");

		SmallSun = GameObject.Find("SmallSun");

		WholeRoom = GameObject.Find("WholeRoom");

		WholeRoomAppearTotalTime = 0.0f;

		OtherDisAppearTotalTime = 0.0f;

		Color Tmp = this.GetComponent<SpriteRenderer>().material.color;
		Tmp.a = 0.0f;
		this.GetComponent<SpriteRenderer>().material.color = Tmp;

		Tmp = SmallSun.GetComponent<SpriteRenderer>().material.color;
		Tmp.a = 0.0f;
		SmallSun.GetComponent<SpriteRenderer>().material.color = Tmp;

		WholeRoom.GetComponent<SpriteRenderer>().material.SetFloat("_SetAlpha", 0.0f);

		LeftScenes = GameObject.FindGameObjectsWithTag("LeftScene");

		TheTower = GameObject.Find("TowerLod2");

        BlackGround = GameObject.Find("black");
        Tmp = BlackGround.GetComponent<SpriteRenderer>().material.color;
        Tmp.a = 0.0f;
        BlackGround.GetComponent<SpriteRenderer>().material.color = Tmp;

    }
	
	void Update () 
	{
		int CurrentFlag = GameMaster.GetComponent<GameController>().GetRunTimeFlag();

		if ((CurrentFlag>>6 & 1) == 0 && (CurrentFlag>>5 & 1) == 1)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Color Tmp = this.GetComponent<SpriteRenderer>().material.color;
				Tmp.a = 1.0f;
				this.GetComponent<SpriteRenderer>().material.color = Tmp;
			}
		}

		if ((CurrentFlag>>7 & 1) == 0 && (CurrentFlag>>6 & 1) == 1)
		{
			if (SmallSunAppearTotalTime <= SmallSunAppearTime)
			{
				Color Tmp = SmallSun.GetComponent<SpriteRenderer>().material.color;
				Tmp.a = SmallSunAppearTotalTime / SmallSunAppearTime;
				SmallSun.GetComponent<SpriteRenderer>().material.color = Tmp;

				SmallSunAppearTotalTime += Time.deltaTime;
			}
			else
			{
				if (!ThreePicture && Input.GetMouseButtonDown(0))
				{
					ThreePicture = true;
				}

				if (ThreePicture)
				{
					if (Camera.main.transform.position.z > FinalPosZ)
					{
						Camera.main.transform.position -= CameraMove * Time.deltaTime * CameraMoveScale;
					}
					else
					{
						if (WholeRoomAppearTotalTime < WholeRoomAppearTime)
						{
							WholeRoom.GetComponent<SpriteRenderer>().material.SetFloat("_SetAlpha", WholeRoomAppearTotalTime / WholeRoomAppearTime);

							WholeRoomAppearTotalTime += Time.deltaTime;
						}
						else
						{
							if (Input.GetMouseButtonDown(0))
							{
								CurrentFlag |= 1<<7;

								GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
								return ;
							}
						}
					}
				}
			}
		}

		if ((CurrentFlag>>8 & 1) == 0 && (CurrentFlag>>7 & 1) == 1)
		{
			if (OtherDisAppearTotalTime < OtherDisAppearTime)
			{
				int Len = LeftScenes.Length;
								
				float NewAlpha = OtherDisAppearTotalTime / OtherDisAppearTime;

				for (int i = 0; i < Len; ++i)
				{
					if (!LeftScenes[i])
					{
						continue;
					}
					Color Tmp = LeftScenes[i].GetComponent<SpriteRenderer>().material.color;
					Tmp.a = 1 - NewAlpha;
					LeftScenes[i].GetComponent<SpriteRenderer>().material.color = Tmp;
				}

				TheTower.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaBlend", 1 - NewAlpha);

				OtherDisAppearTotalTime += Time.deltaTime;
			}
			else
			{
				if (TotalX / 100.0f + 0.4f < 1.0f)
				{
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
                        	float DeltaX = (CurrentMousPos - PreMousePos).normalized.x * MoveMouseSpeed;

                        	if (DeltaX <= -20.0f && !EventFlag)
                        	{
                            	EventFlag = true;
                        	}

							if (DeltaX > 0.0f)
							{
								DeltaX = 0.0f;
							}

                        	WholeRoom.GetComponent<SpriteRenderer>().material.SetFloat("_ShowPercent", Mathf.Min(TotalX / 100.0f + 0.4f, 1.0f));

							TotalX += Mathf.Abs(DeltaX);

                        	PreMousePos = CurrentMousPos;
                    	}
                	}
                	else if (Input.GetMouseButtonUp(0))
                	{
						PreMousePos = new Vector3(0.0f, 0.0f, 0.0f);
                    	MoveMouse = false;
                	}
				}
				else
				{
					CurrentFlag |= 1<<8;

					GameMaster.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
					return ;
				}
			}
		}

        if ((CurrentFlag >> 10 & 1) == 0 && (CurrentFlag >> 9 & 1) == 1)
        {
            if (BecomeBlackTotal < BecomeBlack)
            {
                Color Tmp = BlackGround.GetComponent<SpriteRenderer>().material.color;
                Tmp.a = BecomeBlackTotal / BecomeBlack;
                BlackGround.GetComponent<SpriteRenderer>().material.color = Tmp;
                BecomeBlackTotal += Time.deltaTime;
            }
            else
            {
                Color Tmp = BlackGround.GetComponent<SpriteRenderer>().material.color;
                Tmp.a = 1.0f;
                BlackGround.GetComponent<SpriteRenderer>().material.color = Tmp;
            }
        }

    }
}
