using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int GetDamage { get { return damage; } }
    public int GetRange  { get { return range;  } }
    public int GetScore  { get { return score;  } }


    int damage;
    int range;
    int score;

    MeshRenderer renderer;

    private void Start()
    {
        renderer = transform.GetComponent<MeshRenderer>();

        damage = CalculateRandomInteger(WeaponManager.Instance.MIN_DAMAGE, WeaponManager.Instance.MAX_DAMAGE);
        range  = CalculateRandomInteger(WeaponManager.Instance.MIN_RANGE, WeaponManager.Instance.MAX_RANGE);
        score = CalculateScore();
        renderer.material = WeaponManager.Instance.GetMaterial(score);
    }

    int CalculateRandomInteger(int min, int max) { return Random.Range(min, max); }
    int CalculateScore() { return ((damage * WeaponManager.Instance.DAMAGE_SCORE_MULTIPLIER) + (range * WeaponManager.Instance.RANGE_SCORE_MULTIPLIER)); }
}
