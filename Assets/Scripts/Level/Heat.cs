using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            GameLogic.instance.Die();
            //HealthController playerHealth = col.gameObject.GetComponent<HealthController>();
            //playerHealth.ChangeHealth(-1*Mathf.FloorToInt(playerHealth.currentHealth)-1);
        }
    }
}
