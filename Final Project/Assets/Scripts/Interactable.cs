using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Actions[] actions;
    [SerializeField] private float distancePosition = 1f;
    
    public Vector3 InteractPosition()
    {
        return transform.position + transform.forward * distancePosition;
    }
    public void Interact(PlayerScript player)
    {
        Debug.Log(gameObject.name+ " Clicked by Player");

        StartCoroutine(waitforPlayerArriving(player));
    }

    IEnumerator waitforPlayerArriving(PlayerScript player)
    {
        while (!player.CheckIfArrived())
        {
            yield return null;
        }
        
        //it will the code below when the player arrives
        Debug.Log("Player arrived");
 
        player.SetDirection(transform.position);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }
    }
}
