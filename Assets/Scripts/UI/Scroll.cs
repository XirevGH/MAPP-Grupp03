using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public RectTransform pagesTransform;

    public void SetPosition(RectTransform target)
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        Canvas.ForceUpdateCanvases();
        Vector2 offset = (Vector2)scrollRect.transform.InverseTransformPoint(pagesTransform.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        Vector2 anchor = pagesTransform.anchoredPosition;
        anchor.x = offset.x;
        pagesTransform.anchoredPosition = anchor;
    }
}
