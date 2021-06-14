using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    Enemy[] enemies;
    HealthController[] enemyHealthControllers;
    bool allKilled = false;
    public bool defaultEnabled = false;
    public StoryItemBase afterEvent;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<Enemy>(true);
        enemyHealthControllers = GetComponentsInChildren<HealthController>(true);
        SetEnabled(defaultEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if(allKilled) return;
        allKilled = checkAllKilled();
        if(allKilled&&afterEvent){
            afterEvent.TriggerEvent();
        }
    }
    public void SetEnabled(bool isEnabled){
        foreach(Enemy enemy in enemies){
            enemy.SetEnabled(isEnabled);
        }
    }
    bool checkAllKilled(){
        foreach(HealthController enemy in enemyHealthControllers){
            if(!enemy.isDead){
                return false;
            }
        }
        return true;
    }
}
