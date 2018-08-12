using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width      = 10f;
    public float height     = 5f;
    public float spawnDelay = 0.5f;

    public float speed = 5f;
    bool movingLeft = true;
    float xmin;
    float xmax;
    // Use this for initialization
    void Start () {

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint( new Vector3(0, 0, distance) );
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftMost.x;
        xmax = rightMost.x;
        SpawanEnemiesUntillFull();
    }

    private void SpawanEnemies() {
        foreach (Transform child in transform)
        {
            GameObject Enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.parent = child;
        }
    }

    private void SpawanEnemiesUntillFull() {
        Transform freePosition = NextFreePosition();
        if (freePosition){
            GameObject Enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.parent = freePosition;
            Invoke("SpawanEnemiesUntillFull", spawnDelay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3( width, height, 0 ) );
    }

    // Update is called once per frame
    void Update () {

        if ( movingLeft ) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float rightEdgeOfspawner = transform.position.x + (0.5f * width);
        float leftEdgeOfspawner = transform.position.x - (0.5f * width);

        if (leftEdgeOfspawner < xmin) {
            movingLeft = false;
        } else if (rightEdgeOfspawner > xmax) {
            movingLeft = true;
        }

        if( AllMembersAreDead() ){
            SpawanEnemiesUntillFull();
        }

    }

    Transform NextFreePosition() {
        foreach (Transform childCountProperty in transform)
        {
            if (childCountProperty.childCount == 0)
            {
                return childCountProperty;
            }
        }
        return null;
    }

    bool AllMembersAreDead() {
        foreach (Transform childCountProperty in transform) {
            if (childCountProperty.childCount > 0 ) {
                return false;
            }
        }
        return true;
    }

}
