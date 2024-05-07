using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemButton : MonoBehaviour
{
    public Scroll scrollPanel;
    public RectTransform itemPanel;
    public void ScrollPanelCall()
    {
        scrollPanel.SetPosition(itemPanel);
    } 
}
