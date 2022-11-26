using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFeedback : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _feedback = null;
    [SerializeField]
    private Text _info = null;

    private void Start()
    {
        MenuEventHandler.instance.onFeedback += Feedback;

        ToggleVisibility(false);


        //_feedback = this.gameObject.GetComponent<CanvasGroup>();
        //_info = _feedback.gameObject.GetComponentInChildren<Text>();      
    }

    private void Feedback(string text, float time)
    {
        ToggleVisibility(true);

        _info.text = text;

        StartCoroutine(HideFeedback(time));
    }
    
    private IEnumerator HideFeedback(float time)
    {
        yield return new WaitForSeconds(time);
        ToggleVisibility(false);
    }

    private void ToggleVisibility(bool value)
    {
        if (value)
        {
            _feedback.alpha = 1;
            _feedback.blocksRaycasts = value;
            _feedback.interactable = value;
        }
        else
        {
            _feedback.alpha = 0;
            _feedback.blocksRaycasts = value;
            _feedback.interactable = value;
        }
    }


    private void OnDisable()
    {
        MenuEventHandler.instance.onFeedback -= Feedback;

    }
}
