using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class WavesSpawner : MonoBehaviour
{
    public WaveData[] waves;
    public int curWave = 0;

    public int remainingEnemies;

    [Header("Components")] 
    public Transform enemySpawnPos;
    public TextMeshProUGUI waveText;
    public GameObject nextWaveButton;

    public void SpawnNextWave()
    {
        curWave++;
        
        if(curWave - 1 == waves.Length)
            return;

        waveText.text = $"Wave: {curWave}";
        StartCoroutine(SpawnWave());
    }
    
    IEnumerator SpawnWave()
    {
        nextWaveButton.SetActive(false);
        WaveData wave = waves[curWave - 1];

        for (int x = 0; x < wave.EnemySets.Length; x++)
        {
            yield return new WaitForSeconds(wave.EnemySets[x].spawnDelay);
            for (int y = 0; y < wave.EnemySets[x].spawnCount; y++)
            {
                SpawnEnemy(wave.EnemySets[x].enemyPrefab);
                yield return new WaitForSeconds(wave.EnemySets[x].spawnRate);

            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, enemySpawnPos.position, Quaternion.identity);
        remainingEnemies++;
    }

    public void OnEnemyDestroyed()
    {
        remainingEnemies--;
        
        if(remainingEnemies == 0)
            nextWaveButton.SetActive(true);
    }
}
