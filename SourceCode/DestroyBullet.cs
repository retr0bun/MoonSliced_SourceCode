using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public int destroyTime = 3;

    void OnCollisionEnter(Collision _collision)
    {
        Destroy(gameObject, destroyTime);//destroy bullet after it's collided with something
    }
}
