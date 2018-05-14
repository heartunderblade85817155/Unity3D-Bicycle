using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePassWord : MonoBehaviour
{
    private int CurrentNum;

    private GameObject TheNum;

	private static List<GameObject> Nums = new List<GameObject>();

    public void SetNum(int Num)
    {
		CurrentNum = Num;

        Start();

        TheNum.GetComponent<SpriteRenderer>().sprite = Nums[CurrentNum].GetComponent<SpriteRenderer>().sprite;
    }

    public void ChangeNum()
    {
        CurrentNum++;

		CurrentNum %= 10;

        TheNum.GetComponent<SpriteRenderer>().sprite = Nums[CurrentNum].GetComponent<SpriteRenderer>().sprite;
    }

    public int GetNum()
    {
        return CurrentNum;
    }

    void Start()
    {
		if (Nums.Count == 0)
		{
			for (int i = 0; i < 10 ; ++i)
			{
				string NumName = "mailBox_PasswordLock_0" + i;
				GameObject TheNum = Resources.Load(NumName) as GameObject;

				Nums.Add(TheNum);
			}
		}

        CurrentNum = 0;

        TheNum = this.transform.Find("Num").gameObject;
    }

    void Update()
    {
    }
}
