using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Chronos;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : ChronosBehaviour
{
    #region 公有变量
    [Header("Navigator")]
    public float delay = 5;

    // The center of the potential destinations area
    public Vector3 areaCenter;

    // The size of the potential destinations area
    public Vector3 areaSize;

    [Header("Move")]
    public float viewRadius = 10.0f;//视野最远的距离
    public float viewAngleStep = 40;//射线数量
    public float bulletSpeed = 100.0f;

    public float fireInterval = 1.0f;
    public float moveSpeed = 1.0f;          // 移动速度
    public float turnSpeed = 3.0f;          // 转身速度
    public float maxChaseDist = 11.0f;      // 最大追击距离
    public float maxLeaveDist = 2.0f;      // 最大离开原位距离\
    [Header("Objects")]
    public GameObject bulletPrefab;         //子弹预制体
    public LayerMask playerLayer;

    public enum State
    {
        Idle,
        Attack,
        Back,
        Dead,
    }
    [Header("State")]
    public State state = State.Idle;
    [Header("Time Control")]
    public float speedDownTimeScale = 0.3f;     //减速程度
    public float speedDownTime = 3.0f;         //减速时间
    [Header("Material")]
    public Material m_material;
    #endregion


    #region 私有变量    
    float fireCd = 0;

    Vector3 basePosition;
    Quaternion baseDirection;
    CapsuleCollider m_collider;
    Animator m_animator;
    NavMeshAgent m_navMeshAgent;
    GameObject invader = null;
    private HealthController m_healthController;
    #endregion


    #region 生命周期函数
    private void Start()
    {

        basePosition = transform.position;
        baseDirection = transform.rotation;
        m_collider = GetComponent<CapsuleCollider>();
        m_animator = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_healthController = GetComponent<HealthController>();
    }
    private void Update()
    {
        checkDead();
        switch (state)
        {
            case State.Dead:
                return;

            case State.Idle:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, baseDirection, turnSpeed);
                break;

            case State.Attack:
                if (invader != null)
                {
                    if (Vector3.Distance(invader.transform.position, transform.position) > maxChaseDist)
                    {
                        state = State.Back;
                        return;
                    }
                    if (Vector3.Distance(basePosition, transform.position) > maxLeaveDist)
                    {
                        state = State.Back;
                        return;
                    }
                    changeDestination(invader.transform.position);
                    Fire();
                }
                break;

            case State.Back:
                if (IsInPosition(basePosition))
                {
                    state = State.Idle;
                    m_animator.SetBool("isIdle",true);
                    return;
                }
                changeDestination(basePosition);

                break;
        }
        if(state != State.Dead)
            DrawFieldOfView();
    }
    // Draw the area gizmos for easy manipulation
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
    #endregion


    #region 辅助函数
    void checkDead(){
        if(m_healthController.isDead){
            Invoke("Die",0.1f);
        }
    }
    void Die()
    {
        state = State.Dead;
        m_animator.enabled = false;
        m_navMeshAgent.enabled = false;
        m_collider.enabled =false;
        time.enabled = false;
        GetComponent<EnemyGlowing>().SetNormal();
        GetComponent<EnemyGlowing>().enabled =false;
        GetComponent<EnemyRagdoll>().Ragdoll();

    }

    void DrawFieldOfView()          //画出视野，调试用
    {
        Vector3 forward_left = Quaternion.Euler(0, -45, 0) * transform.forward * viewRadius;
        for (int i = 0; i < viewAngleStep; i++)
        {
            Vector3 v = Quaternion.Euler(0, (90.0f / viewAngleStep) * i, 0) * forward_left;
            Vector3 ray_start = transform.TransformPoint(m_collider.center);

            Ray ray = new Ray(ray_start, v);

            RaycastHit hitt = new RaycastHit();
            Physics.Raycast(ray, out hitt, playerLayer);
            Vector3 ray_end = ray_start + v;
            if (hitt.transform != null)
            {
                ray_end = hitt.point;
            }

            Debug.DrawLine(ray_start, ray_end, Color.red);

            if (hitt.transform != null && hitt.transform.tag == "Player")
            {
                OnEnemySpooted(hitt.transform.gameObject);
            }
        }
    }
    void OnEnemySpooted(GameObject enemy)   //发现玩家
    {
        m_animator.SetBool("isIdle",false);
        state = State.Attack;
        invader = enemy;
    }
    void Fire()     //射击
    {
        if (fireCd > Time.time)
        {
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, transform.position + m_collider.center +
             transform.forward, transform.rotation);
        bullet.transform.parent = null;
        fireCd = Time.time + fireInterval;
    }
    bool IsInPosition(Vector3 pos)      //是否在pos位置
    {
        Vector3 v = pos - transform.position;
        v.y = 0;
        return v.magnitude < 0.05f;
    }
    void changeDestination(Vector3 destination) //改变目的地
    {
        time.Do(true, new ChangeDestinationOccurrence(m_navMeshAgent, destination));
    }
    public void RecoverTimeScale()      //恢复正常速度
    {
        _RecoverSpeed();
    }
    #endregion

    #region 公有函数
    public void SpeedDown()     //减速
    {
        _SpeedDown(speedDownTimeScale);
        Debug.Log("SpeedDown!");

    }
    public void SpeedDownForAWhile()    //减速一段时间
    {
        _SpeedDown(speedDownTimeScale);
        Debug.Log("SpeedDown For A While!");
        Invoke("RevocerTimeScale", speedDownTime);

    }
    #endregion
}
