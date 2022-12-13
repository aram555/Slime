using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [Header("Numbers")]
    public float speed;
    public float startHP;
    public float HP;
    public float damage;
    [Header("Timers")]
    public float timer;
    public float newTimer;
    [Header("Bullet")]
    public GameObject bullet;
    public Transform bulletPos;
    [Header("Enemyes")]
    public float radius;
    public Transform enemy;
    [Header("HealthBar")]
    public Slider healthBar;
    [Header("Joystick")]
    public Joystick joystick;
    [Header("Restart")]
    public GameObject retry;

    Camera cam;
    Rigidbody rb;
    NavMeshAgent navMesh;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        startHP = HP;
        rb = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update() {
        if(HP <= 0) {
            Destroy(this.gameObject);
            retry.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            healthBar.GetComponent<Slider>().maxValue = startHP;
            healthBar.GetComponent<Slider>().value = HP;
        }

        if(!enemy || (enemy && Vector3.Distance(transform.position, enemy.position) > radius)) FindEnemyes();
        else {
            Vector3 direction = enemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotationY, 10 * Time.fixedDeltaTime);

            timer -= Time.deltaTime;
            if(timer <= 0) {
                GameObject bul = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                Bullet b = bul.GetComponent<Bullet>();
                b.damage = this.damage;

                timer = newTimer;
            }
        }
    }

    private void FixedUpdate() {
        JoystickMove();
    }

    void JoystickMove() {
        float vertical = joystick.Vertical;
        float horizontal = joystick.Horizontal;

        rb.velocity = new Vector3(horizontal, 0, vertical) * speed;
    }

    void FindEnemyes() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for(int i = 0; i < colliders.Length; i++) {
            EnemyScript enemy = colliders[i].GetComponent<EnemyScript>();

            if(enemy) {
                this.enemy = enemy.transform;
            }
        }
    }

    public void Damage(float damage) {
        this.HP -= damage;
    }
}
