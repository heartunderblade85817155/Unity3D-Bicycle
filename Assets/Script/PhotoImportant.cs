using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoImportant : MonoBehaviour
{
    public string TheCollectName;
    private bool Select;
    public float SelectTime = 1.0f;
    private float SelectTotalTime = 0.0f;

    private GameObject RedBookMark;

    private Vector3 InitPos;

    private Vector3 GoalPos = new Vector3(7.5f, 7.3f, -10.0f);

    public bool NeedDelete = false;

    public List<AudioClip> Audios = new List<AudioClip>();

    private GameObject GameMaster;

    private bool GoFinal = false;

    public void GetCollect()
    {
        Select = true;

        InitPos = this.transform.position;

        if (TheCollectName != "")
        {
            int len = Audios.Count;
            if (len > 0)
            {
                len = Mathf.RoundToInt(Random.Range(0, len - 1));
                if (Audios[len] != null)
                {
                    this.GetComponent<AudioSource>().clip = Audios[len];
                    this.GetComponent<AudioSource>().Play();
                }
            }

            RedBookMark.GetComponent<ItemBarController>().AddItem(TheCollectName);
            if (NeedDelete)
                RedBookMark.GetComponent<ItemBarController>().DeleteItem();
        }
    }

    public void SetAlpha(float alpha)
    {
        Color TmpColor = this.GetComponent<SpriteRenderer>().material.color;
        TmpColor.a = alpha;
        this.GetComponent<SpriteRenderer>().material.color = TmpColor;
    }

    void Start()
    {
        RedBookMark = GameObject.Find("RedMenu");

        SetAlpha(0.0f);

        GameMaster = GameObject.Find("SceneController");
    }

    void Update()
    {
        if (Select)
        {
            if (SelectTotalTime < SelectTime)
            {
                SelectTotalTime += Time.deltaTime;
                this.transform.position = InitPos + SelectTotalTime / SelectTime * (GoalPos - InitPos);
            }
            else
            {
                SelectTotalTime = 0.0f;
                Select = false;
                if (this.name.Equals("Mail"))
                {
                    GameMaster.GetComponent<GameController_SceneTwo>().GoNext();
                }
            }
        }
        SetAlpha(this.transform.parent.GetComponent<SpriteRenderer>().material.color.a);
    }
}
