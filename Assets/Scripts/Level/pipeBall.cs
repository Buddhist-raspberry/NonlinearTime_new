using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeBall : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    void Update() {
        if(transform.position.y<bottom.position.y){
            Vector3 pos = transform.position;
            pos.y = top.position.y;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
