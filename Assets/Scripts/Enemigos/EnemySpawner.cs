using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Oleada
    {
        public GameObject enemyPrefab;
        public float relojSpawn;
        public float cooldownSpawn;
        public int enemigoPorRonda;
        public int enemigosContador;
    }
    public List<Oleada> oleadas;
    public int numeroOleada;
    public Transform minPos;
    public Transform maxPos;

    void Update()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            oleadas[numeroOleada].relojSpawn += Time.deltaTime;
            if (oleadas[numeroOleada].relojSpawn >= oleadas[numeroOleada].cooldownSpawn)
            {
                oleadas[numeroOleada].relojSpawn = 0;
                SpawnEnemigo();
            }
            if (oleadas[numeroOleada].enemigosContador >= oleadas[numeroOleada].enemigoPorRonda)
            {
                oleadas[numeroOleada].enemigosContador = 0;
                if (oleadas[numeroOleada].cooldownSpawn > 0.15f)
                {
                    oleadas[numeroOleada].cooldownSpawn *= 0.8f;
                }
                numeroOleada++;
            }
            if (numeroOleada >= oleadas.Count)
            {
                numeroOleada = 0;
            }
        }
    }

    private void SpawnEnemigo()
    {
        Instantiate(oleadas[numeroOleada].enemyPrefab, RandomSpawnPoint(), transform.rotation);
        oleadas[numeroOleada].enemigosContador++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        if (Random.Range(0f, 1f) > 0.5)
        {
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.y = minPos.position.y;
            }
            else
            {
                spawnPoint.y = maxPos.position.y;
            }
        }
        else
        {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.x = minPos.position.x;
            }
            else
            {
                spawnPoint.x = maxPos.position.x;
            }
        }
        return spawnPoint;
    }
}
