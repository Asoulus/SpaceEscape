using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    private WirePort[] myPorts;
    private int _outSignal = 5;
    private Wire _outWire = null;

    [SerializeField]
    private string _gateType = string.Empty;

    private Vector3 _initialPos;

    public Wire OutWire
    {
        get => _outWire;
    }

    void Start()
    {
        myPorts = new WirePort[2];
        AssignPorts();
        AssignOutWire();
        _initialPos = transform.position;
    }
   
    private void AssignOutWire()
    {
        _outWire = GetComponentInChildren<Wire>();
        _outWire.CanDrag = false;
    }

    private void AssignPorts()
    {
        WirePort[] tmpPorts = GetComponentsInChildren<WirePort>();

        for (int i = 0; i < tmpPorts.Length; i++)
        {
            myPorts[i] = tmpPorts[i]; 
        }
    }

    public void AssignOutSignal()
    {
        if (myPorts[0].Signal < 2 && myPorts[1].Signal < 2) //załozenie ze nie bedzie mniejsze od 0
        {
            GateLogic();
            _outWire.Signal = _outSignal;
            _outWire.CanDrag = true;
        }
        else
        {
            _outWire.Signal = 5;
            _outWire.CanDrag = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = _initialPos;
    }

    private void GateLogic()
    {
        switch (_gateType)
        {
            case "NOT":
                {
                    _outSignal = NOT();
                }
                break;
            case "AND":
                {
                    _outSignal = AND();                 
                }
                break;
            case "NAND":
                {
                    _outSignal = NAND();                 
                }
                break;
            case "OR":
                {
                    _outSignal = OR();                 
                }
                break;
            case "NOR":
                {
                    _outSignal = NOR();                 
                }
                break; 
            case "XOR":
                {
                    _outSignal = XOR();                 
                }
                break;
            case "XNOR":
                {
                    _outSignal = XNOR();                 
                }
                break;             
        }
    }

    private int NOT()
    {
        if (myPorts[0].Signal == 0)
        {
            return 1;
        }

        return 0;
    }

    private int AND()
    {
        if (myPorts[0].Signal == 1 && myPorts[1].Signal == 1) 
        {
            return 1;
        }

        return 0;
    }
    private int NAND()
    {
        if (myPorts[0].Signal == 1 && myPorts[1].Signal == 1) 
        {
            return 0;
        }

        return 1;
    }

    private int OR()
    {
        if (myPorts[0].Signal == 1 || myPorts[1].Signal == 1)
        {
            return 1;
        }

        return 0;
    } 

    private int NOR()
    {
        if (myPorts[0].Signal == 0 && myPorts[1].Signal == 0)
        {
            return 1;
        }

        return 0;
    }

    private int XOR()
    {
        if (myPorts[0].Signal != myPorts[1].Signal)
        {
            return 1;
        }

        return 0;
    }
    private int XNOR()
    {
        if (myPorts[0].Signal == myPorts[1].Signal)
        {
            return 1;
        }

        return 0;
    }
}
