using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public static Transform itemBeingDragged;
    public static Transform itemDroppedOnSlot;
    
    bool isNumberPanel;

    Vector3 startPosition;

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("number" + Random.Range(0, 5));
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.parent.name.Equals("WheelPanel"))
        {
            isNumberPanel = false;
        }
        else
        {
            isNumberPanel = true;
            itemBeingDragged = transform;
            startPosition = transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isNumberPanel)
            transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
            itemDroppedOnSlot = transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isNumberPanel)
        {
            // check the area of dropped item
            if (itemDroppedOnSlot != null && itemDroppedOnSlot.parent.parent.name.Equals("WheelPanel"))
            {
                // checking the sprites which dragged and onDropped
                if (itemBeingDragged.GetComponent<Image>().sprite.ToString().Equals(itemDroppedOnSlot.GetComponent<Image>().sprite.ToString()))
                {
                    // change the sprite of ondropped item to ok
                    itemDroppedOnSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ok");
                    itemBeingDragged.GetComponent<Image>().sprite = Resources.Load<Sprite>("questionmark");

                    // create a coroutine for changing sprite
                    WaitForXSecAndPutImage(itemDroppedOnSlot, itemBeingDragged);

                    //reset the startTime
                    MainPanelUI.startTime = Time.time;

                }
                else // it doesnt match
                {
                    //  Debug.Log("doesnt match!");
                }
            }


            transform.position = startPosition;
            itemBeingDragged = null;
            itemDroppedOnSlot = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            
        }
        isNumberPanel = false;
    }


    void WaitForXSecAndPutImage(Transform dropped, Transform dragged)
    {
        StartCoroutine("WaitForXSecAndPutImageCoroutine", dropped);
        StartCoroutine("WaitForXSecAndPutImageCoroutine", dragged);
    }

    IEnumerator WaitForXSecAndPutImageCoroutine(Transform x)
    {

        yield return new WaitForSeconds(.5f);

        x.GetComponent<Image>().sprite = Resources.Load<Sprite>("number" + UnityEngine.Random.Range(0,5));

    }





}
 