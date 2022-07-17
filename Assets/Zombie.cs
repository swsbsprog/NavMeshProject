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
    public GameObject attackedEffectGo;
    internal void OnDamage(int damange)
    {
        hp -= damange;
        if (hp <= 0)
        {
            state = Zombie.ZombieStateType.Die;
            // todo: 죽는 모션 하고
            animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }

    internal void InstantiateDamageEffect(Transform bulletTransform)
    {
        InstantiateAttackedEffect();

        // 경직 -> 피격 모션, 뒤로 밀림.
        StartCoroutine(AttackedMotionPlayCo(0.1f));
    }


    [SerializeField] float attackedMoveBack = 0.1f;
    private IEnumerator AttackedMotionPlayCo(float stopTime)
    {
        animator.SetTrigger("Attacked");
        transform.position -= transform.forward * attackedMoveBack; // 살짝 뒤로밀림
        var originalSpeed = agent.speed;
        agent.speed = 0;
        yield return new WaitForSeconds(stopTime);
        agent.speed = originalSpeed;
    }

    private void InstantiateAttackedEffect()
    {
        var newEffect = Instantiate(attackedEffectGo);
        var pos = transform.position;
        pos.y = 1;
        newEffect.transform.position = pos;
        newEffect.transform.forward = transform.forward;
    }

    public ZombieStateType state;
    Animator animator;
    NavMeshAgent agent;

    IEnumerator Start()
    {
        Zombies.Add(this);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
