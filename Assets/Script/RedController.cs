using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : MonoBehaviour 
{
	private bool ButtonDown;
	private int Stage;

	private GameObject BookLine = null;

	public float Stage0To1 = 1.0f;
	public float Stage0To1Time = 1.5f;
	private float Stage0To1TotalTime = 0.0f;

	public float LittleChange = 1.0f;
	public float LittleChangeTime = 0.3f;
	private float LittleChangeTotalTime = 0.0f;

	private float StartY;

	public float Stage2To3 = -1.4f;
	public float Stage2To3Time = 1.0f;
	private float Stage2To3TotalTime = 0.0f;

	public float Stage4To1 = -1.4f;
	public float Stage4To1Time = 1.0f;
	private float Stage4To1TotalTime = 0.0f;

	private GameObject GreenBookMark;

	void Start () 
	{
		ButtonDown = false;

		Stage = 0;

		StartY = -10000.0f;

		BookLine = this.transform.Find("Red").gameObject;

		GreenBookMark = this.transform.parent.Find("GreenMenu").gameObject;
	}

	public void SetButtonStage(bool flag)
	{
		if (flag)
		{
			Color TmpColor = BookLine.GetComponent<SpriteRenderer>().material.color;
			TmpColor.a = 0.6f;
			BookLine.GetComponent<SpriteRenderer>().color = TmpColor;
		}
		else
		{
			Color TmpColor = BookLine.GetComponent<SpriteRenderer>().material.color;
			TmpColor.a = 1.0f;
			BookLine.GetComponent<SpriteRenderer>().color = TmpColor;
			ButtonDown = true;
		}
	}

	public void ChangeStage(int flag)
	{
		Stage = flag;
	}
	
	void Update () 
	{
		if (Stage == 0 || Stage == 1)
		{
			if (ButtonDown)
			{
				if (StartY.Equals(-10000.0f))
				{
					StartY = this.transform.position.y;
				}

				if (LittleChangeTotalTime < LittleChangeTime)
				{
					LittleChangeTotalTime += Time.deltaTime;
					Vector3 TmpPos = this.transform.position;
					if (Stage == 0)
						this.transform.position = new Vector3(TmpPos.x, StartY + LittleChange * (LittleChangeTotalTime / LittleChangeTime), TmpPos.z);
					else 
						this.transform.position = new Vector3(TmpPos.x, StartY - LittleChange * (LittleChangeTotalTime / LittleChangeTime), TmpPos.z);

					if (LittleChangeTotalTime > LittleChangeTime)
					{
						StartY = this.transform.position.y;
					}
				}
				else if (Stage0To1TotalTime < Stage0To1Time)
				{
					Stage0To1TotalTime += Time.deltaTime;
					Vector3 TmpPos = this.transform.position;
					if (Stage == 0)
						this.transform.position = new Vector3(TmpPos.x, StartY - Stage0To1 * (Stage0To1TotalTime / Stage0To1Time), TmpPos.z);
					else
						this.transform.position = new Vector3(TmpPos.x, StartY + Stage0To1 * (Stage0To1TotalTime / Stage0To1Time), TmpPos.z);
				}
				else
				{
					Stage ^= 1;
					ButtonDown = false;
					StartY = -10000.0f;
					LittleChangeTotalTime = 0.0f;
					Stage0To1TotalTime = 0.0f;
				}
			}
		}

		if (Stage == 2)
		{
			if (StartY.Equals(-10000.0f))
			{
				StartY = this.transform.position.y;
			}

			if (Stage2To3TotalTime < Stage2To3Time)
			{
				Stage2To3TotalTime += Time.deltaTime;
				Vector3 TmpPos = this.transform.position;
				this.transform.position = new Vector3(TmpPos.x, StartY + Stage2To3 * (Stage2To3TotalTime / Stage2To3Time), TmpPos.z);
			}
			else
			{
				Stage = 3;
				StartY = -10000.0f;
				Stage2To3TotalTime = 0.0f;

				GreenBookMark.GetComponent<GreenController>().ChangeStage(1);
			}
		}

		if (Stage == 4)
		{
			if (StartY.Equals(-10000.0f))
			{
				StartY = this.transform.position.y;
			}

			if (Stage4To1TotalTime < Stage4To1Time)
			{
				Stage4To1TotalTime += Time.deltaTime;
				Vector3 TmpPos = this.transform.position;
				this.transform.position = new Vector3(TmpPos.x, StartY + Stage4To1 * (Stage4To1TotalTime / Stage4To1Time), TmpPos.z);
			}
			else
			{
				Stage = 1;
				StartY = -10000.0f;
				Stage4To1TotalTime = 0.0f;
			}
		}

		
	}
}
