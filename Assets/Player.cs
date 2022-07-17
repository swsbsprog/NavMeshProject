using System;
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

        // 멈췄을때 가장 근처의 좀비에게 총알 발사.
        if(agentSpeed > 0 && ingFire == false)
        {
            StartCoroutine(FireBulletCo());
        }
    }

    public bool ingFire; // 발사중일땐 true
    public float fireTime = 0.5f;
    // 좀비 방향으로 바라보기
    // 이동 방향으로 미사일 발사.
    private IEnumerator FireBulletCo()
    {
        ingFire = true;
        // 1. 가장 가까운 좀비 찾기
        Zombie nearnestZombie = 
            Zombie.GetNearNestZombie(transform.position);

        if (nearnestZombie != null)
        {
            // 2. 가장 가까운 좀비로 바라보기
            transform.LookAt(nearnestZombie.transform);

            Destroy(nearnestZombie.gameObject);
        }

        // 3. 발사 애니메이션 재생.
        animator.SetTrigger("Fire");
        // 4. 실제 미사일 발사
        FireBullet();
        yield return new WaitForSeconds(fireTime);

        ingFire = false;
    }
    private void FireBullet()
    {
        //throw new NotImplementedException();
    }
}
