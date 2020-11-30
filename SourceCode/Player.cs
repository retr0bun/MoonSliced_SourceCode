using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int playerDamage = 10;
    public int destroyTime = 20;

    public HealthBar viata;

    void Start()
    {
        currentHealth = maxHealth;//ammount of health you start with
        viata.SetMaxHealth(maxHealth);

    }
    void Update()
    {
        if (currentHealth <= 0) // scene reset
        {
            Die();
        }
        
        
        
    }
    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.tag == "enemyBullet")
        {
            currentHealth -= playerDamage;//sets the current health
            FindObjectOfType<AudioController>().Play("Damage");//to play sound
            viata.SetHeatlh(currentHealth);//set current health on the healthBar
        }
    }
    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);// scene reset
    }
}