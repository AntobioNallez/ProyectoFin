using UnityEngine;

public class Explosion : Weapon
{
    [SerializeField] private GameObject prefab;
    private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Transform enemy = GetRandomEnemy();

                if (enemy != null)
                {
                    Instantiate(prefab, enemy.position, Quaternion.identity, transform);
                }
            }
        }
    }

    Transform GetRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return null;

        int randomIndex = Random.Range(0, enemies.Length);
        return enemies[randomIndex].transform;
    }
}
