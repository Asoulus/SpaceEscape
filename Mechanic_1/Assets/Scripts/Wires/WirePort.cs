using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WirePort : MonoBehaviour
{
    private Gate _myGate;

    [SerializeField]
    private int _signal = 5;

    [SerializeField]
    private bool _isLastOutput = false;

    private List<Wire> _myWires = new List<Wire>();

    public int Signal
    {
        get => _signal;
    }


    private void Start()
    {
        _myGate = transform.parent.GetComponent<Gate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wire"))
        {
            _myWires.Add(other.GetComponent<Wire>());

            if (_myWires.Count == 1) 
            {
                _signal = other.GetComponent<Wire>().Signal;

                if (_myGate != null)
                {
                    _myGate.AssignOutSignal();
                }
            }                   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wire"))
        {
            _myWires.Remove(other.GetComponent<Wire>());

            if (_myWires.Count == 0) 
            {
                _signal = 5;

                if (!_isLastOutput)
                {
                    _myGate.AssignOutSignal();
                    _myGate.OutWire.ResetPosition();
                }
            }                    
        }
    }


}
