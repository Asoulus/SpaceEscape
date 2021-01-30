using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerReference : MonoBehaviour
{
    #region Singleton

    public static PlayerInputHandlerReference instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
}
