using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
        GameObject[] spawnerlist;
        private float spawnCoolDown;
        [SerializeField] float spawnRate = 2;


        public GameObject[] enemyPrefab;




        void Start()
        {
            spawnerlist = GameObject.FindGameObjectsWithTag("Spawn");

            
            
           

        }

        void Update()
        {

            CheckEnemy();
        }
        public void CheckEnemy()
        {

            spawnCoolDown -= Time.deltaTime;

            if (spawnCoolDown <= 0f)
            {
                SpawnRandomPosition();


            spawnCoolDown = spawnRate;
            }
        }


        public void SpawnRandomPosition()
        {
            var a = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)]);
            a.transform.position = spawnerlist[Random.Range(0, spawnerlist.Length)].transform.position;
         




        }
    

}
