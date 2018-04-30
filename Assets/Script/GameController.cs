using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int RunTimeFlag = 0;

    public int GetRunTimeFlag()
    {
        return RunTimeFlag;
    }

    public void SetRunTimeFlag(int NowValue)
    {
        RunTimeFlag = NowValue;
    }

	void Start ()
    {
        RunTimeFlag = 0;
	}
	
	void Update ()
    {

	}
}
