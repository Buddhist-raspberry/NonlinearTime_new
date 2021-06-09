using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Chronos;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public abstract class Weapon : ChronosBehaviour
{
    [Header("Bools")]
    public bool active = true;
    public bool reloading;
    public float speedUpTimeScale = 3.0f;      //加速程度
    public float speedUpTime = 3.0f;            //减速时间

    public float throwPower = 500.0f;           //抛掷力度
    public int useMP = 0;

    protected RigidbodyTimeline3D rb;
    protected Rigidbody m_rb;
    protected Collider m_collider;
    protected Renderer m_renderer;
    protected PlayerController playerController;

    public enum WeaponType      //武器类型
    {
        GUN, EGG
    };
    public WeaponType weaponType { get; protected set; }
    [Space]
    [Header("Weapon Settings")]
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
        ChangeSettings();
    }

    void ChangeSettings()       //根据武器是否在玩家手上，改变rigidbodyhecollider设置
    {
        if (transform.parent != null)
            return;

        rb.isKinematic = (playerController.weapon == this) ? true : false;
        m_rb.useGravity = (playerController.weapon == this) ? false : true;
        m_collider.isTrigger = (playerController.weapon == this);
    }

    protected abstract void Init();
    public void setUseMP(int useMP) {
        this.useMP = useMP;
        Debug.Log(this.useMP);
    }
    public void Throw()     //抛掷武器
    {
        if (playerController.weapon == this)
            StartCoroutine(Reload());
        Sequence s = DOTween.Sequence();
        s.OnUpdate(() => s.timeScale = time.timeScale);
        s.AppendCallback(() => transform.parent = null);
        s.AppendCallback(() => ChangeSettings());
        s.AppendCallback(() => rb.velocity = Vector3.zero);
        Vector3 throwDirection = Camera.main.transform.forward;
        s.AppendCallback(() => rb.velocity = throwDirection * throwPower);
        // 扔武器消耗体力MP
        PlayerProperty.instance.reduceMP(useMP);
    }

    public void Pickup()        //捡起武器
    {
        if (!active)
            return;
        Debug.Log("PickUP");
        playerController.weapon = this;
        ChangeSettings();

        transform.parent = playerController.weaponHolder;

        transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
    }

    public void Release()       //放弃武器
    {
        active = true;
        transform.parent = null;
        rb.isKinematic = false;
        m_collider.isTrigger = false;

        rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);

    }

    //加速
    public virtual void SpeedUp()   //加速
    {
        _SpeedUp(speedUpTimeScale);
        Invoke("RecoverTimeScale", speedUpTime);
    }


    protected IEnumerator Reload()  //冷却
    {
        if (playerController.weapon != this)
            yield break;
        playerController.ReloadUI(reloadTime);
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    protected virtual void RecoverTimeScale()   //恢复正常速度
    {
        _RecoverSpeed();
    }
    public float getVelocity()
    {
        return rb.velocity.magnitude * time.timeScale;
    }

    protected void Hit(GameObject hitObject)    //击中物体
    {
        if (hitObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            float r_velocity = rb.velocity.magnitude;   //引擎内参数速度
            float eTimeScale = hitObject.GetComponent<Timeline>().timeScale;
            if (eTimeScale < 0)     //回溯敌人无法击中
                return;

            if (Mathf.Abs(eTimeScale - time.timeScale) <= 0.01)
                eTimeScale = time.timeScale;

            float velocity = r_velocity * time.timeScale / eTimeScale;
            
            //击中敌人计算伤害
            int damage = Mathf.FloorToInt(velocity * damagePerVelocity);
            Debug.Log("Cause Damage: " + damage);
            hitObject.GetComponent<EnemyProperty>().reduceHP(damage);

            return;
        }
    }

}