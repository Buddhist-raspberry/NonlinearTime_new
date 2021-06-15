using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public GameObject introduceCaption;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            introduceCaption.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Player")){
            introduceCaption.SetActive(false);
        }
    }
}
