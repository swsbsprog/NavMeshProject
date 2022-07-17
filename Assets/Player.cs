using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public float agentSpeed;
    public Animator animator;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // 이동중에는 run애니메이션, 가만히있을땐 idle 애니메이션 재생
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo)) // Physics물리 -> 충돌 영역 필요
            {
                agent.destination = hitInfo.point;
            }
        }
        agentSpeed = agent.velocity.magnitude;
        animator.SetFloat("AgentSpeed", agentSpeed); //agentSpeed 0.1크면 run 아니면 idle
    }
}
