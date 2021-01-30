using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Lever : MonoBehaviour
{
    [SerializeField]
    private WireBox _myWireBox= null;

    private Quaternion _initialRotation;

    private void Start()
    {
        _initialRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        //TODO turn lever down
        if (_myWireBox.Using)
        {
            _myWireBox.SendSignalToDevices();
            //transform.rotation = Quaternion.Euler(0, 90, -40);
            LeanTween.rotateLocal(this.gameObject,new Vector3(0,-90,-35),0.15f);
            StartCoroutine(LeverDelay());
        }    
    }

    private IEnumerator LeverDelay()
    {
        yield return new WaitForSeconds(0.16f);
        //transform.rotation = _initialRotation;
        LeanTween.rotateLocal(this.gameObject, new Vector3(0, -90, 75), 0.15f);
    }
}
