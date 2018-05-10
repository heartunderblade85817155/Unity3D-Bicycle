using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour
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

    private GameObject GameMaster;

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
        GameMaster = GameObject.Find("SceneController");
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
                    BeginShow = true;
                }
                return;
            }

            HitCollider = hit.collider;

            if (HitCollider.gameObject.tag.Equals("PhotoFenjing"))
            {
                if (ShowNum == TotalFenJings - 1)
				    HitCollider.gameObject.GetComponent<PhotoImportant>().GetCollect();
                if (HitCollider.gameObject.name.Equals("Medicine"))
                {
                    GameMaster.GetComponent<GameController_SceneTwo>().SetYao();
                }
            }
			else if (HitCollider.gameObject.tag.Equals("Close"))
			{
                HitCollider.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				this.gameObject.SetActive(false);
                GameMaster.GetComponent<GameController_SceneTwo>().NextScene();
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
                    BeginShow = true;
                }
            }
        }

    }
}
