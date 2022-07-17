using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int power = 10;

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

        zombie.OnDamage(power);
        Destroy(gameObject);

        Debug.LogWarning("todo:피격 이펙트 표시");
    }
}
