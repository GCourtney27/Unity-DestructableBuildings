using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    #region Singleton
    public static GoalManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject goal;
}
