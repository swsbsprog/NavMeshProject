using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    static public List<Zombie> Zombies = new();
    IEnumerator Start()
    {
        Zombies.Add(this);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        while (true)
        {
            agent.destination = Player.instance.transform.position;

            yield return null;

            animator.SetFloat("AgentSpeed", agent.velocity.magnitude);
        }
    }
    private void OnDestroy() => Zombies.Remove(this);
    static public Zombie GetNearNestZombie(Vector3 pos)
    {
        // 오름 -> 1 -> 2
        return Zombies.OrderBy(x =>
           Vector3.Distance(x.transform.position, pos))
            .FirstOrDefault();
    }
}
