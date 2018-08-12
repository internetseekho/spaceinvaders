using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviours : MonoBehaviour {

    public float health = 200f;
    public GameObject Projectile;
    public float projectileVelocity = 10f;
    public float shotingRate = 0.5f;

    private void Update(){
        float probability = Time.deltaTime * shotingRate;
        if ( Random.value < probability ) { 
            Fire();
        }
    }

    void Fire() {
        Vector3 projectilePosition = transform.position + new Vector3(0, -1f, 0);
        GameObject missile = Instantiate(Projectile, projectilePosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileVelocity, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if ( health <= 0 ) {
                Destroy(gameObject);
            }
        }
    }

}
