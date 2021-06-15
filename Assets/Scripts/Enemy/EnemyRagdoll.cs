using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    public Transform skeleton;
    public float fallForce = 20.0f;
    Rigidbody[] rigidbodies;
    CharacterJoint[] characterJoints;
    Collider[] colliders;
    bool isRagdoll = false;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbodies =  skeleton.GetComponentsInChildren<Rigidbody>();
        characterJoints = skeleton.GetComponentsInChildren<CharacterJoint>();
        colliders = skeleton.GetComponentsInChildren<Collider>();
    }

    void Start(){
        DisRagDoll();
    }
    void Update() {
        if(isRagdoll){
            foreach(Rigidbody rigidbody in rigidbodies){
                if(rigidbody.velocity.magnitude<0.1f){
                    rigidbody.isKinematic = true;
                }
            }
        }
    }
    public void DisRagDoll(){
        isRagdoll = false;
        foreach(Collider collider in colliders){
            collider.enabled = false;
        }
        foreach(Rigidbody rigidbody in rigidbodies){
            rigidbody.isKinematic = true;
        }

    }
    void setIsRagdoll(){
        isRagdoll = true;
    }
    public void Ragdoll()
    {
        foreach(Collider collider in colliders){
            collider.enabled = true;
        }
        foreach(Rigidbody rigidbody in rigidbodies){
            rigidbody.isKinematic = false;
            rigidbody.velocity = Vector3.zero;
            if(rigidbody.transform.name == "Head"){
                rigidbody.AddForce(-transform.forward*fallForce);
            }
        }
        foreach(CharacterJoint joint in characterJoints){
            joint.enableProjection = true;
        }   
        Invoke("setIsRagdoll",5.0f);
    }
}
