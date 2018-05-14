using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTwoFinal : MonoBehaviour 
{
	public float BlackAppearTime = 2f;
    private float BlackAppearTotalTime = 0.0f;
    private GameObject TheBlackGround;

    private bool TheFinal = false;
    private bool NextIs2 = false;

	private GameObject RedBookMark;

	void Start () 
	{
		TheBlackGround = GameObject.Find("SecondBlackBG");
		Color Tmpcolor = TheBlackGround.GetComponent<SpriteRenderer>().color;
		Tmpcolor.a = 0.0f;
		TheBlackGround.GetComponent<SpriteRenderer>().color = Tmpcolor;

        TheBlackGround.SetActive(false);
		RedBookMark = GameObject.Find("RedMenu");
	}

	public void GoNextScene()
	{
		TheBlackGround.SetActive(true);
        TheFinal = true;
                    
        if (!RedBookMark.GetComponent<ItemBarController>().CheckGetTheItem("ui_jiezhi_"))
        {
            NextIs2 = false;
        }
        else
        {
            NextIs2 = true;
        }
	}
	
	void Update () 
	{
		 if (TheFinal)
        {
            if (BlackAppearTotalTime < BlackAppearTime)
            {
                BlackAppearTotalTime += Time.deltaTime;
                Color TmpColor = TheBlackGround.GetComponent<SpriteRenderer>().color;
                TmpColor.a = BlackAppearTotalTime / BlackAppearTime;
                TheBlackGround.GetComponent<SpriteRenderer>().color = TmpColor;

                if (!NextIs2)
                {
					GameObject.Find("FinalText").GetComponent<TextMeshPro>().color = TmpColor;
				}
            }
            else
            {
                if (NextIs2)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
	}
}
