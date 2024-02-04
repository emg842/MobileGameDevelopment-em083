using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    public Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() //Independiente de los fotogramas    
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized; //entre 0 y 1
        rb.velocity = direction * bulletSpeed;
        
    }

    private void OnCollisionEnter2D(Collision2D other)  //Cuando la bala toca a un enemigo
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject); //Destruimos la bala
    }


}
