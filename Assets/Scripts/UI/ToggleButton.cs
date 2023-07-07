using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    public UnityEvent<bool> ToggleButtonEvent;
    private Image toggleSprite;
    private Toggle toggleElement;
    private void Start()
    {
        toggleSprite= GetComponent<Image>();
        toggleElement= GetComponent<Toggle>();
    }
    public void OnToggleChange()
    {
        if(toggleElement.isOn)
        {
            ToggleButtonEvent?.Invoke(toggleElement.isOn);
            toggleSprite.sprite = onSprite;
            return;
        }
        toggleSprite.sprite = offSprite;
        ToggleButtonEvent?.Invoke(toggleElement.isOn);
    }
}
