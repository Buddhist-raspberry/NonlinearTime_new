using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty : MonoBehaviour
{
    public int maxHP = 80;
    int currentHP;

    public int getPlayHP(){return currentHP;}
    public void reduceHP(int value)
    {
        currentHP -= value;
        currentHP = currentHP >= 0?currentHP:0;
        Debug.Log(currentHP);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
