using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour, LateInitializable
{
    private RewardButton myButton;
    [SerializeField] RectTransform LinePrefab;
    [SerializeField] Transform LineParent;
    
    public void LateInit()
    {
        myButton = GetComponent<RewardButton>();

        print("my position: " + transform.position + " and I am " + name + " and my local position " + transform.localPosition + " and my anchored position " + GetComponent<RectTransform>().anchoredPosition);

        if (myButton.Parent != null)
        {
            RectTransform myRect = GetComponent<RectTransform>();
            RectTransform parentRect = myButton.Parent.GetComponent<RectTransform>();

            //so, first find the y distance
            float dy = parentRect.anchoredPosition.y - myRect.anchoredPosition.y;
            float dx = parentRect.anchoredPosition.x - myRect.anchoredPosition.x;

            RectTransform seg1 = Instantiate(LinePrefab, LineParent);
            seg1.anchoredPosition = new Vector3(myRect.anchoredPosition.x, myRect.anchoredPosition.y + (dy / 2));
            seg1.localScale = new Vector3(10, Mathf.Abs(dy));

            RectTransform seg2 = Instantiate(LinePrefab, LineParent);
            seg2.anchoredPosition = new Vector3(myRect.anchoredPosition.x + (dx / 2), parentRect.anchoredPosition.y);
            seg2.localScale = new Vector3(Mathf.Abs(dx), 10); 
        }
    }
}
