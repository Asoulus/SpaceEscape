using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Wire : MonoBehaviour
{
    [SerializeField]
    private Camera _myCamera = null;

    [SerializeField]
    private int _signal = 0;

    [SerializeField]
    private bool _canDrag = true;

    [SerializeField]
    private float _dragResponseTreshHold = 0.25f;

    private Vector3 _initialPos;


    private float _cameraDistance;
    private bool _connected;

    [SerializeField]
    private AudioSource _sound = null;


    [SerializeField]
    private GameObject _originalParent = null;

    public int Signal
    {
        get => _signal;
        set => _signal = value;
    }

    public bool CanDrag
    {
        get => _canDrag;
        set => _canDrag = value;
    }
    public bool Connected
    {
        get => _connected;
    }

    void Start()
    {
        _initialPos = transform.position;
        _cameraDistance = _myCamera.WorldToScreenPoint(transform.position).z;    
    }

    private void OnMouseDrag()
    {
        if (CanDrag)
        {         
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraDistance);

            Vector3 mouseWorldPos = _myCamera.ScreenToWorldPoint(mouseScreenPos);
            
            if (!_connected)
            {
                transform.position = mouseWorldPos;
            }
            else if (Vector3.Distance(transform.position, mouseWorldPos) > _dragResponseTreshHold)
            {
                _connected = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WirePort"))
        {
            _connected = true;
            transform.position = other.transform.position;
            transform.parent = other.gameObject.transform;

            _sound.Play();
        }
    }

    private void OnMouseUp()
    {
        if (!_connected)
        {
            ResetPosition();
        }
    }

    private void OnMouseDown()
    {
        _sound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WirePort"))
        {
            transform.parent = _originalParent.transform;
        }        
    }

    public void ResetPosition()
    {
        transform.position = _initialPos;
    }
}
