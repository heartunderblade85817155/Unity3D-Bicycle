using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoImportant : MonoBehaviour 
{
	public string TheCollectName;
	private bool Select;
	public void GetCollect()
	{

	}

	void Start ()
	{
		
	}
	
	void Update () 
	{
		Color TmpColor = this.GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = this.transform.parent.GetComponent<SpriteRenderer>().material.color.a;
      	this.GetComponent<SpriteRenderer>().material.color = TmpColor;
	}
}
