using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Bullet : ChronosBehaviour
{
    public float speed;
    private int harmHP = 8;
    public float destroyTime = 3.0f;
    void Start()
    {
        if (time.timeScale > 0) // Move only when time is going forward
        {
            time.rigidbody.velocity = transform.forward * speed;
        }
        Invoke("DestorySelf", destroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Debug.Log("已中弹");
            PlayerProperty.instance.reduceHP(harmHP);
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<HealthController>().ChangeHealth(-5);
            Destroy(gameObject);
        }
    }
    void DestorySelf()
    {
        GameObject.Destroy(gameObject);
    }
}
