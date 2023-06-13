using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ColorController : MonoBehaviour
{
    [System.Serializable]
    public class ColorUnityEvent : UnityEvent<Color>
    {

    }

    private Color _currentColor;
    public Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            // Event here
            if (OnColorChanged.GetPersistentEventCount() > 0)
            {
                OnColorChanged.Invoke(value);
            }
        }
    }

    [SerializeField] private Color InactiveColor;
    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color HoveredColor;
    [SerializeField] private Color SelectedColor;

    public ColorUnityEvent OnColorChanged;

    public void OnInactiveEvent(BaseEventData e)
    {
        CurrentColor = InactiveColor;
    }

    public void OnMouseOverEvent(BaseEventData e)
    {
        if (CurrentColor == SelectedColor) return;
        CurrentColor = HoveredColor;
    }

    public void SetMouseExitEvent(BaseEventData e)
    {
        if (CurrentColor == SelectedColor) return;
        CurrentColor = ActiveColor;
    }

    public void SetMouseClickEvent(BaseEventData e)
    {
        if (CurrentColor == SelectedColor)
        {
            CurrentColor = HoveredColor;
        }
        else
        {
            CurrentColor = SelectedColor;
        }
    }
}
