using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject projectile;
    public float procjecttileSpeed = 5f;
    public float firingRate = 0.2f;

    float xmin;
    float xmax;
    float padding = 0.5f;
    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        // is bit 0 1 0 is the left most point and 1 is the right most. 0.5
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3( 0, 0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    void fire() {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, procjecttileSpeed, 0f);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("fire", 0.000001f, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3( newX, transform.position.y, transform.position.z );
	}
}
