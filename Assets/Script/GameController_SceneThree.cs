using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_SceneThree : MonoBehaviour 
{
	private int SceneStage = 0;

	public int GetCurrentStage()
	{
		return SceneStage;
	}

	public void SetCurrentStage(int Stage)
	{
		SceneStage = Stage;
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
}
