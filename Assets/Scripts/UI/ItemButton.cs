    using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public TweenScroll scrollPanel;
    public RectTransform itemPanel;
    public void ScrollPanelCall()
    {
        scrollPanel.SetTarget(itemPanel);
    } 
}
