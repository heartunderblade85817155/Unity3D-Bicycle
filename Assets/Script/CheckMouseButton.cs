﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseButton : MonoBehaviour
{

    private Collider2D HitCollider = null;

    private bool TimeOut;

    private bool EnableOrNot;

    void Start()
    {
        TimeOut = false;

        EnableOrNot = true;
    }

    public void SetTimeOut(bool flag)
    {
        TimeOut = flag;
    }

    public void SetEnableOrNot(bool flag)
    {
        EnableOrNot = flag;
    }

    void Update()
    {
        if (!EnableOrNot)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MouseWorldPos, Vector2.zero);

            HitCollider = hit.collider;

            if (!HitCollider)
            {
                return;
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name);

                if (HitCollider.tag.Equals("furniture"))
                {
                    if (TimeOut)
                    {
                        return;
                    }
					HitCollider.gameObject.GetComponent<FurnitureController>().MouseDown();
                }
                else if (HitCollider.tag.Equals("BookMark"))
                {
                    if (HitCollider.transform.parent.GetComponent<RedController>())
                    {
                        HitCollider.transform.parent.GetComponent<RedController>().SetButtonStage(true);
                    }
                }
                else if (HitCollider.gameObject.name.Equals("StopButton"))
                {
                    HitCollider.gameObject.GetComponent<StopButton>().GetMouseDown();
                }
                else if (HitCollider.gameObject.name.Equals("PlayButton"))
                {
                    HitCollider.gameObject.GetComponent<PlayButton>().GetMouseDown();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!HitCollider)
            {
                return;
            }
            else
            {
                if (HitCollider.tag.Equals("furniture"))
                {
                    if (TimeOut)
                    {
                        return;
                    }
					HitCollider.gameObject.GetComponent<FurnitureController>().MouseUp();
                }
                else if (HitCollider.tag.Equals("BookMark"))
                {
                    if (HitCollider.transform.parent.GetComponent<RedController>())
                    {
                        HitCollider.transform.parent.GetComponent<RedController>().SetButtonStage(false);
                    }
                }
                else if (HitCollider.gameObject.name.Equals("StopButton"))
                {
                    HitCollider.gameObject.GetComponent<StopButton>().GetMouseUp();
                }
                else if (HitCollider.gameObject.name.Equals("PlayButton"))
                {
                    HitCollider.gameObject.GetComponent<PlayButton>().GetMouseUp();
                }
				else if (HitCollider.tag.Equals("Collect"))
				{
					HitCollider.transform.parent.GetComponent<ItemBarController>().ShowWhichItem(HitCollider.gameObject.name);
				}
            }

            HitCollider = null;
        }


    }
}
