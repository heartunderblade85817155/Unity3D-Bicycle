using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MailController : MonoBehaviour
{
    public float ShowTime = 0.5f;
    private float ShowTotalTime = 0.0f;
    private bool BeginShow;
    private int ShowNum = -1;

    public int TotalFenJings = 5;

    private int ImportStage;

    protected int CurrentStage;

    private Collider2D HitCollider;

    private List<GameObject> Photos = new List<GameObject>();

    private GameObject GameMaster;

    public List<AudioClip> FenJingAudio;

    private List<GameObject> PassLocks = new List<GameObject>();

    private bool IsMailBoxOpen = false;


    private void ChangeAlpha(int ID, float alpha)
    {
        if (ID != 1)
        {
            Color TmpColor = Photos[ID].GetComponent<SpriteRenderer>().material.color;
            TmpColor.a = alpha;
            Photos[ID].GetComponent<SpriteRenderer>().material.color = TmpColor;
            foreach (Transform Child in Photos[ID].transform)
            {
                if (Child.name.Equals("hand"))
                {
                    Child.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", alpha);
                }
                else
                {
                    TmpColor = Child.GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = alpha;
                    Child.GetComponent<SpriteRenderer>().material.color = TmpColor;

                    foreach (Transform Son in Child)
                    {
                        TmpColor = Son.GetComponent<SpriteRenderer>().material.color;
                        TmpColor.a = alpha;
                        Son.GetComponent<SpriteRenderer>().material.color = TmpColor;
                    }
                }
            }
        }
        else
        {
            Photos[ID].GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", alpha);
            foreach (Transform Child in Photos[ID].transform)
            {
                if (Child.name.Equals("hand"))
                {
                    Child.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", alpha);
                }
                else
                {
                    Color TmpColor = Child.GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = alpha;
                    Child.GetComponent<SpriteRenderer>().material.color = TmpColor;

                    foreach (Transform Son in Child)
                    {
                        TmpColor = Son.GetComponent<SpriteRenderer>().material.color;
                        TmpColor.a = alpha;
                        Son.GetComponent<SpriteRenderer>().material.color = TmpColor;
                    }
                }
            }
        }
    }


    public void SetCurrentStage()
    {
        CurrentStage = 0;

        ShowNum = -1;

        IsMailBoxOpen = false;

        if (Photos.Count == 0)
            Start();

        PassLocks.Clear();

        foreach (Transform Child in Photos[1].transform)
        {
            if (Child.GetComponent<ChangePassWord>())
            {
                PassLocks.Add(Child.gameObject);
            }
        }

        for (int i = 0; i < PassLocks.Count; ++i)
        {
            PassLocks[i].GetComponent<ChangePassWord>().SetNum(0);
        }

        for (int i = 0; i < TotalFenJings; ++i)
        {
            ChangeAlpha(i, 0.0f);

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
        GameMaster = GameObject.Find("SceneController");
    }

    void Update()
    {
        // 如果密码输入正确
        if ((ShowNum == 1 || ShowNum == 2) && !IsMailBoxOpen && PassLocks[0].GetComponent<ChangePassWord>().GetNum() == 9 &&
            PassLocks[1].GetComponent<ChangePassWord>().GetNum() == 5 && PassLocks[2].GetComponent<ChangePassWord>().GetNum() == 6)
        {
            if (ShowNum == 1)
                ShowNum++;

            if (ShowTotalTime < ShowTime)
            {
                ShowTotalTime += Time.deltaTime;
                ChangeAlpha(ShowNum, ShowTotalTime / ShowTime);
                ChangeAlpha(0, 1 - ShowTotalTime / ShowTime);
            }
            else
            {
                CurrentStage |= 1 << ShowNum;
                ShowTotalTime = 0.0f;
                ShowNum = 2;
                IsMailBoxOpen = true;
                BeginShow = false;
            }
            return;
        }

        // 普通分镜的出现
        if (BeginShow)
        {
            if (ShowTotalTime < ShowTime)
            {
                ShowTotalTime += Time.deltaTime;
                ChangeAlpha(ShowNum, ShowTotalTime / ShowTime);
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

            //播放分镜声音
            if (FenJingAudio[ShowNum])
            {
                GameMaster.GetComponent<GameController_SceneTwo>().PlayAudio(FenJingAudio[ShowNum]);
            }
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
            else if (HitCollider.tag.Equals("PassWordLock"))
            {
                HitCollider.GetComponent<ChangePassWord>().ChangeNum();
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
                    if (tmp == 2)
                    {
                        return;
                    }

                    ShowNum = tmp;

                    //播放分镜声音
                    if (FenJingAudio[ShowNum])
                    {
                        GameMaster.GetComponent<GameController_SceneTwo>().PlayAudio(FenJingAudio[ShowNum]);
                    }

                    BeginShow = true;
                }
                return;
            }

            HitCollider = hit.collider;

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
                    if (tmp == 2)
                    {
                        return;
                    }

                    ShowNum = tmp;

                    //播放分镜声音
                    if (FenJingAudio[ShowNum])
                    {
                        GameMaster.GetComponent<GameController_SceneTwo>().PlayAudio(FenJingAudio[ShowNum]);
                    }

                    BeginShow = true;
                }
            }
        }

    }
}
