using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : HealthController
{
    public static PlayerProperty instance { get; protected set; }
    
    public int maxHP = 200;
    // public int maxMP = 100;
    public int maxAccMP = 100;
    public int maxDecMP = 100;
    int currentHP;
    // int currentMP;
    int currentAccMP;
    int currentDecMP;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = maxHP;
        // currentMP = maxMP;
        currentAccMP = maxAccMP;
        currentDecMP = maxDecMP;
        // InvokeRepeating("recoverHP",0,currentHPTime);
        // InvokeRepeating("recoverMP",0,currentMPTime);
    }

    // Update is called once per frame
    void Update()
    {
        // checkPlayerHP();
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
    public int getPlayAccMP(){return currentAccMP;}
    public int getPlayDecMP(){return currentDecMP;}
    // 回血
    public void recoverHP(int value)
    {
        currentHP += value;
        currentHP = currentHP <=maxHP?currentHP:maxHP;
        Debug.Log("recover HP: "+currentHP);
    }
    // public void recoverMP(int value)
    // {
    //     currentMP += value;
    //     currentMP = currentMP <=maxMP?currentMP:maxMP;
    //     Debug.Log("recover MP: "+currentMP);
    // }
    public void recoverAccMP(int value)
    {
        currentAccMP += value;
        currentAccMP = currentAccMP <=maxAccMP?currentAccMP:maxAccMP;
        Debug.Log("recover Acc MP: "+currentAccMP);
    }
    public void recoverDecMP(int value)
    {
        currentDecMP += value;
        currentDecMP = currentDecMP <=maxDecMP?currentDecMP:maxDecMP;
        Debug.Log("recover Dec MP: "+currentDecMP);
    }
    // 掉血
    public void reduceHP(int value)
    {
        currentHP -= value;
        currentHP = currentHP >= 0?currentHP:0;
        Debug.Log("reduce HP: "+currentHP);
        checkPlayerHP();
    }
    public void reduceAccMP(int value)
    {
        currentAccMP -= value;
        currentAccMP = currentAccMP >= 0?currentAccMP:0;
        Debug.Log("reduce Acc MP: "+currentAccMP);
    }
    public void reduceDecMP(int value)
    {
        currentDecMP -= value;
        currentDecMP = currentDecMP >= 0?currentDecMP:0;
        Debug.Log("reduce Dec MP: "+currentDecMP);
    }
}
