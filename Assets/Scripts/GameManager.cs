using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjects;
    //적의 프리펩 배열과 생성 위치 배열 변수를 선언
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;
    // Start is called before the first frame update
    public int health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            //Spawn
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }

    //instantiate는 게임 도중에 적을 생성시 사용 따라 prefab에 저장되어 있어야 한다.
    //instantiate의 매개변수는 순차적으로 게임 오브젝트, 생성 위치, 회전값을 요구한다.
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        Instantiate(enemyObjects[ranEnemy], 
        spawnPoints[ranPoint].position, 
        spawnPoints[ranPoint].rotation);    
    }
}
