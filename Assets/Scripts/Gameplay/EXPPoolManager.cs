using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPoolManager : MonoBehaviour
{
    public static EXPPoolManager instance;
    public GameObject expPrefab;

    public Queue<EXP> expPool = new();

    public void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SpawnEXP(Vector3 toSpawnAt, float expAmt)
    {
        if(expPool.Count > 0)
        {
            EXP newEXP = expPool.Dequeue();
            newEXP.Setup(toSpawnAt, expAmt);
        }
        else
        {
            EXP newEXP = Instantiate(expPrefab, toSpawnAt, Quaternion.identity).GetComponent<EXP>();
            newEXP.Setup(toSpawnAt, expAmt);
        }
    }

    public void AddToPool(EXP toAdd)
    {
        expPool.Enqueue(toAdd);
    }


}
