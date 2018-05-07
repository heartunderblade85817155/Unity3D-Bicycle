using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpButton : MonoBehaviour
{

    private GameObject Normal;
    private GameObject Over;
    private GameObject Down;

    private void OnMouseOver()
    {
        if (Down.activeInHierarchy)
        {
            return;
        }

        Normal.SetActive(false);
        Over.SetActive(true);
        Down.SetActive(false);
    }

    void Start()
    {
        Normal = this.transform.Find("Normal").gameObject;
        Over = this.transform.Find("Over").gameObject;
        Down = this.transform.Find("Down").gameObject;

        Normal.SetActive(true);
        Over.SetActive(false);
        Down.SetActive(false);
    }

    public void GetMouseDown()
    {
        Normal.SetActive(false);
        Over.SetActive(false);
        Down.SetActive(true);
    }

    public void GetMouseUp()
    {
        Normal.SetActive(true);
        Over.SetActive(false);
        Down.SetActive(false);

    }

    void Update()
    {

    }
}
