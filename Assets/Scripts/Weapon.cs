//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: May 27th 2019
// Last modification date: May 27th 2019 
//
// © pokoidev 2019 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int     GetDamage   { get { return damage; } }
    public int     GetRange    { get { return range;  } }    
    public Vector3 GetPosition { get { return transform.position; } }


    int damage;
    int range;
    int score;
    TextMesh text;   

    MeshRenderer renderer;

    private void Start()
    {
        renderer = transform.GetComponent<MeshRenderer>();
        text     = GetComponentInChildren<TextMesh>();        
        damage = CalculateRandomInteger(WeaponManager.Instance.MIN_DAMAGE, WeaponManager.Instance.MAX_DAMAGE);
        range  = CalculateRandomInteger(WeaponManager.Instance.MIN_RANGE, WeaponManager.Instance.MAX_RANGE);          
    }

    int CalculateRandomInteger(int min, int max) { return Random.Range(min, max); }
    public int GetScore(Vector3 position) {
        score = GetFStar(position);
        renderer.material = WeaponManager.Instance.GetMaterial(score);
        text.text = score.ToString();
        return score;
    }


    int GetFStar(Vector3 position)
    {
        float to_return = 0;
        to_return = ((CalculateG(position) * WeaponManager.Instance.DISTANCE_MULTIPLIER) + (damage * WeaponManager.Instance.DAMAGE_SCORE_MULTIPLIER) + (range * WeaponManager.Instance.RANGE_SCORE_MULTIPLIER));
        return Mathf.RoundToInt(to_return);
    }

    float CalculateG(Vector3 position) { return Mathf.Clamp((Vector3.Distance(position, GetPosition) * 0.01f), 0, 1); }

}
