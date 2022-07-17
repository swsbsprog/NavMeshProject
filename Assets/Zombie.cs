using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        while (true)
        {
            agent.destination = Player.instance.transform.position;

            yield return null;

            animator.SetFloat("AgentSpeed", agent.velocity.magnitude);
        }
    }
}
