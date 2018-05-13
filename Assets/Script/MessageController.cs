using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    private GameObject MessageBox;

    private int CurrentStage;

    private GameObject[] MessageClips = new GameObject[20];

    public float ShowMessageTime = 3.0f;
    private float ShowMessageTotalTime;

    public float AppearTime = 0.5f;
    private float AppearTotalTime = 0.0f;

    private void SetAlpha(float alpha)
    {
        int len = MessageClips.Length;
        for (int i = 0; i < len; ++i)
        {
            Color TmpColor = MessageClips[i].GetComponent<SpriteRenderer>().material.color;
            TmpColor.a = alpha;
            MessageClips[i].GetComponent<SpriteRenderer>().material.color = TmpColor;
        }
        Color Tmp = new Vector4(0.0f, 0.0f, 0.0f, alpha);
        MessageBox.GetComponent<TextMeshPro>().faceColor = Tmp;
    }

    public void SetMessage(string Message)
    {
        CurrentStage = 1;
        MessageBox.SetActive(true);
        SetAlpha(0.0f);
        MessageBox.GetComponent<TextMeshPro>().text = Message;
        ShowMessageTotalTime = 0.0f;
		AppearTotalTime = 0.0f;
    }

    void Start()
    {
        CurrentStage = 0;
        MessageBox = GameObject.Find("MessageBox");

        MessageClips = GameObject.FindGameObjectsWithTag("MessageClip");

		SetAlpha(0.0f);
		MessageBox.SetActive(false);
    }

    void Update()
    {
        if (CurrentStage == 1)
        {
            if (AppearTotalTime < AppearTime)
            {
                AppearTotalTime += Time.deltaTime;
                SetAlpha(AppearTotalTime / AppearTime);
            }
            else
            {
                CurrentStage = 2;
                AppearTotalTime = 0.0f;
            }
        }
        else if (CurrentStage == 2)
        {
            if (ShowMessageTotalTime < ShowMessageTime)
            {
                ShowMessageTotalTime += Time.deltaTime;
            }
            else
            {
                CurrentStage = 3;
            }
        }
        else if (CurrentStage == 3)
        {
            if (AppearTotalTime < AppearTime)
            {
                AppearTotalTime += Time.deltaTime;
                SetAlpha(1 - AppearTotalTime / AppearTime);
            }
            else
            {
				SetAlpha(0.0f);
                MessageBox.SetActive(false);
            }
        }

    }
}
