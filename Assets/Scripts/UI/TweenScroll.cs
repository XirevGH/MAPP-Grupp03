using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TweenScroll : TweenUI
{
    public RectTransform pagesTransform;
    private RectTransform targetTransform;

    protected override void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0f).setOnComplete(Scroll);
    }

    private void Scroll()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        Canvas.ForceUpdateCanvases();
        Vector2 offset = (Vector2)scrollRect.transform.InverseTransformPoint(pagesTransform.position) - (Vector2)scrollRect.transform.InverseTransformPoint(targetTransform.position);
        LeanTween.moveLocalX(pagesTransform.gameObject, offset.x, 0f).setOnComplete(ZoomIn);
    }

    private void ZoomIn()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.3f);
    }
    public void SetTarget(RectTransform target)
    {
        targetTransform = target;
    }
}
