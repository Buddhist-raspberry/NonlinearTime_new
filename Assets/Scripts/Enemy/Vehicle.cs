using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Vehicle : ChronosBehaviour
{
    UnityEngine.AI.NavMeshAgent m_navMeshAgent;
    Transform curr_destionation;
    int cur_destination_index = 0;
    public Transform [] path;
    public bool isActive = false;
    public float damagePerVelocity = 5f;
    
    RigidbodyTimeline3D rb;
    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = time.rigidbody;
        if(path.Length>0){
            isActive = true;
            cur_destination_index = 0;
            changeDestination(path[0]);
        }
    }
    float distance2D(Vector3 p1, Vector3 p2 ){
        Vector2 p1_2d = new Vector2(p1.x,p1.z);
        Vector2 p2_2d = new Vector2(p2.x,p2.z);
        return Vector2.Distance(p1_2d,p2_2d);
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if(distance2D(transform.position,curr_destionation.position)<0.05f){
                Debug.Log("Get!");
                cur_destination_index++;
                if(cur_destination_index>=path.Length){
                    cur_destination_index = 0;
                }
            }
            changeDestination(path[cur_destination_index]);
        }

    }
    void changeDestination(Transform destination) //改变目的地
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
        curr_destionation = destination;
        time.Do(true, new ChangeDestinationOccurrence(m_navMeshAgent, destination.position));
    }

    void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    void Hit(GameObject hitObject)    //击中物体
    {
        if (hitObject.layer == LayerMask.NameToLayer("Player"))
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
            Debug.Log("Vehicle Cause Damage: " + damage);
            hitObject.GetComponent<PlayerProperty>().reduceHP(damage);

            return;
        }
    }
}
