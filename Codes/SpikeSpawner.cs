using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject Spike;
    public float SpawnRate = 2f;
    float timer = 0;
    public float delaySpawn = 0;
    public bool InstantSpawn = false;

    private void Start()
    {
        if (InstantSpawn == true)
        {
            SpawnTrap();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < SpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnTrap();
            timer = 0;
        }

    }

    void SpawnTrap()
    {
        StartCoroutine(SpawnTrapT());
    }

    IEnumerator SpawnTrapT()
    {
        Instantiate(Spike, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        yield return new WaitForSeconds(delaySpawn);
    }
}
