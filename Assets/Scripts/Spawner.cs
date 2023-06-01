using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform spawnPoint;

    public void Spawn()
    {
        Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
    }
}