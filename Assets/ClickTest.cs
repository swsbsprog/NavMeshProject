using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickTest : MonoBehaviour
{
    public Transform pointer;
    public NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo)) // Physics물리 -> 충돌 영역 필요
            {
                print(hitInfo.point);
                //pointer.position = hitInfo.point;
                agent.destination = hitInfo.point;
            }
        }
    }
}
