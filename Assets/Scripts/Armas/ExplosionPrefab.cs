using UnityEngine;

public class ExplosionPrefab : MonoBehaviour
{
    private Explosion weapon;
    private float duration;

    void Start()
    {
        weapon = GameObject.Find("Explosion").GetComponent<Explosion>();
        duration = weapon.stats[weapon.weaponLevel].duration;
        AudioController.Instance.ReproducirSonido(AudioController.Instance.directionalWeaponSpawn);
        transform.SetParent(null);
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            AudioController.Instance.ReproducirSonido(AudioController.Instance.directionalWeaponHit);
        }
    }
}