using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    static public Player instance;
    public NavMeshAgent agent;
    public float agentSpeed;
    public Animator animator;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Awake() => instance = this;
    private void OnDestroy() => instance = null;
    void Update()
    {   
        if (Input.GetMouseButtonDown(0)) // Input.GetKeyDown(KeyCode.Mouse0)동일
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
