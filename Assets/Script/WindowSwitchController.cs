﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSwitchController : MonoBehaviour
{
    public float ShowTime = 0.5f;
    private float ShowTotalTime = 0.0f;
    private bool BeginShow;
    private int ShowNum = -1;

    public int TotalFenJings = 5;

    private int ImportStage;

    private int CurrentStage;

    private Collider2D HitCollider;

    private List<GameObject> Photos = new List<GameObject>();

    private GameObject DarkGround;

    public List<AudioClip> FenJingAudio;

    public void SetCurrentStage()
    {
        CurrentStage = 0;

        ShowNum = -1;

        if (Photos.Count == 0)
            Start();

        for (int i = 0; i < Photos.Count; ++i)
        {
            Color TmpColor = Photos[i].GetComponent<SpriteRenderer>().material.color;
            TmpColor.a = 0.0f;
            Photos[i].GetComponent<SpriteRenderer>().material.color = TmpColor;

            foreach (Transform Child in Photos[i].transform)
            {
                if (Child.GetComponent<PhotoImportant>())
                    Child.GetComponent<PhotoImportant>().SetAlpha(0);
            }
        }
    }

    void Start()
    {
        BeginShow = false;
        for (int i = 1; i <= TotalFenJings; ++i)
        {
            string name = i.ToString();
            GameObject Tmp = this.transform.Find(name).gameObject;
            Photos.Add(Tmp);
        }

        DarkGround = this.transform.Find("Dark").gameObject;
        DarkGround.SetActive(false);
    }

    private void PlayTheAudio()
    {
        if (FenJingAudio[ShowNum])
        {
            this.GetComponent<AudioSource>().clip = FenJingAudio[ShowNum];
            this.GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (BeginShow)
        {
            if (ShowTotalTime < ShowTime)
            {
                ShowTotalTime += Time.deltaTime;
                Color TmpColor = Photos[ShowNum].GetComponent<SpriteRenderer>().material.color;
                TmpColor.a = ShowTotalTime / ShowTime;
                Photos[ShowNum].GetComponent<SpriteRenderer>().material.color = TmpColor;

                if (ShowNum == 4)
                {
                    DarkGround.SetActive(true);
                    DarkGround.GetComponent<SpriteRenderer>().material.color = TmpColor;
                }

                if (ShowNum == 7)
                {
                    TmpColor.a = 1 - ShowTotalTime / ShowTime;
                    Photos[6].GetComponent<SpriteRenderer>().material.color = TmpColor;
                }

                if (ShowNum == 8)
                {
                    TmpColor.a = 1 - ShowTotalTime / ShowTime;
                    Photos[7].GetComponent<SpriteRenderer>().material.color = TmpColor;
                }
            }
            else
            {
                CurrentStage |= 1 << ShowNum;
                ShowTotalTime = 0.0f;
                BeginShow = false;
            }
            return;
        }

        if (ShowNum == -1)
        {
            ShowNum++;
            PlayTheAudio();
            BeginShow = true;
            return;
        }

        if (ShowNum >= 2 && ShowNum < 4)
        {

            ShowNum++;
            PlayTheAudio();
            BeginShow = true;
            return;
        }

        if (ShowNum >= 6 && ShowNum < 8)
        {
            ShowNum++;
            PlayTheAudio();
            BeginShow = true;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, Vector2.zero);

            if (!hit)
            {
                return;
            }

            HitCollider = hit.collider;

            if (HitCollider.gameObject.tag.Equals("Close"))
            {
                HitCollider.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, Vector2.zero);

            if (!hit)
            {
                int tmp = 0;
                while ((CurrentStage >> tmp & 1) == 1)
                {
                    tmp++;
                }
                if (tmp < TotalFenJings)
                {
                    ShowNum = tmp;
                    PlayTheAudio();
                    BeginShow = true;
                }
                return;
            }

            HitCollider = hit.collider;

            Debug.Log("windowfenjing:   " + HitCollider.gameObject.name);

            if (HitCollider.gameObject.tag.Equals("PhotoFenjing"))
            {
                if (ShowNum == TotalFenJings - 1)
                    HitCollider.gameObject.GetComponent<PhotoImportant>().GetCollect();
            }
            else if (HitCollider.gameObject.tag.Equals("Close"))
            {
                HitCollider.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                this.gameObject.SetActive(false);
            }
            else
            {
                int tmp = 0;
                while ((CurrentStage >> tmp & 1) == 1)
                {
                    tmp++;
                }
                if (tmp < TotalFenJings)
                {
                    ShowNum = tmp;
                    PlayTheAudio();
                    BeginShow = true;
                }
            }
        }

    }
}
