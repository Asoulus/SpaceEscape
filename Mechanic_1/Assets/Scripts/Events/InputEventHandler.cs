using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventHandler : MonoBehaviour
{
    public static InputEventHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onInteractionButtonPressed;
    public event Action onLeftMouseButtonPressed;
    public event Action onEscapeButtonPressed;
    public event Action onSpaceButtonPressed;
    public event Action onEnterButtonPressed;
    public event Action onResetButtonPressed;
    public event Action onArrowUpPressed;
    public event Action onArrowDownPressed;
    public event Action<int> onOneButtonPressed;
    public event Action<int> onTwoButtonPressed;
    public event Action<float> onScrollWheelUsed;

    public event Action onConsoleExit;

    public event Action<USB> onPlugUSBEvent;

    public void InteractionButtonPressed()
    {
        if (onInteractionButtonPressed != null)
        {
            onInteractionButtonPressed();
        }
    }
    
    public void LeftMouseButtonPressed()
    {
        if (onLeftMouseButtonPressed != null)
        {
            onLeftMouseButtonPressed();
        }
    }

    public void EscapeButtonPressed()
    {
        if (onEscapeButtonPressed != null)
        {
            onEscapeButtonPressed();
        }
    } 
    public void SpaceButtonPressed()
    {
        if (onSpaceButtonPressed != null)
        {
            onSpaceButtonPressed();
        }
    } 

    public void EnterButtonPressed()
    {
        if (onEnterButtonPressed != null)
        {
            onEnterButtonPressed();
        }
    }
    
    public void ResetButtonPressed()
    {
        if (onResetButtonPressed != null)
        {
            onResetButtonPressed();
        }
    } 
    public void ArrowUpPressed()
    {
        if (onArrowUpPressed != null)
        {
            onArrowUpPressed();
        }
    }
    public void ArrowDownPressed()
    {
        if (onArrowDownPressed != null)
        {
            onArrowDownPressed();
        }
    }
    
    public void OneButtonPressed(int selected)
    {
        if (onOneButtonPressed != null)
        {
            onOneButtonPressed(selected);
        }
    } 
    
    public void ScrollWheelUsed(float value)
    {
        if (onScrollWheelUsed != null)
        {
            onScrollWheelUsed(value);
        }
    }
    
    public void TwoButtonPressed(int selected)
    {
        if (onTwoButtonPressed != null)
        {
            onTwoButtonPressed(selected);
        }
    }
    
    public void ConsoleExit()
    {
        if (onConsoleExit != null)
        {
            onConsoleExit();
        }
    }

    public void PlugUsbEvent(USB usb)
    {
        if (onPlugUSBEvent != null)
        {
            onPlugUSBEvent(usb);
        }
    }


}
