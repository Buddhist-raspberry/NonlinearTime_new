using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            
            PlayerProperty playerHealth = col.gameObject.GetComponent<PlayerProperty>();
            playerHealth.reduceHP(3000);
        }
    }
}
