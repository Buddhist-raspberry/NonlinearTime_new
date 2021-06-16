using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Chronos;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class Capsula : ChronosBehaviour
{
    public bool active = true;
    public int recoverValue = 30;

    protected RigidbodyTimeline3D rb;
    protected Rigidbody m_rb;
    protected Collider m_collider;
    protected Renderer m_renderer;
    protected PlayerController playerController;
     //药丸类型
    public enum CapsulaType { HP,AccMP,DecMP };
    public CapsulaType type;
    [Space]
    [Header("Capsula Settings")]
    public float reloadTime = .3f;
    public float damagePerVelocity = 5f;
    void Start()
    {   
        rb = time.rigidbody;
        m_rb = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
        m_renderer = GetComponent<Renderer>();
        playerController = PlayerController.instance;

    }
    // public void setRecoverValue(int value) {    
    //     this.recoverValue = value;
    //     Debug.Log("Capsula Value: " + this.recoverValue);
    // }
    public void Pickup()        //捡起武器
    {
        Debug.Log("Pick Up Capsula!!!!!");
        Debug.Log(type);
        switch (type)
        {
            case CapsulaType.HP:
                PlayerProperty.instance.recoverHP(recoverValue);
                break;
            case CapsulaType.AccMP:
                PlayerProperty.instance.recoverAccMP(recoverValue);
                break;
            case CapsulaType.DecMP:
                PlayerProperty.instance.recoverDecMP(recoverValue);
                break;
            default:
                Debug.Log("无效");
                break;
            }
        transform.parent = playerController.weaponHolder;
        transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
        Invoke("destroySelf",0.05f);
    }
    void destroySelf(){
        GameObject.Destroy(gameObject);
    }
}
