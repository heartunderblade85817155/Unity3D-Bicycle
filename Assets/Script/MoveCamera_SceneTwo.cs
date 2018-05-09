using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera_SceneTwo : MonoBehaviour 
{

	public Vector4 LimitMove;
	private Vector3 PreMousePos;

	private bool Moved;

	private bool TimeOut;

	private GameObject[] FenJings = new GameObject[8];

	public void SetTimeOut(bool flag)
    {
        TimeOut = flag;
    }

	void Start () 
	{
		Moved = false;

		PreMousePos = Vector3.zero;
	}
	
	void Update ()
	{
		if (TimeOut)
		{
			return;
		}

		FenJings = GameObject.FindGameObjectsWithTag("FenJings");
		if (FenJings.Length > 0)
		{
			return;
		}


		if (Input.GetMouseButton(0))
		{
			if (PreMousePos.Equals(Vector3.zero))
			{
				PreMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				return;
			}

			Vector3 Offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - PreMousePos;

			this.transform.position += Offset;

			float TheX = Mathf.Clamp(this.transform.position.x, LimitMove.x, LimitMove.y);
			float TheY = Mathf.Clamp(this.transform.position.y, LimitMove.z, LimitMove.w);
			this.transform.position = new Vector3(TheX, TheY, this.transform.position.z);

			if (!Offset.Equals(Vector3.zero))
			{
				Moved = true;
			}

			PreMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (!Moved)
			{

			}
			PreMousePos = Vector3.zero;
			Moved = false;
		}

	}
}
