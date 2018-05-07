using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseButton : MonoBehaviour
{

    private Collider2D HitCollider = null;

    private bool TimeOut;

    void Start()
    {
        TimeOut = false;
    }

    public void SetTimeOut(bool flag)
    {
        TimeOut = flag;
    }

    void Update()
    {
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
            }

            HitCollider = null;
        }


    }
}
