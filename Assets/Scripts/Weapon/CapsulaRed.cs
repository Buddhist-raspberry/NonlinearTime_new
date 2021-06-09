using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulaRed : Capsula
{
    public int value = 6;
    protected Material originalMaterial;
    protected override void Init()
    {
        capsulaType = CapsulaType.RED;
        setRecoverValue(value);
    }
    // public void setRecoverValue(int value) {
    //     this.recoverValue = value;
    //     // Debug.Log(this.recoverValue);
    // }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     base.Hit(collision.gameObject);
    // }
}