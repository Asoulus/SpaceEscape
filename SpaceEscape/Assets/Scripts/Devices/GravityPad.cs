using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPad : MonoBehaviour
{
    [SerializeField]
    private bool _isOn;

    public bool IsOn
    {
        get => _isOn;
        set => _isOn = value;
    }

    public Vector3 ReturnVector3()
    {
        return -transform.up;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
   
}
