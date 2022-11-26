using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScaling : MonoBehaviour
{
    [SerializeField]
    private GameObject startPivot = null;
    [SerializeField]
    private GameObject endPivot = null;

    private Vector3 _initialScale;

    // Start is called before the first frame update
    void Start()
    {
        _initialScale = this.transform.localScale;

        AdjustScale();
    }

    // Update is called once per frame
    void Update()
    {
        if (endPivot.transform.hasChanged)
        {
            AdjustScale();
        }
    }

    private void AdjustScale()
    {
        //new code for scale
        float distance = Vector3.Distance(startPivot.transform.position, endPivot.transform.position);
        transform.localScale = new Vector3(_initialScale.x, distance, _initialScale.z); 

        Vector3 middlePoint = (startPivot.transform.position + endPivot.transform.position) / 2f; //zmiana pozycji 
        transform.position = middlePoint;

        Vector3 rotationDir = (endPivot.transform.position - startPivot.transform.position); //zmiana rotacji
        transform.up = rotationDir;
        endPivot.transform.forward = rotationDir;
    }
}
