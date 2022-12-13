using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Numbers")]
    public float speed;
    public float damage;
    public float life;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);

        if(other.collider.CompareTag("Enemy")) {
            EnemyScript enemy = other.collider.GetComponent<EnemyScript>();
            enemy.HP -= damage;
        }
    }


}
