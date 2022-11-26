using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    #region Singleton

    public static PlayerReference instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
}
