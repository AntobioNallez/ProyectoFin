using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStats> stats;
    public Sprite weaponImage;
    public string basicDescription;

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            if (weaponLevel >= stats.Count - 1)
            {
                PlayerController.Instance.nivelMaximoArma.Add(this);
                PlayerController.Instance.armasActivas.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float cooldown;
    public float duration;
    public float damage;
    public float range;
    public float speed;
    public float amount;

    public string descripcion;
}
