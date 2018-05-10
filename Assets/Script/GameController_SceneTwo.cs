using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_SceneTwo : MonoBehaviour 
{

	private uint CurrentStage;

	private GameObject[] FenJings = new GameObject[8];

	private GameObject BlackGround;

	public float BlackAppearTime = 1.5f;
	private float BlackAppearTotalTime = 0.0f;


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

		BlackGround = GameObject.Find("black");

		BlackAppearTotalTime = 0.0f;
	}
	
	void Update () 
	{
		if (BlackGround.activeInHierarchy)
		{
			if (BlackAppearTotalTime < BlackAppearTime)
			{
				BlackAppearTotalTime += Time.deltaTime;
				Color TmpColor = BlackGround.GetComponent<SpriteRenderer>().color;
				TmpColor.a = 1 - BlackAppearTotalTime / BlackAppearTime;
				BlackGround.GetComponent<SpriteRenderer>().color = TmpColor;
			}
			else
			{
				BlackGround.SetActive(false);
			}
		}
	}
}
