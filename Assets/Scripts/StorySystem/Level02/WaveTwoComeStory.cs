using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTwoComeStory : StoryItemBase
{
    public EnemyWave enemyWave;
    protected override void executeEvent()
    {
        foreach(Enemy enemy in enemyWave.gameObject.GetComponentsInChildren<Enemy>(true)){
            enemy.gameObject.SetActive(true);
        }
        enemyWave.SetEnabled(true);
        Debug.Log(text);
    }
}
