using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Task_Patrol : BT_Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int currentWpIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;

    private bool waiting = false;

    public Task_Patrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Patrolling");

        if (waiting)
        {
            waitCounter += Time.deltaTime;

            if (waitCounter >= waitTime)
                waiting = false;
        }
        else
        {
            Transform wp = _waypoints[currentWpIndex];

            if(Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWpIndex = (currentWpIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, ProtoAI.Speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
