using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeSliderHandler : MonoBehaviour
{
    [SerializeField] private Item item;
    private Slider slider;

    public string method;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = (int) item.GetType().GetMethod(method).Invoke(item, null);
    }
}
