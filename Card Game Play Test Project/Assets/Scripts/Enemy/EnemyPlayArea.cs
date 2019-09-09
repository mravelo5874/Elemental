using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayArea : MonoBehaviour
{
    public List<GameObject> enemyList;

    public void AddEnemy(GameObject enemy)
    {
        if (enemyList.Count <= 2)
        {
            var physicalEnemy = enemy.GetComponent<EnemyPrefab>().physicalEnemy;
            GameObject newEnemy = Instantiate(physicalEnemy, this.transform);
            enemyList.Add(newEnemy);
        }
        else
        {
            Debug.Log("Too many enemies in play.");
        }
        
    }

    public void DestroyEnemy(GameObject enemy, float animationTime)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Destroy(enemy, animationTime);
        }
    }

    public void FreezeRandomEnemy(int turns)
    {
        enemyList[Random.Range(0, enemyList.Count)].GetComponent<EnemyManager>().FreezeEnemy(turns);
    }

    public void ChangeAllEnemyBuffs(int vigor, int defence, int bramble, int parasite)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].GetComponent<EnemyBuffManager>().vigor += vigor;
            enemyList[i].GetComponent<EnemyBuffManager>().defence += defence;
            enemyList[i].GetComponent<EnemyBuffManager>().bramble += bramble;
            enemyList[i].GetComponent<EnemyBuffManager>().parasite += parasite;
        }
    }
}
