using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    public AudioClip FurnitueAudio;
    private AudioSource FurnitureAudioSource;

    public float TheScale = 0.1f;

    public bool SpecialThing;

    public string FenJingName;

    private GameObject GameMaster;
	

    public void MouseDown()
    {
        float CurrentScale = TheScale;
        this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);

		FurnitureAudioSource.Play();

        if (SpecialThing)
        {
            GameMaster.GetComponent<GameController_SceneTwo>().ActiveFenJing(FenJingName);
        }
    }

    public void MouseUp()
    {
        float CurrentScale = 1.0f;
        this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);
    }

    void Start()
    {
        FurnitureAudioSource = this.GetComponent<AudioSource>();
        GameMaster = GameObject.Find("SceneController");
    }

    void Update()
    {

    }
}
