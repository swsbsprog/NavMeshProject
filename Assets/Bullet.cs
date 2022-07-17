using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        /// 성능상 더 좋다.
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.gameObject.GetComponent<Zombie>();
        if(zombie == null)
            return;

        zombie.state = Zombie.ZombieStateType.Die;
        Destroy(zombie.gameObject);
    }
}
