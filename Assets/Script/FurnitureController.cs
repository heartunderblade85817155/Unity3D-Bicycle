using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    public AudioClip FurnitueAudio;
    private AudioSource FurnitureAudioSource;

    public float ScaleTime = 0.3f;
    private float ScaleTotalTime = 0.0f;
    public float TheScale = 0.1f;

    public bool SpecialThing;


    public void MouseDown()
    {
        if (ScaleTotalTime < ScaleTime)
        {
            ScaleTotalTime += Time.deltaTime;
            float CurrentScale = 1.0f + TheScale * (ScaleTotalTime / ScaleTime);
            this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);
        }
        else
        {
            ScaleTotalTime = ScaleTime;
        }
    }

    public void MouseUp()
    {
        if (ScaleTotalTime > 0.0f)
        {
            ScaleTotalTime -= Time.deltaTime;
            float CurrentScale = 1.0f + TheScale * (ScaleTotalTime / ScaleTime);
            this.transform.localScale = new Vector3(CurrentScale, CurrentScale, 1.0f);
        }
        else
        {
            ScaleTotalTime = 0.0f;
        }
    }

    void Start()
    {
        FurnitureAudioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {

    }
}
