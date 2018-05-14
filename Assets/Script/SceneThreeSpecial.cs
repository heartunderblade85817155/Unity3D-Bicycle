using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeSpecial : MonoBehaviour
{
    private bool OnlyOne = true;

    private bool GirlOne = true;

    private bool NTROne = true;

    private GameObject FenJingGirl;

    private GameObject FenJingNTR;

    private GameObject RedBookMark;

    private GameObject[] FenJings = new GameObject[8];
    void Start()
    {
        FenJingGirl = null;

        RedBookMark = GameObject.Find("RedMenu");
    }

    void Update()
    {
        if (OnlyOne)
        {
            RedBookMark.GetComponent<ItemBarController>().AddItem("jiezhi");
            OnlyOne = false;
        }

        if (FenJingGirl == null)
        {
            FenJings = GameObject.FindGameObjectsWithTag("FenJings");

            for (int i = 0; i < FenJings.Length; ++i)
            {
                if (FenJings[i].name.Equals("FenJingGirl"))
                {
                    FenJingGirl = FenJings[i];
                    return;
                }
            }
        }

        if (GirlOne)
        {
            if (FenJingGirl)
            {
                if (!FenJingGirl.activeInHierarchy)
                {
                    GameObject OpenDoor = Resources.Load("openDoor") as GameObject;

                    GameObject TheDoor = Instantiate(OpenDoor);
                    TheDoor.name = "openDoor";
                    TheDoor.transform.parent = GameObject.Find("ButtonDownCheck").transform;
                    TheDoor.transform.localPosition = new Vector3(4.405f, 0.614f, -1.0f);

                    GameObject.Find("ButtonDownCheck").transform.Find("girl").gameObject.SetActive(false);
                    GirlOne = false;
                    RedBookMark.GetComponent<ItemBarController>().DeleteItem();
                    return;
                }
            }
        }

        if (FenJingNTR == null)
        {
            FenJings = GameObject.FindGameObjectsWithTag("FenJings");

            for (int i = 0; i < FenJings.Length; ++i)
            {
                if (FenJings[i].name.Equals("FenJingNTR"))
                {
                    FenJingNTR = FenJings[i];
                    return;
                }
            }
        }

        if (NTROne)
        {
            if (FenJingNTR)
            {
                if (!FenJingNTR.activeInHierarchy)
                {
                    GameObject OpenDoor = Resources.Load("Cordon") as GameObject;
                    GameObject TheDoor = Instantiate(OpenDoor);

                    GameObject.Destroy(GameObject.Find("openDoor"));

                    TheDoor.transform.parent = GameObject.Find("ButtonDownCheck").transform;
                    TheDoor.transform.localPosition = new Vector3(4.405f, 0.614f, -1.0f);

                    NTROne = false;
                    return;
                }
            }
        }

    }
}
