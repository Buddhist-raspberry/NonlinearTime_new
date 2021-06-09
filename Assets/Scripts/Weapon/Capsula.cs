using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Chronos;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public abstract class Capsula : ChronosBehaviour
{
    public bool active = true;
    public int recoverValue = 0;

    protected RigidbodyTimeline3D rb;
    protected Rigidbody m_rb;
    protected Collider m_collider;
    protected Renderer m_renderer;
    protected PlayerController playerController;

    public enum CapsulaType      //药丸类型
    {
        RED
    };
    public CapsulaType capsulaType { get; protected set; }
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

        Init();
    }
    protected abstract void Init();
    public void setRecoverValue(int value) {
        this.recoverValue = value;
        Debug.Log("Capsula Value: " + this.recoverValue);
    }
    public void Pickup()        //捡起武器
    {
        // if (!active)
        //     return;
        Debug.Log("Pick Up Capsula!!!!!");
        PlayerProperty.instance.recoverHP(recoverValue);
        
        GameObject.Destroy(gameObject,2.0f);
        // playerController.weapon = this;
        // ChangeSettings();

        // transform.parent = playerController.weaponHolder;

        // transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
        // transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
    }
}
