using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseExit : MonoBehaviour 
{
	private GameObject Normal;
	private GameObject Over;
	private GameObject Down;
	private void OnMouseExit()
	{
		Normal.SetActive(true);
        Over.SetActive(false);
        Down.SetActive(false);
	}

	void Start () 
	{
		Normal = this.transform.Find("Normal").gameObject;
        Over = this.transform.Find("Over").gameObject;
        Down = this.transform.Find("Down").gameObject;
	}
	
	void Update () 
	{
		
	}
}
