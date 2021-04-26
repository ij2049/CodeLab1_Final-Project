using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning;
    private Quaternion targetRot;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //0 is left click and 1 is right click 2 middle click
        if (Input.GetMouseButtonDown(0))
            OnClick();
        
        if (turning && transform.rotation != targetRot)
        {
            transform.rotation = Quaternion.Slerp
                (transform.rotation, targetRot, 15f * Time.deltaTime);
        }
    }

    void OnClick()
    {
        Debug.Log("Left Clicked!");
        
        //(collison detection 충돌감지)
        RaycastHit hit;
        
        //the camera shoot a ray to our scene
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        //if hit something, out hit(if the ray hit, this will save on the hit variable^)
        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                //if our hit collider has tht component. If the material
                //or npcs we clicked haver interactable class attached to it
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    MovePlayer(interactable.InteractPosition());
                    interactable.Interact(this);
                }

                else
                {
                    MovePlayer(hit.point);
                }
                
            }
        }
    }

    //if the player arrived on the destination (ex: to the interactable)
    public bool CheckIfArrived()
    {
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
        
    }
    
    void MovePlayer(Vector3 targetPosition)
    {
        turning = false;
        
        agent.SetDestination(targetPosition);
    }

    public void SetDirection(Vector3 targetDirection)
    {
        turning = true;
        targetRot = Quaternion.LookRotation(targetDirection - transform.position);
    }
}
