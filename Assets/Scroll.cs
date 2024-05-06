using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    
    public void SetPosition(RectTransform target)
    {
        RectTransform contentRt = GetComponent<RectTransform>();
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        Canvas.ForceUpdateCanvases();
        Vector2 offset = (Vector2)scrollRect.transform.InverseTransformPoint(contentRt.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        Vector2 anchor = contentRt.anchoredPosition;
        anchor.x = offset.x;
        contentRt.anchoredPosition = anchor;
    }

}
