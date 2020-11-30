using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 20;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    public Camera fpsCam;
    public ParticleSystem particule;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;

    
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if(isReloading)
           return;
        
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;

        FindObjectOfType<AudioController>().Play("RELOAD");

        animator.SetBool("Reloading", true);
        
        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }


    void Start()
    {
      currentAmmo = maxAmmo;
    }

    void Shoot ()
    {
        currentAmmo--;
        
        particule.Play();

        //FindObjectOfType<AudioController>().Play("SHOOT SMG");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            DestroyAlien target = hit.transform.GetComponent<DestroyAlien>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
