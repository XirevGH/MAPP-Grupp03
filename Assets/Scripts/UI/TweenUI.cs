using System.Xml;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.3f);
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setOnComplete(DisableElement);
    }

    private void DisableElement()
    {
        gameObject.SetActive(false);
    }
}
