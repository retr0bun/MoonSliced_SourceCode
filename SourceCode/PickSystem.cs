using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSystem : MonoBehaviour
{
    public Gun gScript;
    public Rigidbody rb;
    public BoxCollider collider;
    public Transform player, gunHolder, camera;
    public AmmoCounter ammoC;

    public float pickRange;
    public float dropForwardRange, dropUpwardRange;

    public bool equipped;
    public static bool slotIsFull;

    private void Start()
    {
        if(!equipped)
        {
            gScript.enabled = false;
            rb.isKinematic = false;
            collider.isTrigger = false;
            ammoC.ammoText.enabled = false;
        }
        if(equipped)
        {
            gScript.enabled = true;
            rb.isKinematic = true;
            collider.isTrigger = true;
            slotIsFull = true;
            ammoC.ammoText.enabled = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickRange && Input.GetKeyDown(KeyCode.E) && !slotIsFull) PickWeapon();

        if (equipped && Input.GetKeyDown(KeyCode.Q)) DropWeapon();
    }

    private void PickWeapon()
    {
        FindObjectOfType<AudioController>().Play("PickUp");
        equipped = true;
        slotIsFull = true;

        transform.SetParent(gunHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        collider.isTrigger = true;

        gScript.enabled = true;
        ammoC.ammoText.enabled = true;
    }

    private void DropWeapon()
    {
        equipped = false;
        slotIsFull = false;
        

        transform.SetParent(null);

        rb.isKinematic = false;
        collider.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(camera.forward * dropForwardRange, ForceMode.Impulse);
        rb.AddForce(camera.up * dropUpwardRange, ForceMode.Impulse);

        gScript.enabled = false;
        ammoC.ammoText.enabled = false;
    }
}
