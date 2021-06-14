using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            Debug.Log("Hit Player");
        }
    }
}
