using Array2DEditor;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int Waves;
    private int currentWave = 0;
    public float SimpleEnemySpawnDelay;
    public GameObject[] EnemyPrefabs; 
    public Transform[] Spawners,Goals;
    private bool[] WaveSpawned;
    public float[] WaveDelay;
    [SerializeField] private Array2DBool WavetoSpawner, EnemiesSpawned;
    [SerializeField] private Array2DInt[] SpawnersWave, EnemyType, EnemyGoal;
    [SerializeField] private Array2DFloat[] SpawningDelay; 
    private void Awake()
    {
        WaveSpawned = new bool[Waves];
        for (int i = 0; i < WavetoSpawner.GridSize.x; i++) 
        {
            for (int j = 0; j < WavetoSpawner.GridSize.y; j++)
            {
                EnemiesSpawned.SetCell(i, j,!WavetoSpawner.GetCell(i, j));
            }
        }
    }
    private void Update()
    {
        bool WaveCompleted= true;
        for(int i = 0; i < Spawners.Length; i++)
        {
            if(!EnemiesSpawned.GetCell(currentWave,i))
            {
                WaveCompleted = false;
                break;
            }
        }
        if(WaveCompleted)
        {
            if (currentWave == Waves)
            {
                Debug.Log("All Waves Spawned");
            }
            else 
            {
                WaveSpawned[currentWave] = true;
                currentWave++;
                StartWave(currentWave);
            }
        }
    }
    private void Start()
    {
        StartWave(currentWave);
    }
    public void StartWave(int Wave)
    {
        for (int i = 0; i < Spawners.Length; i++)
        {
            if (WavetoSpawner.GetCell(Wave, i))
            {
                StartCoroutine(StartSpawning(currentWave, i));
            }
        }
    }
    IEnumerator StartSpawning(int currentWave,int Spawner)
    {
        for(int i = 0; i < SpawnersWave[Spawner].GridSize.y; i++)
        {
            for(int j = 0; j < SpawnersWave[Spawner].GetCell(currentWave,i); j++)
            {
                GameObject Enemy = Instantiate(EnemyPrefabs[EnemyType[Spawner].GetCell(currentWave, i)], Spawners[Spawner].position, Quaternion.identity);
                Enemy.GetComponent<AIDestinationSetter>().target = Goals[EnemyGoal[Spawner].GetCell(currentWave,i)];
                yield return new WaitForSeconds(SpawningDelay[Spawner].GetCell(currentWave, i));
            }
            yield return new WaitForSeconds(SimpleEnemySpawnDelay);
        }
        yield return new WaitForSeconds(WaveDelay[currentWave]);
        EnemiesSpawned.SetCell(currentWave,Spawner,true);
    }
}
