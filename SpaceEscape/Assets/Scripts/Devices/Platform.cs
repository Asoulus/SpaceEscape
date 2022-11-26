using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerRotator = null;

    [SerializeField]
    private GameObject _player = null;

    private void Start()
    {
        _player = PlayerReference.instance.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerRotator.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerRotator.transform.parent = null;
        }
    }
}
