using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithDrag : MonoBehaviour
{
    [SerializeField]
    private Camera _myCamera = null;

    private float _cameraDistance;

    [SerializeField]
    private AudioSource _sound = null;

    

    void Start()
    {
        transform.hasChanged = false;
        if (_myCamera != null)
        {
            _cameraDistance = _myCamera.WorldToScreenPoint(transform.position).z;
        }
        else
        {
            //TODO remove else
            Debug.LogError("Nie ma przypisanej kamery");
        }

    }

    private void OnMouseDown()
    {
        _sound.Play();
    }

    private void OnMouseUp()
    {
        transform.hasChanged = false;
    }

    private void OnMouseDrag()
    {
        //move wire
       
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraDistance);

        Vector3 mouseWorldPos = _myCamera.ScreenToWorldPoint(mouseScreenPos);

        transform.position = mouseWorldPos;
    }
}
