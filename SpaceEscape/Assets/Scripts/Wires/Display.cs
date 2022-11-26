using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField]
    private WireBox _myWireBox = null;

    [SerializeField]
    private GameObject _zeroGO = null;

    [SerializeField]
    private GameObject _oneGO = null;

    private int _signal;
    

    private void Start()
    {
        _signal = _myWireBox.DesiredSignal;

        AdjustVisuals();
    }

    private void AdjustVisuals()
    {
        if (_signal == 0)
        {
            _zeroGO.SetActive(true);
            _oneGO.SetActive(false);
        }
        else
        {
            _zeroGO.SetActive(false);
            _oneGO.SetActive(true);
        }
    }
}
