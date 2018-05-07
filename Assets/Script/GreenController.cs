using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenController : MonoBehaviour 
{
	private int Stage = 0;

	public float Stage0To1 = 1.0f;
	public float Stage0To1Time = 1.5f;
	private float Stage0To1TotalTime = 0.0f;
	private float StartY;

	public float Stage2To3 = 1.0f;
	public float Stage2To3Time = 1.5f;
	private float Stage2To3TotalTime = 0.0f;

	private GameObject RedBookMark;

	public void ChangeStage(int flag)
	{
		Stage = flag;
	}

	void Start () 
	{
		Stage = 0;

		StartY = -10000.0f;

		RedBookMark = this.transform.parent.Find("RedMenu").gameObject;
	}
	
	void Update () 
	{
		if (Stage == 1)
		{
			if (StartY.Equals(-10000.0f))
			{
				StartY = this.transform.position.y;
				return;
			}
			if (Stage0To1TotalTime < Stage0To1Time)
			{
				Stage0To1TotalTime += Time.deltaTime;
				Vector3 TmpPos = this.transform.position;
				this.transform.position = new Vector3(TmpPos.x, StartY + Stage0To1 * (Stage0To1TotalTime / Stage0To1Time), TmpPos.z);
			}
			else
			{
				Stage = 2;
				StartY = -10000.0f;
				Stage0To1TotalTime = 0.0f;
			}
		}

		if (Stage == 3)
		{
			if (StartY.Equals(-10000.0f))
			{
				StartY = this.transform.position.y;
				return;
			}
			if (Stage2To3TotalTime < Stage2To3Time)
			{
				Stage2To3TotalTime += Time.deltaTime;
				Vector3 TmpPos = this.transform.position;
				this.transform.position = new Vector3(TmpPos.x, StartY + Stage2To3 * (Stage2To3TotalTime / Stage2To3Time), TmpPos.z);
			}
			else
			{
				Stage = 4;
				StartY = -10000.0f;
				Stage2To3TotalTime = 0.0f;
				RedBookMark.GetComponent<RedController>().ChangeStage(4);
			}
		}
	}
}
