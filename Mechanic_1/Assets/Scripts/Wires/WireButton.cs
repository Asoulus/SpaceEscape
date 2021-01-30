using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WireButton : MonoBehaviour
{
    [SerializeField]
    private Wire _myWire = null;

    [SerializeField]
    private int currentSignal = 0;

    [Header("References")]
    [SerializeField]
    private GameObject _zeroGO = null;
    [SerializeField]
    private GameObject _oneGO = null;

    [SerializeField]
    private AudioSource _sound = null;

    private void Start()
    {
        SendSignal();
    }

    private void OnMouseDown()
    {
        if (currentSignal == 0)
        {
            currentSignal = 1;
        }
        else
        {
            currentSignal = 0;
        }

        _sound.Play();

        SendSignal();
        _myWire.ResetPosition();
    }

    private void SendSignal()
    {
        if (currentSignal == 0)
        {
            _zeroGO.SetActive(true);
            _oneGO.SetActive(false);
        }
        else
        {
            _zeroGO.SetActive(false);
            _oneGO.SetActive(true);
        }

        _myWire.Signal = currentSignal;
    }
}
