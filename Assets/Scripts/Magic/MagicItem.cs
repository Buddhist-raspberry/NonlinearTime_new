using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItem : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            col.gameObject.GetComponent<PlayerController>().AddMagic(1);
            GameObject.Destroy(gameObject);
        }
    }
}
