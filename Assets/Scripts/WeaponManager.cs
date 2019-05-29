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

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance {get {return instance;}} 

    static WeaponManager instance;

    public readonly int MIN_DAMAGE =  1;
    public readonly int MAX_DAMAGE = 10;
    public readonly int MIN_RANGE  =  2;
    public readonly int MAX_RANGE  = 20;
    public readonly int DAMAGE_SCORE_MULTIPLIER = 2;
    public readonly int RANGE_SCORE_MULTIPLIER  = 1;
    public int score_material_separator;

    public Material[] materials;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        score_material_separator = ((MAX_DAMAGE * DAMAGE_SCORE_MULTIPLIER) + (MAX_RANGE * RANGE_SCORE_MULTIPLIER)) / materials.Length;
    }


    public Material GetMaterial(int _score) { return materials[_score / score_material_separator]; }


}
