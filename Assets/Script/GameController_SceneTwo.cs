using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_SceneTwo : MonoBehaviour 
{

	private uint CurrentStage;

	private GameObject[] FenJings = new GameObject[8];


	public uint GetCurrentStage()
	{
		return CurrentStage;
	}

	public void SetCurrentStage(uint Stage)
	{
		CurrentStage = Stage;
	}

	public void ActiveFenJing(string name)
	{
		for (int i = 0; i < FenJings.Length; ++i)
		{
			if (FenJings[i].name.Equals(name))
			{
				FenJings[i].SetActive(true);
				if (FenJings[i].GetComponent<PhotoController>())
				{
					FenJings[i].GetComponent<PhotoController>().SetCurrentStage();
				}
				else if (FenJings[i].GetComponent<FlowerController>())
				{
					FenJings[i].GetComponent<FlowerController>().SetCurrentStage();
				}
				else if (FenJings[i].GetComponent<WindowSwitchController>())
				{
					FenJings[i].GetComponent<WindowSwitchController>().SetCurrentStage();
				}
				break;
			}
		}
	}

	void Start () 
	{
		CurrentStage = 0;

		FenJings = GameObject.FindGameObjectsWithTag("FenJings");

		for (int i = 0; i < FenJings.Length; ++i)
		{
			FenJings[i].SetActive(false);
		}
	}
	
	void Update () 
	{
		
	}
}
