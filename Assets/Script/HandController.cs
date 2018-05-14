using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour 
{
	public float MinX;
	public float MaxX;

	public float WeiTiao;

	private bool CanMove;

	public void SetCanMove(bool flag)
	{
		CanMove = flag;
	}

	void Start () 
	{
		CanMove = true;
	}
	
	void Update () 
	{
		if (CanMove)
		{
			Vector3 LocalMousePos =  this.transform.worldToLocalMatrix * Camera.main.ScreenToWorldPoint(Input.mousePosition);

			LocalMousePos = new Vector3(LocalMousePos.x + WeiTiao, this.transform.localPosition.y, this.transform.localPosition.z);

			if (LocalMousePos.x > MaxX)
			{
				LocalMousePos.x = MaxX;
			}
			if (LocalMousePos.x < MinX)
			{
				LocalMousePos.x = MinX;
			}
			this.transform.localPosition = LocalMousePos;
		}
	}
}
