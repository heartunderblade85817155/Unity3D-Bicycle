using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour 
{
	public Vector3 InitPosition = new Vector3(1400.0f, -1400.0f, -1.0f);

	public Vector3 FinialPosition = new Vector3(720.0f, -650.0f, -1.0f);

	public float MoveSpeedScale = 5.0f;

	public float CloseTV = 0.8f;			//关掉电视需要的时间
	private float CloseTotalTime = 0.0f;	//运行时关掉电视耗费的时间
	private Vector3 InitScale;

	private GameObject GameController;

	private bool MoveRemote;

	private GameObject RemoteCircle;

	private GameObject Remote1;

	private GameObject TVWhite;
	private GameObject TVBlack;

	public AudioClip RemoteAudio;

	void Start () 
	{
		MoveRemote = false;

		Remote1 = GameObject.Find("RemoteControl1");

		GameController = GameObject.Find("SceneController");

		RemoteCircle = GameObject.Find("CheckCircle");

		TVWhite = GameObject.Find("WhiteTV");
		TVWhite.SetActive(false);

		TVBlack = GameObject.Find("BlackTV");
		TVBlack.SetActive(false);

		CloseTotalTime = 0.0f;
		InitScale = TVWhite.transform.localScale;

		if (RemoteCircle)
		{
			Color Tmp = RemoteCircle.GetComponent<SpriteRenderer>().material.color;
			Tmp.a = 0.0f;
			RemoteCircle.GetComponent<SpriteRenderer>().material.color = Tmp;
		}
	}
	
	void Update () 
	{
		if(GameController)
		{
			int CurrentFlag = GameController.GetComponent<GameController>().GetRunTimeFlag();
			if((CurrentFlag>>1 & 1) == 1 && (CurrentFlag>>2 & 1) == 0)
			{
				if (Input.GetMouseButtonDown(0) && !MoveRemote)
				{
					MoveRemote = true;
				}
			}

			if (MoveRemote)
			{
				Vector3 MoveDir = (FinialPosition - this.transform.position);

				if (MoveDir.magnitude < 5.0f)
				{	
					this.transform.position = FinialPosition;	
					CurrentFlag |= 1<<2;
					MoveRemote = false;

					GameController.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
				}
				else
				{
					this.transform.position += MoveDir.normalized * MoveSpeedScale * Time.deltaTime;
				}
			}

			if ((CurrentFlag>>2 & 1) == 1 && (CurrentFlag>>3 & 1) == 0)
			{
				if (Input.GetMouseButtonDown(0))
				{
					//遥控器移动完毕，准备点击事件

					Vector3 MouseWorldPos = Input.mousePosition;
					MouseWorldPos.z = Mathf.Abs(Camera.main.transform.position.z + this.transform.position.z);
					MouseWorldPos = Camera.main.ScreenToWorldPoint(MouseWorldPos);
					MouseWorldPos.z = 0.0f;

					RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, new Vector3(0.0f, 0.0f, -1.0f), 10.0f);
            		if(hit)    
            		{
                		if (hit.collider.gameObject.name == "CheckCircle")    
                		{
                    		CurrentFlag |= 1<<3;
							Remote1.SetActive(false);
							TVWhite.SetActive(true);
							TVBlack.SetActive(true);

							GameController.GetComponent<GameController>().PlayAudio(RemoteAudio);

							GameController.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
							return;
                		}
            		}
					else
					{
						Color Tmp = RemoteCircle.GetComponent<SpriteRenderer>().material.color;
						Tmp.a = 1.0f;
						RemoteCircle.GetComponent<SpriteRenderer>().material.color = Tmp;
					}
				}
			}

			if ((CurrentFlag>>3 & 1) == 1 && (CurrentFlag>>4 & 1) == 0)
			{
				if (CloseTotalTime <= CloseTV)
				{
					if (CloseTotalTime >= CloseTV/2.0f)
					{
						Remote1.SetActive(true);
					}

					CloseTotalTime += Time.deltaTime;

					TVWhite.transform.localScale = new Vector3(InitScale.x, InitScale.y * (1 - CloseTotalTime / CloseTV), InitScale.z);
				}
				else
				{
					CurrentFlag |= 1<<4;
					TVWhite.transform.localScale = new Vector3(InitScale.x, 0.0f, InitScale.z);

					GameController.GetComponent<GameController>().SetRunTimeFlag(CurrentFlag);
				}
			}
		}

	}
}
