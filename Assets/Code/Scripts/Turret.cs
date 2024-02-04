using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private Transform turretRotationPoint; //Punto de rotacion de la torreta
   [SerializeField] private LayerMask enemyMask;  //Esto especifica qué objetos se considerarán como enemigos.
   
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private Transform firingPoint; //La localización desde la que se dispara una bala 


   [Header("Attribute")]
   [SerializeField] private float targetingRange = 3f; //Rango maximo torreta-enemigo
   [SerializeField] private float rotationSpeed = 200f;
   [SerializeField] private float bps = 1f;  //Bulles per second

   private Transform  target;

   private float timeUntilFire;


   private void Update() {
    if (target == null) {
        FindTarget();
        return;         //Si no encuentra un enemigo pues nada
    }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f/bps) { //Verifica si ha pasado suficiente tiempo para que la torreta haga otro disparo
                Shoot();
                timeUntilFire = 0f; 
            }
        }
   }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); //default rotacion
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); 
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        //Esto va a coger todos los hits que necesitamos, es decir, los enemigos que tiene en radio
        //centro del circulo (la torreta), radio del circulo, Vector2 direction, ángulo inicial del círculo de lanzamiento, int layerMask
        //dibuja rayos circulares alrededor de la posición de la torreta para buscar objetos de la capa indicada en ese rango.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
        0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;  //Esto va a poner como objetivo el primer objeto detectado en el rango
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() { //Girar la torreta hacia el enemigo
        //la multiplicacion es para obtenerlo en radianes el angulo y el -90  grados pq sino apuntaba en la direccion contraria
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f; 
       
       //Necesitamos esta función para obtener el Quaternion que representa esa rotación
       Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle)); //X=0, Y=0 pq solo estamos rotando en el zed axis
         //turretRotationPoint.rotation = targetRotation;
         turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); //Rotacion smoother
    }


}
