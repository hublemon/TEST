using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public Transform playerTransform;

    public float maxDistance = 8f; //생성반경
    public float minDistance = 4f;
    public float Distance;

    public float timeBetSpawnMax=7f;
    public float timeBetSpawnmin=2f;
    public float timeBetSpawn;   //생성간격

    float lastSpawnTime;   //마지막 생성시점

    // Start is called before the first frame update
    void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnmin, timeBetSpawnMax);
        lastSpawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn=Random.Range(timeBetSpawnmin, timeBetSpawnMax);
            Spawn();
        }
    }
    void Spawn()
    {
        Distance = Random.Range(minDistance, maxDistance);
        int ran = Random.Range(0, 360); //랜덤으로 0~360도
        float x = Mathf.Cos(ran * Mathf.Deg2Rad) * Distance;
        float z = Mathf.Sin(ran * Mathf.Deg2Rad) * Distance;

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        Vector3 pos = playerTransform.position + new Vector3(x, 1f, z); 
                GameObject item = Instantiate(selectedItem, pos, Quaternion.identity);
                Destroy(item, 5f);
            }
    }

