using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InputEventHandler.instance.InteractionButtonPressed();   //e         
        }

        if (Input.GetMouseButtonDown(0))
        {
            InputEventHandler.instance.LeftMouseButtonPressed();//left mouse
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputEventHandler.instance.EscapeButtonPressed();//esc
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputEventHandler.instance.SpaceButtonPressed();//space
        } 
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            InputEventHandler.instance.ResetButtonPressed();//R
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InputEventHandler.instance.OneButtonPressed(1);//1
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InputEventHandler.instance.TwoButtonPressed(2);//2
        }

        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            InputEventHandler.instance.ScrollWheelUsed(Input.GetAxis("Mouse ScrollWheel"));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputEventHandler.instance.ArrowUpPressed();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputEventHandler.instance.ArrowDownPressed();
        }
    }
}
