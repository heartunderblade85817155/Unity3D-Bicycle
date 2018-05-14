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

	public float SoundAppearTime = 0.1f;
	private float SoundAppearTotalTime = 0.0f;
	private bool CanPlayAudio = true;

	private bool GoFinal;

	public GameObject GetBlackBG()
	{
		return BlackGround;
	}

	public void GoNext()
	{
		GoFinal = true;
	}


	public uint GetCurrentStage()
	{
		return CurrentStage;
	}

	public void SetCurrentStage(uint Stage)
	{
		CurrentStage = Stage;
	}

	public void PlayAudio(AudioClip Audio)
	{
		if (!CanPlayAudio)
		{
			return;
		}
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().clip = Audio;
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().loop = false;
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().Play();
		CanPlayAudio = false;
	}

	public void PlayLoopAudio(AudioClip LoopAudio)
	{
		if (!CanPlayAudio)
		{
			return;
		}
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().clip = LoopAudio;
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().loop = true;;
		this.transform.Find("AudioPlayer").GetComponent<AudioSource>().Play();
		CanPlayAudio = false;
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
				else if (FenJings[i].GetComponent<MailController>())
				{
					FenJings[i].GetComponent<MailController>().SetCurrentStage();
				}
				else if (FenJings[i].GetComponent<NTRController>())
				{
					FenJings[i].GetComponent<NTRController>().SetCurrentStage();
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

		BlackGround = GameObject.Find("BlackBG");

		BlackAppearTotalTime = 0.0f;
	}
	
	void Update () 
	{
		if (GoFinal)
        {
            if (BlackAppearTotalTime < BlackAppearTime)
            {
                BlackAppearTotalTime += Time.deltaTime;


                if (!BlackGround.activeInHierarchy)
                {
                    BlackGround.SetActive(true);
                }

                Color TmpColor = BlackGround.GetComponent<SpriteRenderer>().color;
                TmpColor.a = BlackAppearTotalTime / BlackAppearTime;
                BlackGround.GetComponent<SpriteRenderer>().color = TmpColor;
            }
            else
            {
                SceneManager.LoadScene(3);
            }
            return;
        }

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
				BlackAppearTotalTime = 0.0f;
				BlackGround.SetActive(false);
			}
		}

		if (!CanPlayAudio)
		{
			if (SoundAppearTotalTime < SoundAppearTime)
			{
				SoundAppearTotalTime += Time.deltaTime;
			}
			else
			{
				CanPlayAudio = true;
				SoundAppearTotalTime = 0.0f;
			}
		}
	}
}
