using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarController : MonoBehaviour
{

    private GameObject[] ItemBars = new GameObject[5];

    private List<GameObject> ItemList = new List<GameObject>();

    private GameObject TheSelectItem;

    public Dictionary<string, AudioClip> AudioForName;

    private GameObject GameMaster;

    void Start()
    {
        GameMaster = GameObject.Find("SceneController");

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
            if (ItemList[i].transform.Find("Item") == null)
            {
                GameObject ThisItemPicture = Resources.Load(name) as GameObject;
                GameObject TheItem = Instantiate(ThisItemPicture);

                TheItem.transform.parent = ItemList[i].transform;

                if (name.Equals("jiangpai"))
                {
                    TheItem.transform.localPosition = new Vector3(0.3f, 0.0f, 0.0f);
                }
                else
                {
                    TheItem.transform.localPosition = Vector3.zero;
                }

                TheItem.name = "Item";
                break;
            }
        }
    }

    public void DeleteItem()
    {
        if (!TheSelectItem)
        {
            return;
        }
        bool flag = false;
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (!flag)
            {
                if (ItemList[i].Equals(TheSelectItem))
                {
                    flag = true;
                    GameObject.Destroy(ItemList[i].transform.Find("Item").gameObject);
                    continue;
                }
            }
            else
            {
                if (ItemList[i].transform.Find("Item"))
                {
                    ItemList[i].transform.Find("Item").SetParent(ItemList[i - 1].transform, false);
                }
                else
                {
                    break;
                }
            }
        }
        TheSelectItem = null;
        ShowWhichItem("");
    }

    public void ChangeItemState(string TheName)
    {
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (ItemList[i].name.Equals(TheName))
            {
                TheSelectItem = ItemList[i];

                if (TheSelectItem.transform.Find("Item"))
                    TheSelectItem.transform.Find("Item").GetComponent<SpriteRenderer>().sortingOrder = 13;

                break;
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



                if (TheSelectItem.transform.Find("Item"))
                {
                    TheSelectItem.transform.Find("Item").GetComponent<SpriteRenderer>().sortingOrder = 15;
                    TheSelectItem.transform.Find("Item").GetComponent<AudioSource>().Play();
                }

                ItemList[i].transform.GetComponent<ItemController>().SetShowSign(true);
            }
            else
            {
                ItemList[i].transform.GetComponent<ItemController>().SetShowSign(false);
            }
        }
    }

    public string GetSelectItemName()
    {
        if (!TheSelectItem)
        {
            return "null";
        }
        if (!TheSelectItem.transform.Find("Item"))
        {
            return "null";
        }
        return TheSelectItem.transform.Find("Item").GetComponent<SpriteRenderer>().sprite.name;
    }

    public bool CheckGetTheItem(string TheName)
    {
        for (int i = 0; i < ItemList.Count; ++i)
        {
            if (ItemList[i].transform.Find("Item") == null)
            {
                return false;
            }
            else if (ItemList[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite.name.Equals(TheName))
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {

    }
}
