using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        //spawn enemies every 5 seconds
        //spawn continously
            while(_stopSpawning == false)
        {
            //posToSpawn variable containing the position where the enemy should spawn
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7.5f,0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            //Set the new enemy as a child to the Enemy Container
            newEnemy.transform.SetParent(_enemyContainer.transform);
            //Wait for 5 seconds before repeating the code above
            yield return new WaitForSeconds(5f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7f,0);
            Instantiate(_tripleShotPrefab,posToSpawn,Quaternion.identity);
            //wait a random number of seconds within the range of 6-9 secs before spawning another one
            yield return new WaitForSeconds(Random.Range(6,10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
