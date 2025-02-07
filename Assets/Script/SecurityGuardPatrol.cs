using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float waitMinTime = 2f;
    public float waitMaxTime = 5f;
    public float speed = 2f;

    private Transform targetPoint;
    private bool movingToA = true;
    private NavMeshAgent agent;
    private bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        targetPoint = pointB;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        agent.SetDestination(targetPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndSwitchTarget() );
        }
        
    }
    IEnumerator WaitAndSwitchTarget()
    {
        isWaiting = true;
        float waitTime = Random.Range(waitMinTime, waitMaxTime);
        Debug.Log(waitTime);
        yield return new WaitForSeconds( waitTime );

        movingToA = !movingToA;
        targetPoint = movingToA ? pointA : pointB;

        MoveToTarget() ;

        isWaiting = false;
    }
}
