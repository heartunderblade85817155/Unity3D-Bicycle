using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FurnitureController : MonoBehaviour
{

    public float TheScale = 0.1f;
    public float OriginScale = 1.0f;

    public bool SpecialThing;

    public string SpecialThingName;

    public string FenJingName = null;

    private GameObject GameMaster;

    private GameObject RedBookMark;

    private GameObject Person;

    public string Message;

    public List<AudioClip> NormalAudios;

    public AudioClip SpecialAudio;


    public void MouseDown()
    {
        float CurrentScale = TheScale;
        this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);

        int len = NormalAudios.Count;

        if (RedBookMark.GetComponent<ItemBarController>().CheckGetTheItem(SpecialThingName))
        {
            GameMaster.GetComponent<GameController_SceneTwo>().PlayAudio(SpecialAudio);
        }
        else
        {
            if (len > 0)
            {
                len = Mathf.RoundToInt(Random.Range(0, len - 1));
                GameMaster.GetComponent<GameController_SceneTwo>().PlayAudio(NormalAudios[len]);
            }
        }
    }

    public void MouseUp()
    {
        this.transform.localScale = new Vector3(OriginScale, OriginScale, 1.0f);

        if (FenJingName != "")
        {
            if (SpecialThing)
            {
                string ImportantName = RedBookMark.GetComponent<ItemBarController>().GetSelectItemName();
                if (ImportantName.Equals(SpecialThingName))
                {
                    GameMaster.GetComponent<GameController_SceneTwo>().ActiveFenJing(FenJingName);
                }
                else
                {
                    if (Person)
                        Person.GetComponent<MessageController>().SetMessage(Message);
                }
            }
            else
            {
                GameMaster.GetComponent<GameController_SceneTwo>().ActiveFenJing(FenJingName);
            }
        }
        else
        {
            // 药 + 人 = 场景2
            if (SpecialThing)
            {
                string ImportantName = RedBookMark.GetComponent<ItemBarController>().GetSelectItemName();

                if (ImportantName.Equals(SpecialThingName))
                {
                   this.GetComponent<SceneTwoFinal>().GoNextScene();
                   return;
                }
            }
            if (Person)
                Person.GetComponent<MessageController>().SetMessage(Message);
        }
    }


    void Start()
    {
        GameMaster = GameObject.Find("SceneController");
        RedBookMark = GameObject.Find("RedMenu");

        Person = GameObject.Find("Person");
    }

    void Update()
    {
       
    }
}
