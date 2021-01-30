using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.25f;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        transform.Rotate(0, 50 * _speed * Time.deltaTime, 0); 
    }
}
