using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetEnabled(bool isEnabled){
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        foreach(Enemy enemy in enemies){
            enemy.SetEnabled(isEnabled);
        }
    }
}
