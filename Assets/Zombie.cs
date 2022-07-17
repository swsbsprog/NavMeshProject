using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    static public List<Zombie> Zombies = new();
#if UNITY_EDITOR

    [UnityEditor.InitializeOnEnterPlayMode]
    static void OnEnterPlaymodeInEditor(UnityEditor.EnterPlayModeOptions options)
    {
        Zombies = new();
    }
#endif

    public enum ZombieStateType
    {
        Idle,
        Run,
        Die,
    }

    public int hp = 100;
    internal void OnDamage(int damange)
    {
        hp -= damange;
        if (hp <= 0)
        {
            state = Zombie.ZombieStateType.Die;
            Destroy(gameObject);
        }
    }

    public ZombieStateType state;
    IEnumerator Start()
    {
        Zombies.Add(this);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        state = ZombieStateType.Run;
        while (true)
        {
            if (state == ZombieStateType.Die)
                yield break;
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
