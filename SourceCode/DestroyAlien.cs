using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAlien : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)//if health reaches 0 execute Die funtion
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);//destroy alien
        FindObjectOfType<AudioController>().Play("Destroyed");
    }
}
