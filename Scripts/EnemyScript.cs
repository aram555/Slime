using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [Header("Number")]
    public float HP;
    private float startHP;
    public float damage;
    public int addCoins;
    [Header("Timer")]
    public float timer;
    public float newTimer;
    private bool isDamage;
    private Slime slime;
    [Header("HealthBar")]
    public Slider healthBar;
    Transform player;
    
    Camera cam;
    GameManager manager;
    NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        startHP = HP;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0) {
            addCoins = Random.Range(5, 10);
            manager.coins += addCoins;
            PlayerPrefs.SetInt("Coins", manager.coins);

            Destroy(this.gameObject);
        }
        else {
            healthBar.GetComponent<Slider>().maxValue = startHP;
            healthBar.GetComponent<Slider>().value = HP;
        }

        if(isDamage) {
            navMesh.SetDestination(transform.position);
            timer -= Time.deltaTime;
            if(timer <= 0) {
                slime.Damage(damage);
                timer = newTimer;
            }
        }
        else {
            navMesh.SetDestination(player.position);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Player")) {
            isDamage = true;
            slime = other.collider.GetComponent<Slime>();
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.collider.CompareTag("Player")) isDamage = false;
    }


}
