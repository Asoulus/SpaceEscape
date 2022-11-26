using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemReference : MonoBehaviour
{
    #region Singleton

    public static EventSystemReference instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
}
