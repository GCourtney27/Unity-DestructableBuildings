using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankManager : MonoBehaviour
{
    #region Singleton
    public static EnemyTankManager instance;

    //public static GameObject[] enemyTanks = GameManager.instance.game.m_enemyTanks;

    private void Awake()
    {
        instance = this;
        //for(int i = 0; i< enemyTanks.Length; i++)
        //{
        //    if(i == index)
        //    {
        //        return enemyTanks[i];
        //    }
        //}
        //return null;
    }
    #endregion

    public EnemyTank enemyTank;


}
