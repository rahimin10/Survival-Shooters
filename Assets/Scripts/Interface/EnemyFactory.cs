using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField] public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag)
    {
        GameObject newenemy = Instantiate(enemyPrefab[tag]);
        return newenemy;
    }

}
