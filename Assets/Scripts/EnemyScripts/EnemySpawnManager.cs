using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : NetworkBehaviour
{
    [SerializeField] private float secondsBetweenSpawn = 10;
    
    private float timer;

    [SerializeField] private GameObject enemyPrefab;
    

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            timer += Time.deltaTime; 
            
            if (timer > secondsBetweenSpawn)
            {
                print("SPAWN!!!");

                NetworkObject Enemy = Instantiate(enemyPrefab).GetComponent<NetworkObject>();

                Enemy.gameObject.transform.position = new Vector2(Random.Range(120, -120), Random.Range(60, -60));
                
                Enemy.Spawn();

                timer = 0;
            }
        }
    }
}
