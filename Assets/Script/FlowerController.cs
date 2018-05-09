using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{

    private GameObject RedBookMark;

    private List<GameObject> PinTus = new List<GameObject>();

    public List<Vector3> FinalPos = new List<Vector3>();

    private int CurrentStage;

    private Collider2D HitCollider;
    private Vector3 PreMousePos;

    public float ShowResultTime = 1.0f;
    private float ShowResultTotalTime = 0.0f;

    private GameObject ResultBigOne;
    private GameObject ResultBigTwo;
    private GameObject FlowerPlane;

    public float DeltaHeight = 0.9f;
    public float DeltaHeightTime = 0.5f;
    private float DeltaHeightTotalTime = 0.0f;
    private int ClickTimes = 0;
    private bool ClickFlag;
    private float StartY;

    public float LeafGrowTime = 1.5f;
    private float LeafGrowTotalTime = 0.0f;
    private GameObject Leaf1;
    private Vector3 InitLeafPos1;
    private Vector3 InitLeafScale1;
    public Vector3 FinalLeafPos1;
    private GameObject Leaf2;
    private Vector3 InitLeafPos2;
    private Vector3 InitLeafScale2;
    public Vector3 FinalLeafPos2;
    private GameObject Leaf3;
    private Vector3 InitLeafPos3;
    private Vector3 InitLeafScale3;
    public Vector3 FinalLeafPos3;

    public float FlowerTime = 2.5f;
    private float FlowerTotalTime = 0.0f;

    private GameObject CloseButton;

    private void SetAlpha(float alpha)
    {
        Color TmpColor = ResultBigOne.GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = alpha;
        ResultBigOne.GetComponent<SpriteRenderer>().material.color = TmpColor;

        TmpColor = ResultBigTwo.GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = alpha;
        ResultBigTwo.GetComponent<SpriteRenderer>().material.color = TmpColor;

        TmpColor = FlowerPlane.GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = alpha;
        FlowerPlane.GetComponent<SpriteRenderer>().material.color = TmpColor;
        FlowerPlane.transform.Find("Plane").GetComponent<SpriteRenderer>().material.color = TmpColor;
    }

    private void LeafGrowUp(float canshu)
    {
        Leaf1.transform.localPosition = (FinalLeafPos1 - InitLeafPos1) * canshu + InitLeafPos1;
        Leaf1.transform.localScale = new Vector3((1.0f - InitLeafScale1.x) * canshu + InitLeafScale1.x, (1.0f - InitLeafScale1.y) * canshu + InitLeafScale1.y, 1.0f);

        Leaf2.transform.localPosition = (FinalLeafPos2 - InitLeafPos2) * canshu + InitLeafPos2;
        Leaf2.transform.localScale = new Vector3((1.0f - InitLeafScale2.x) * canshu + InitLeafScale2.x, (1.0f - InitLeafScale2.y) * canshu + InitLeafScale2.y, 1.0f);

        Leaf3.transform.localPosition = (FinalLeafPos3 - InitLeafPos3) * canshu + InitLeafPos3;
        Leaf3.transform.localScale = new Vector3((1.0f - InitLeafScale3.x) * canshu + InitLeafScale3.x, (1.0f - InitLeafScale3.y) * canshu + InitLeafScale3.y, 1.0f);
    }

    public void SetCurrentStage()
    {
        StartY = -100.0f;

        ClickFlag = false;

        ClickTimes = 0;

        CurrentStage = 0;

        PinTus[0].transform.localPosition = FinalPos[0];

        for (int i = 1; i < PinTus.Count; ++i)
        {
            Vector3 RandomPos = Vector3.zero;
            RandomPos.x = Random.Range(-6.2f, -3.5f);
            RandomPos.y = Random.Range(-3.3f, 3.3f);

            float Tmp = Random.Range(-0.1f, 0.1f);
            if (Tmp > 0.0f)
            {
                RandomPos.x = -RandomPos.x;
            }

            PinTus[i].transform.localPosition = RandomPos;
        }
        SetAlpha(0.0f);

        LeafGrowUp(0.0f);

        FlowerPlane.transform.Find("Plante").Find("Flower1").localScale = new Vector3(0.01f, 0.01f, 1.0f);
        FlowerPlane.transform.Find("Plante").Find("Flower2").localScale = new Vector3(0.01f, 0.01f, 1.0f);
    }

    void Start()
    {
        PreMousePos = Vector3.zero;
        PinTus.Clear();
        for (int i = 1; i <= 6; ++i)
        {
            string PinTuName = i.ToString();

            PinTus.Add(this.transform.Find(PinTuName).gameObject);
        }

        CurrentStage = 0;

        ResultBigOne = this.transform.Find("Result1").gameObject;
        ResultBigTwo = this.transform.Find("Result2").gameObject;
        FlowerPlane = this.transform.Find("FlowerPlan").gameObject;

        Leaf1 = FlowerPlane.transform.Find("Plante").Find("Leaf1").gameObject;
        InitLeafPos1 = Leaf1.transform.localPosition;
        InitLeafScale1 = Leaf1.transform.localScale;

        Leaf2 = FlowerPlane.transform.Find("Plante").Find("Leaf2").gameObject;
        InitLeafPos2 = Leaf2.transform.localPosition;
        InitLeafScale2 = Leaf2.transform.localScale;

        Leaf3 = FlowerPlane.transform.Find("Plante").Find("Leaf3").gameObject;
        InitLeafPos3 = Leaf3.transform.localPosition;
        InitLeafScale3 = Leaf3.transform.localScale;

        CloseButton = this.transform.Find("UI_close").gameObject;
        CloseButton.SetActive(false);
    }

    void Update()
    {
        if ((CurrentStage & 1) == 0)
        {
            int num = 0;
            for (int i = 0; i < PinTus.Count; ++i)
            {
                if (PinTus[i].transform.localPosition.Equals(FinalPos[i]))
                {
                    num++;
                }
            }
            if (num == 6)
            {
                CurrentStage |= 1;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!HitCollider)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, Vector2.zero);

                        if (!hit)
                        {
                            return;
                        }

                        HitCollider = hit.collider;
                    }

                    Debug.Log("FlowerFenjing:" + HitCollider.name);

                    if (HitCollider.gameObject.tag.Equals("PinTu") && !HitCollider.gameObject.name.Equals("1"))
                    {
                        if (PreMousePos.Equals(Vector3.zero))
                        {
                            PreMousePos = MouseWorldPos;
                            return;
                        }
                        else
                        {
                            Vector3 Offset = MouseWorldPos - PreMousePos;
                            HitCollider.transform.position = new Vector3(HitCollider.transform.position.x + Offset.x,
                                        HitCollider.transform.position.y + Offset.y, HitCollider.transform.position.z);

                            PreMousePos = MouseWorldPos;
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    float MinDis = 10000.0f;
                    int Num = -1;
                    for (int i = 0; i < FinalPos.Count; ++i)
                    {
                        Vector2 Tmp1 = new Vector2(HitCollider.transform.position.x, HitCollider.transform.position.y);
                        Vector2 Tmp2 = new Vector2(FinalPos[i].x, FinalPos[i].y);

                        float TmpDis = (Tmp1 - Tmp2).magnitude;

                        if (TmpDis < MinDis && TmpDis < 0.5f)
                        {
                            MinDis = (Tmp1 - Tmp2).magnitude;
                            Num = i;
                        }
                    }
                    if (Num != -1)
                    {
                        HitCollider.transform.localPosition = FinalPos[Num];
                    }

                    HitCollider = null;
                    PreMousePos = Vector3.zero;
                }
            }
        }
        else if ((CurrentStage >> 1 & 1) == 0 && (CurrentStage & 1) == 1)
        {
            if (ShowResultTotalTime < ShowResultTime)
            {
                Color TmpColor;
                ShowResultTotalTime += Time.deltaTime;
                for (int i = 0; i < PinTus.Count; ++i)
                {
                    TmpColor = PinTus[i].GetComponent<SpriteRenderer>().material.color;
                    TmpColor.a = Mathf.Clamp(1 - ShowResultTotalTime / ShowResultTime, 0.0f, 1.0f);
                    PinTus[i].GetComponent<SpriteRenderer>().material.color = TmpColor;
                }
                SetAlpha(Mathf.Clamp(ShowResultTotalTime / ShowResultTime, 0.0f, 1.0f));
            }
            else
            {
                CurrentStage |= 1 << 1;
                ShowResultTotalTime = 0.0f;
                FlowerPlane.transform.Find("Plante").localPosition = new Vector3(0.0f, -2.5f, 0.0f);
            }
        }
        else if ((CurrentStage >> 2 & 1) == 0 && (CurrentStage >> 1 & 1) == 1)
        {
            if (ClickFlag)
            {
                if (DeltaHeightTotalTime < DeltaHeightTime)
                {
                    DeltaHeightTotalTime += Time.deltaTime;
                    Vector3 CurrentPos = FlowerPlane.transform.Find("Plante").localPosition;
                    FlowerPlane.transform.Find("Plante").localPosition = new Vector3(CurrentPos.x, StartY + DeltaHeight * (DeltaHeightTotalTime / DeltaHeightTime), CurrentPos.z);
                }
                else
                {
                    ClickTimes++;
                    ClickFlag = false;
                    DeltaHeightTotalTime = 0.0f;
                }
            }

            if (Input.GetMouseButtonDown(0) && ClickTimes < 5)
            {
                StartY = FlowerPlane.transform.Find("Plante").localPosition.y;
                ClickFlag = true;
            }
            else if (ClickTimes >= 5)
            {
                CurrentStage |= 1 << 2;
            }
        }
        else if ((CurrentStage >> 3 & 1) == 0 && (CurrentStage >> 2 & 1) == 1)
        {
            if (LeafGrowTotalTime < LeafGrowTime)
            {
                LeafGrowTotalTime += Time.deltaTime;
                LeafGrowUp(LeafGrowTotalTime / LeafGrowTime);
                Color TmpColor = ResultBigOne.GetComponent<SpriteRenderer>().material.color;
                TmpColor.a = 1 - LeafGrowTotalTime / LeafGrowTime;
                ResultBigOne.GetComponent<SpriteRenderer>().material.color = TmpColor;
            }
            else
            {
                LeafGrowTotalTime = 0.0f;
                CurrentStage |= 1 << 3;
            }
        }
        else if ((CurrentStage >> 4 & 1) == 0 && (CurrentStage >> 3 & 1) == 1)
        {
            if (FlowerTotalTime < FlowerTime)
            {
                FlowerTotalTime += Time.deltaTime;
                FlowerPlane.transform.Find("Plante").Find("Flower1").localScale = new Vector3(0.01f + 0.99f * (FlowerTotalTime / FlowerTime), 0.01f + 0.99f * (FlowerTotalTime / FlowerTime), 1.0f);
                FlowerPlane.transform.Find("Plante").Find("Flower2").localScale = new Vector3(0.01f + 0.99f * (FlowerTotalTime / FlowerTime), 0.01f + 0.99f * (FlowerTotalTime / FlowerTime), 1.0f);
            }
            else
            {
                FlowerTotalTime = 0.0f;
                CurrentStage |= 1 << 4;
            }
        }
        else if ((CurrentStage >> 5 & 1) == 0 && (CurrentStage >> 4 & 1) == 1)
        {
            CloseButton.SetActive(true);

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
                    return;
                }

                HitCollider = hit.collider;

                if (HitCollider.gameObject.tag.Equals("PhotoFenjing"))
                {
                    HitCollider.gameObject.GetComponent<PhotoImportant>().GetCollect();
                }
                else if (HitCollider.gameObject.tag.Equals("Close"))
                {
                    HitCollider.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
