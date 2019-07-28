using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AboutMeScript : MonoBehaviour
{
    [SerializeField] private Transform minTransform, maxTransform;
    [SerializeField] private float spawnTime;
    private float timeToSpawn;
    private bool isSpawned;
    private void Update()
    {
        if (timeToSpawn <= 0)
        {
            GameObject tempGameObject = ObjectPoolManager.Instance.GetPooledObject("MyLovelyCube");
            if (tempGameObject != null)
            {
                tempGameObject.SetActive(true);
                tempGameObject.transform.parent = transform;
                tempGameObject.transform.position = new Vector3(Random.Range(minTransform.position.x, maxTransform.position.x), Random.Range(minTransform.position.y, maxTransform.position.y), minTransform.position.z);
            }
            timeToSpawn = spawnTime;
        }
        else
        {
            if (transform.parent.GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED)
            {
                timeToSpawn -= Time.deltaTime;
            }
            else
            {
                OnDisable();
            }
        }
    }
    
    private void OnDisable()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("MyLovelyCube");

        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(false);
        }
    }
}
