using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarController : MonoBehaviour
{

    private GameObject[] ItemBars = new GameObject[5];

    private List<GameObject> ItemList = new List<GameObject>();

    private GameObject TheSelectItem;

    void Start()
    {
        ItemBars = GameObject.FindGameObjectsWithTag("Collect");

        int len = ItemBars.Length;
        for (int i = 0; i < len; ++i)
        {
            ItemList.Add(ItemBars[i]);
        }

        ItemList.Sort(delegate (GameObject a, GameObject b) { return b.transform.localPosition.y.CompareTo(a.transform.localPosition.y); });
    }

    public void AddItem(string name)
    {
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (ItemList[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite == null)
            {
                Sprite ThisItemPicture = Resources.Load(name) as Sprite;
                ItemList[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = ThisItemPicture;
            }

        }
    }

    public void DeleteItem()
    {
        bool flag = false;
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (!flag)
            {
                if (ItemList[i].Equals(TheSelectItem))
                {
					flag = true;
					continue;
                }
            }
			else
			{
				ItemList[i - 1].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = ItemList[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
				ItemList[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = null;
			}
        }
    }

    public void ShowWhichItem(string TheName)
    {
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (ItemList[i].name.Equals(TheName))
            {
                TheSelectItem = ItemList[i];
                ItemList[i].transform.GetComponent<ItemController>().SetShowSign(true);
            }
            else
            {
                ItemList[i].transform.GetComponent<ItemController>().SetShowSign(false);
            }
        }
    }

    void Update()
    {

    }
}
