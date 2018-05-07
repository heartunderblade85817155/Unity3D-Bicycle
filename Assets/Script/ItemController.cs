using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour 
{
	private GameObject ItemSign;

	public float ShowTime = 0.5f;
	private float ShowTotalTime = 0.0f;

	private bool ShowSign;

	public void SetShowSign(bool flag)
	{
		ShowSign = flag;

		if (ShowSign)
		{
			ItemSign.SetActive(true);
		}
		else
		{
			Color TmpColor = ItemSign.GetComponent<SpriteRenderer>().material.color;
			TmpColor.a = 0.0f;
			ItemSign.GetComponent<SpriteRenderer>().material.color = TmpColor;
			
			ItemSign.SetActive(false);
		}
	}

	void Start () 
	{		
		ShowSign = false;

		ItemSign = this.transform.Find("ItemBarSign").gameObject;

		ItemSign.SetActive(false);
	}
	
	void Update () 
	{
		if (ItemSign.activeInHierarchy)
		{
			Color TmpColor = ItemSign.GetComponent<SpriteRenderer>().material.color;
			TmpColor.a = Mathf.Sin(Mathf.Repeat(ShowTotalTime, ShowTime) / ShowTime * 3.14f);
			ItemSign.GetComponent<SpriteRenderer>().material.color = TmpColor;

			ShowTotalTime += Time.deltaTime;
		}
		else
		{
			ShowTotalTime = 0.0f;
		}
	}
}
