    using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public Scroll scrollPanel;
    public RectTransform itemPanel;
    public void ScrollPanelCall()
    {
        scrollPanel.SetPosition(itemPanel);
    } 
}
