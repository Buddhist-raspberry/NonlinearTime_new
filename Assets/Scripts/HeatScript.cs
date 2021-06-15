using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void onCollisionEneter(Collision col){
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            Debug.Log("Hit Player");
        }
    }
}
