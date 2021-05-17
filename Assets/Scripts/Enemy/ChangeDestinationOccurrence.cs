using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using UnityEngine.AI;

public class ChangeDestinationOccurrence : Occurrence
{
    private NavMeshAgent agent;
    private Vector3 newDestination;
    private Vector3 previousDestination;

    public ChangeDestinationOccurrence(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.newDestination = destination;
    }

    // The action when time is going forward
    public override void Forward()
    {
        this.previousDestination = agent.destination;
        this.agent.SetDestination(this.newDestination);
    }

    // The action when time is going backward
    public override void Backward()
    {
        this.agent.SetDestination(this.previousDestination);
    }
}
