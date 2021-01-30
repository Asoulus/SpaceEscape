using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private float _delayTime = 5f;

    [SerializeField]
    private LevelLoader _loader = null;

    void Start()
    {
        StartCoroutine(GoToMenu());
    }
    
    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(_delayTime);
        _loader.LoadLevel("Menu");
    }
}
