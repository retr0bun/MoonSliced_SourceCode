using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.4f; //to make the player bigger
    public float duration = 4f;//duratia powerUP-ului
     
    public GameObject pickupEffect;//effectul de particule

    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player") ) //check if something with the PLAYER tag has collided with it
        {
            StartCoroutine( PickUp(other) );
            
        }

    }

    IEnumerator PickUp(Collider player)
    {
        FindObjectOfType<AudioController>().Play("PickUp");
        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        //Apply effect to player
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        stats.speed *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;//disables MeshRenderer
        GetComponent<SphereCollider>().enabled = false;//disables BoxCollider

        //wait some seconds
        yield return new WaitForSeconds(duration);
        //reverse changes
        stats.speed /= multiplier;
        //destroy power up
        Destroy(gameObject);
    }
}
