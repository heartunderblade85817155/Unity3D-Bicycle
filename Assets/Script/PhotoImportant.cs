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

    public void GetCollect()
    {
        Select = true;

        InitPos = this.transform.position;

        if (TheCollectName != "")
        {
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
            }
        }
        SetAlpha(this.transform.parent.GetComponent<SpriteRenderer>().material.color.a);
    }
}
