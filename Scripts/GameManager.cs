using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Numbers")]
    public int coins;
    [Header("UI")]
    public Text coinsText;
    public int[] price;
    [Header("EnemySpawn")]
    public GameObject[] enemy;
    public Transform[] spawnPos;
    [Header("Enviroment")]
    public GameObject[] enviroment;
    public Vector3 center;
    public Vector3 size;
    [Header("NavMeshSurface")]
    public NavMeshSurface[] surface;

    Slime player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Slime>();
        coins = PlayerPrefs.GetInt("Coins");
        
        if(coins <= 0) {
            coins = 2000;
            PlayerPrefs.SetInt("Coins", coins);
        }
        StartCoroutine(EnemySpawn());
        EnviromentSpawn();
        UpadteNavmesh();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + coins.ToString();
    }

    public void UpadteNavmesh() {
        for (int i = 0; i < surface.Length; i++) 
        {
            surface[i].BuildNavMesh();
        }
    }

    public IEnumerator EnemySpawn() {
        while(true) {
            yield return new WaitForSeconds(5f);
            for(int i = 0; i < spawnPos.Length; i++) {
                int random = Random.Range(0, enemy.Length);
                Instantiate(enemy[random], spawnPos[i].position, Quaternion.identity);
            }
        }
    }

    public void EnviromentSpawn() {
        int envir = Random.Range(0, enviroment.Length);
        for(int i = 0; i < enviroment.Length; i++) {
            Vector3 newPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(enviroment[i], newPos, Quaternion.Euler(0, Random.Range(0, 180), 0));
        }
    }

    public void AddDamage(float damage) {
        if(coins >= price[0]) {
            player.damage += damage;
            coins -= price[0];
            PlayerPrefs.SetInt("Coins", coins);
        }
        else return;
    }

    public void AddHealth(float health) {
        if(coins >= price[1]) {
            player.startHP += health;
            coins -= price[1];
            PlayerPrefs.SetInt("Coins", coins);
        }
        else return;
    }

    public void AddRadius(float radius) {
        if(coins >= price[2]) {
            player.radius += radius;
            coins -= price[2];
            PlayerPrefs.SetInt("Coins", coins);
        }
        else return;
    }

    public void AddTimer(float timer) {
        if(coins >= price[3]) {
            player.newTimer -= timer;
            coins -= price[3];
            PlayerPrefs.SetInt("Coins", coins);
        }
        else return;
    }

    public void EliminateAll() {
        if(coins >= price[4]) {
            coins -= price[4];
            GameObject[] enemyes = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemyes.Length; i++) {
                Destroy(enemyes[i]);
            }
        }
        else return;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() {
        Application.Quit();
    }
}
