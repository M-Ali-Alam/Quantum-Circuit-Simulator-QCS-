using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public char gate; // - when no gate


    void Awake()
    {
        gate = '-';
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null){
            // eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
            //eventData.pointerDrag.transform.position.y;

            int abc = eventData.pointerDrag.GetComponent<cal>().Count;
            if(abc == 0)
            {
                gate = 'X';
            }else if(abc == 1)
            {
                gate = 'Y';
            }else if(abc == 2)
            {
                gate = 'Z';
            } else if(abc == 3)
            {
                gate = 'H';
            }

        }

    }
}
