using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int RunTimeFlag = -1;

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
        RunTimeFlag = -1;
	}

    public void PlayBgm()
    {
        RunTimeFlag = 0;
        this.GetComponent<AudioSource>().Play();
    }

    public void PlayAudio(AudioClip TheAudio)
    {
        if (TheAudio.Equals(this.transform.Find("PlayAudio").GetComponent<AudioSource>().clip))
        {
            return;
        }
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().clip = TheAudio;
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().loop = false;
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().Play();
    }

    public void PlayLoopAudio(AudioClip TheLoopAudio)
    {
        if (TheLoopAudio.Equals(this.transform.Find("PlayAudio").GetComponent<AudioSource>().clip))
        {
            return;
        }
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().clip = TheLoopAudio;
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().loop = true;
        this.transform.Find("PlayAudio").GetComponent<AudioSource>().Play();
    }
	
	void Update ()
    {
	}
}
