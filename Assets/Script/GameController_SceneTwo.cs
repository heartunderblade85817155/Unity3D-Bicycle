using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_SceneTwo : MonoBehaviour 
{

	private uint CurrentStage;

	public uint GetCurrentStage()
	{
		return CurrentStage;
	}

	public void SetCurrentStage(uint Stage)
	{
		CurrentStage = Stage;
	}

	void Start () 
	{
		CurrentStage = 0;
	}
	
	void Update () 
	{
		
	}
}
