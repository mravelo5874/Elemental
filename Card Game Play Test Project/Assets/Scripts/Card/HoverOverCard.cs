using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOverCard : MonoBehaviour
{
    public float transformTime;
    public int sortOrder;
    private float currTime = 0f;
    private int CardDefaultPos;
    private int CardHoverPos;
    public Canvas canvas;
    public bool isHover = false;

    private void Start()
    {
        CardDefaultPos = Screen.height / 5 - Screen.height / 6;
        CardHoverPos = Screen.height / 3 - Screen.height / 9;
        canvas.overrideSorting = true;
    }

    public void SetCanvas(Canvas newCanvas)
    {
        canvas = newCanvas;
    }

    private bool IsHoverOverCard()
    {
        isHover = true;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject == this.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsAnyOtherCardBeingHovered()
    {
        //Debug.Log(this.transform.parent.parent.childCount);
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            var child = this.transform.parent.GetChild(i).GetComponent<HoverOverCard>();
            //Debug.Log(child);
            if (child != null)
            {
                if (child.isHover && this.transform.parent.GetChild(i) != this.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Update()
    {
        // check if mouse pointer is over card
        if (IsHoverOverCard() && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !IsAnyOtherCardBeingHovered())
        {
            //place card on the top layer
            sortOrder = transform.parent.childCount + 1;
            
            
            if (currTime <= transformTime && this.transform.localScale.x < 1.2f)
            {
                currTime += Time.deltaTime;
                this.transform.position = new Vector3(transform.position.x, Mathf.Lerp(CardDefaultPos, CardHoverPos, currTime / transformTime), transform.position.z);
                this.transform.localScale = new Vector3(Mathf.Lerp(1.0f, 1.2f, currTime / transformTime), Mathf.Lerp(1.0f, 1.2f, currTime / transformTime), transform.localScale.z);
            }
            else
            {
                currTime = 0f;
            }
        }
        else
        {
            //Reset card layer
            sortOrder = transform.GetSiblingIndex() + 1;
            isHover = false;

            if (currTime <= transformTime && this.transform.localScale.x > 1.0f)
            {
                currTime += Time.deltaTime;
                this.transform.position = new Vector3(transform.position.x, Mathf.Lerp(CardHoverPos, CardDefaultPos, currTime / transformTime), transform.position.z);
                this.transform.localScale = new Vector3(Mathf.Lerp(1.2f, 1.0f, currTime / transformTime), Mathf.Lerp(1.2f, 1.0f, currTime / transformTime), transform.localScale.z);
            }
            else
            {
                currTime = 0f;
            }
        }

        // set order to be rendered
        canvas.sortingOrder = sortOrder;
    }
}
