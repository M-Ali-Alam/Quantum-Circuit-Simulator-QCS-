using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup cg;
    [SerializeField]
    GameObject obj;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("OnBeginDrag");
        if (eventData.position.y > 850)
        {
            GameObject temp = Instantiate(obj, transform.position, Quaternion.identity);
            temp.transform.SetParent(transform.parent);
            temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            temp.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        cg.blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("EndHandler");
        Debug.Log("eventData.position.y = " + eventData.position.y);
        if (eventData.position.y> 740 || eventData.position.y < 640)
        {
            Debug.Log("Destroy this");
        }
        cg.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
}
