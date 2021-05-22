using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : HealthController
{
    public static PlayerProperty instance { get; protected set; }
    
    public int maxHP = 200;
    public int maxMP = 100;
    public int maxAddMP = 100;
    public int maxDesMP = 100;
    int currentHP;
    int currentMP;
    int currentAddMP;
    int currentDesMP;
    // int timeRecoverHP = 6;
    // int timeRecoverMP = 3;
    // float currentHPTime = 3.0f;
    // float currentMPTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = maxHP;
        currentMP = maxMP;
        currentAddMP = maxAddMP;
        currentDesMP = maxDesMP;
        // InvokeRepeating("recoverHP",0,currentHPTime);
        // InvokeRepeating("recoverMP",0,currentMPTime);
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerHP();
    }
    
    IEnumerator ActionE(float time)
    {
        GlobalTimeController.instance.setAction(true);
        yield return new WaitForSecondsRealtime(.06f);
        GlobalTimeController.instance.setAction(false);
    }
    public void checkPlayerHP(){
        if(currentHP<=0){
            GameLogic.instance.Die();
        }
    }
    public int getPlayHP(){return currentHP;}
    public int getPlayMP(){return currentHP;}
    public void recoverHP(int value)
    {
        currentHP += value;
        currentHP = currentHP <=maxHP?currentHP:maxHP;
        Debug.Log("recover HP: "+currentHP);
    }
    public void recoverMP(int value)
    {
        currentMP += value;
        currentMP = currentMP <=maxMP?currentMP:maxMP;
        Debug.Log("recover MP: "+currentMP);
    }
    public void reduceHP(int value)
    {
        currentHP -= value;
        currentHP = currentHP >= 0?currentHP:0;
        Debug.Log("reduce HP: "+currentHP);
    }
    public void reduceMP(int value)
    {
        currentMP -= value;
        currentMP = currentMP >= 0?currentMP:0;
        Debug.Log("reduce MP: "+currentMP);
    }
}
