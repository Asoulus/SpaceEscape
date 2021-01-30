using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject _selector;

    private AudioSource _selectSound; 

    private void Start()
    {
        _selector = GameObject.Find("Select_Sound");

        _selectSound = _selector.GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _selectSound.Play();
    }
}
