using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    public AudioClip FurnitueAudio;
    private AudioSource FurnitureAudioSource;

    public float TheScale = 0.1f;

    public bool SpecialThing;

    public string SpecialThingName;

    public string FenJingName = null;

    private GameObject GameMaster;

    private GameObject RedBookMark;

    private GameObject Person;

    public string Message;
	

    public void MouseDown()
    {
        float CurrentScale = TheScale;
        this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);

		FurnitureAudioSource.Play();
    }

    public void MouseUp()
    {
        float CurrentScale = 1.0f;
        this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);
        
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
            Person.GetComponent<MessageController>().SetMessage(Message);
        }
    }


    void Start()
    {
        FurnitureAudioSource = this.GetComponent<AudioSource>();
        GameMaster = GameObject.Find("SceneController");
        RedBookMark = GameObject.Find("RedMenu");

        Person = GameObject.Find("Person");
    }

    void Update()
    {

    }
}
