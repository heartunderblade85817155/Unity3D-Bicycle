using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTRController : MonoBehaviour
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

    public List<AudioClip> FenJingAudio;

    public float Rotate = 30.0f;
    public float GuitarRotateTime = 1.0f;
    private float GuitarRotateTotalTime = 0.0f;

    public float PersonRotatePerSecond = 10.0f;
    public float PersonAppearTime = 3.0f;
    private float PersonAppearTotalTime = 0.0f;
    private bool CheckPersonAppear = false;

    private GameObject TheScrub = null;

    private bool CanScrub;

    private Queue<GameObject> ScrubsPool = new Queue<GameObject>();

    public List<Vector2> CheckPoints = new List<Vector2>();
    private bool[] CheckCover = new bool[32];


    private GameObject MailNum;

    public float ChuFenDanMoveTime = 1.0f;
    private float ChuFenDanMoveTotalTime = 0.0f;
    public Vector3 GoalPos;
    private Vector3 ChuFenDanInitPos;

    private GameObject BlackBG;
    private Vector2 MoveDir;

    public void SetCurrentStage()
    {
        for (int i = 0; i < CheckCover.Length; ++i)
        {
            CheckCover[i] = false;
        }

        ChuFenDanMoveTotalTime = 0.0f;

        ScrubsPool.Clear();

        CurrentStage = 0;

        ShowNum = -1;

        CheckPersonAppear = false;

        CanScrub = true;

        GuitarRotateTotalTime = 0.0f;

        if (Photos.Count == 0)
            Start();

        for (int i = 0; i < Photos.Count; ++i)
        {
            SetAlpha(i, 0.0f);
        }

        BlackBG = this.transform.Find("Black").gameObject;
        BlackBG.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", 0.0f);
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
        TheScrub = Resources.Load("TheScrub") as GameObject;
    }

    private void PlayTheAudio()
    {
        if (FenJingAudio[ShowNum])
        {
            this.GetComponent<AudioSource>().clip = FenJingAudio[ShowNum];
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void SetAlpha(int ID, float Alpha)
    {
        Color TmpColor = Photos[ID].GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = Alpha;
        Photos[ID].GetComponent<SpriteRenderer>().material.color = TmpColor;

        foreach (Transform Child in Photos[ID].transform)
        {
            if (Child.name.Equals("TheMailNum"))
            {
                MailNum = Child.gameObject;
                MailNum.SetActive(false);
            }

            TmpColor = Child.GetComponent<SpriteRenderer>().material.color;
            TmpColor.a = Alpha;
            Child.GetComponent<SpriteRenderer>().material.color = TmpColor;
        }
    }

    private bool CheckCanScrub()
    {
        int Total = 0;
        for (int i = 0; i < CheckCover.Length; ++i)
        {
            if (CheckCover[i])
            {
                Total++;
            }
        }
        if ((float)Total / (float)CheckPoints.Count > 0.85f)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (BeginShow)
        {
            if (ShowTotalTime < ShowTime)
            {
                ShowTotalTime += Time.deltaTime;
                if (ShowNum != 5)
                {
                    SetAlpha(ShowNum, ShowTotalTime / ShowTime);
                }
                else if (ShowNum == 5)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        SetAlpha(i, 1 - ShowTotalTime / ShowTime);
                    }
                    SetAlpha(ShowNum, ShowTotalTime / ShowTime);
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

        if ((CurrentStage >> 5 & 1) == 1 && (CurrentStage >> 6 & 1) == 0)
        {
            if (GuitarRotateTotalTime < GuitarRotateTime)
            {
                GuitarRotateTotalTime += Time.deltaTime;

                Photos[ShowNum].transform.RotateAround(new Vector3(-4.09f, -3.11f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f), -Rotate * Time.deltaTime / GuitarRotateTime);
            }
            else
            {
                CurrentStage |= 1 << 6;
                GuitarRotateTotalTime = 0.0f;
            }
            return;
        }
        else if ((CurrentStage >> 7 & 1) == 1 && (CurrentStage >> 8 & 1) == 0)
        {
            if (CheckPersonAppear == false)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    CheckPersonAppear = true;
                }
            }
            else
            {
                if (PersonAppearTotalTime < PersonAppearTime)
                {
                    PersonAppearTotalTime += Time.deltaTime;
                    float Bili = PersonAppearTotalTime / PersonAppearTime;
                    Photos[7].transform.localScale = new Vector3(1 - Bili, 1 - Bili, 1.0f);
                    Photos[7].transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), PersonRotatePerSecond);
                }
                else
                {
                    PersonAppearTotalTime = 0.0f;
                    CurrentStage |= 1 << 8;
                    ShowNum = 8;
                    BlackBG.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", 1.0f);
                    BeginShow = true;
                }
            }
            return;
        }
        else if ((CurrentStage >> 8 & 1) == 1 && (CurrentStage >> 9 & 1) == 0)
        {
            if (TheScrub && CanScrub)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Vector2 WorldCreatePos = new Vector2(WorldPos.x, WorldPos.y);

                    for (int i = 0; i < CheckPoints.Count; ++i)
                    {
                        if ((WorldCreatePos - CheckPoints[i]).magnitude < 0.6f)
                        {
                            CheckCover[i] = true;
                        }
                    }

                    if (ScrubsPool.Count < 500)
                    {
                        GameObject NewScrub = Instantiate(TheScrub);
                        NewScrub.transform.position = new Vector3(WorldCreatePos.x, WorldCreatePos.y, -30.0f);
                        ScrubsPool.Enqueue(NewScrub);
                    }
                    else
                    {
                        GameObject TheOld = ScrubsPool.Dequeue();
                        TheOld.transform.position = new Vector3(WorldCreatePos.x, WorldCreatePos.y, -30.0f);
                        ScrubsPool.Enqueue(TheOld);
                    }
                }

                if (CheckCanScrub())
                {
                    Color TmpColor = MailNum.GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = 0.0f;
                    MailNum.GetComponent<SpriteRenderer>().material.color = TmpColor;

                    MailNum.SetActive(true);
                    CanScrub = false;
                }
            }
            else
            {
                if (ShowTotalTime < ShowTime)
                {
                    ShowTotalTime += Time.deltaTime;
					Color TmpColor = MailNum.GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = ShowTotalTime / ShowTime;
                    MailNum.GetComponent<SpriteRenderer>().material.color = TmpColor;

					BlackBG.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", 1 - TmpColor.a);
                }
                else
                {
					ShowTotalTime = 0.0f;
                    BlackBG.SetActive(false);
					for (int i = 0; i < TotalFenJings; ++i)
					{
						if (i == 6 || i == 8)
						{
							continue;
						}
						Photos[i].SetActive(false);
					}
					this.transform.Find("BG").gameObject.SetActive(false);
                    ChuFenDanInitPos = Photos[8].transform.position;
                    MoveDir = new Vector2(GoalPos.x, GoalPos.y) - new Vector2(ChuFenDanInitPos.x, ChuFenDanInitPos.y);
                    CurrentStage |= 1 << 9;
                }
            }
            return;
        }
        else if ((CurrentStage >> 9 & 1) == 1 && (CurrentStage >> 10 & 1) == 0)
        {
            if (ChuFenDanMoveTotalTime < ChuFenDanMoveTime)
            {
                ChuFenDanMoveTotalTime += Time.deltaTime;

                Photos[8].transform.position = MoveDir * (ChuFenDanMoveTotalTime / ChuFenDanMoveTime);
                float tmp = Mathf.Max(1 - (ChuFenDanMoveTotalTime / ChuFenDanMoveTime), 0.2f);
                Photos[8].transform.localScale = new Vector3(tmp, tmp, 1.0f);
            }
            else
            {
				if (ShowTotalTime < ShowTime)
                {
                    ShowTotalTime += Time.deltaTime;
					Color TmpColor = Photos[6].GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = 1 - ShowTotalTime / ShowTime;
					Photos[6].GetComponent<SpriteRenderer>().material.color = TmpColor;
				}
				else
				{
                    GameObject.Find("RedMenu").GetComponent<ItemBarController>().AddItem("chufen");
					ShowTotalTime = 0.0f;
					this.gameObject.SetActive(false);
					CurrentStage |= 1 << 10;
				}
            }
            return;
        }


        if (ShowNum == -1 || ShowNum == 0 || ShowNum == 5 || ShowNum == 6)
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
