using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGameManager : MonoBehaviour
{
    #region Singleton
    public static FPSGameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public FPSGame FPSGame;
}
