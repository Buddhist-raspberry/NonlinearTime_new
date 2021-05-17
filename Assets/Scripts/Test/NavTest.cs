using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Chronos;

public class NavTest : ChronosBehaviour
{

    private NavMeshAgent m_agent;
    public List<Transform> destinations;
    public float changeTime = 10.0f;
    public Clock globalClock;
    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        // m_agent.SetDestination(destinations[0].position);
        time.Do(true,new ChangeDestinationOccurrence(m_agent,destinations[0].position));
        for(int i=1;i<destinations.Count;i++){
            time.Plan(changeTime*i,true,new ChangeDestinationOccurrence(m_agent,destinations[i].position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_agent.SetDestination(destinations[0].position);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.gameObject.tag == "Enemy"){
            globalClock.localTimeScale=1;
        }
    }
}
