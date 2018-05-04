using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBlackMask : MonoBehaviour 
{
	public float AppearTime = 1.0f;
	private float AppearTotalTime = 0.0f;

	void Start () 
	{
		AppearTotalTime = -1.0f;

		Color tmp = this.GetComponent<SpriteRenderer>().material.color;
		tmp.a = 1.0f;
		this.GetComponent<SpriteRenderer>().material.color = tmp;
	}
	
	void Update () 
	{
		if (AppearTotalTime > AppearTime)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			Color tmp = this.GetComponent<SpriteRenderer>().material.color;
			tmp.a = 1 - AppearTotalTime / AppearTime;
			this.GetComponent<SpriteRenderer>().material.color = tmp;

			AppearTotalTime += Time.deltaTime;
		}
	}
}
