using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDemo : MonoBehaviour
{

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            //cast a ray to get where the mouse is pointing at
            //投射光线以获得鼠标指向的位置
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            //stores the position where the ray hit.
            RaycastHit hit; 
            //if the raycast doesn't hit a wall.
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }

        }	
    }
}
