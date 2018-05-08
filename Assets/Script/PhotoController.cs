using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour
{
    public float ShowTime = 0.5f;
    private float ShowTotalTime = 0.0f;
    private bool BeginShow;
    private int ShowNum;

    private int ImportStage;

    private int CurrentStage;

    private Collider2D HitCollider;

    private List<GameObject> Photos = new List<GameObject>();

	private GameObject GameMaster;

    public void SetCurrentStage()
    {
        CurrentStage = 0;

        for (int i = 0; i < Photos.Count; ++i)
        {
            Color TmpColor = Photos[i].GetComponent<SpriteRenderer>().material.color;
            TmpColor.a = 0.0f;
            Photos[i].GetComponent<SpriteRenderer>().material.color = TmpColor;
        }
    }

    void Start()
    {
        BeginShow = false;
        for (int i = 1; i <= 5; ++i)
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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, Vector2.zero);

            HitCollider = hit.collider;

            if (HitCollider.gameObject.tag.Equals("PhotoFenjing"))
            {
				HitCollider.gameObject.GetComponent<PhotoImportant>().GetCollect();
            }
			else if (HitCollider.gameObject.tag.Equals("Close"))
			{
				this.gameObject.SetActive(false);
				GameMaster.GetComponent<CheckMouseButton>().SetEnableOrNot(true);
			}
            else
            {
                int tmp = 0;
                while ((CurrentStage >> tmp & 1) == 1)
                {
                    tmp++;
                }
                if (tmp < 5)
                {
                    ShowNum = tmp;
                    BeginShow = true;
                }
            }
        }

    }
}
