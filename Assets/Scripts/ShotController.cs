//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: October 19th 2018
// Last modification date: October 19th 2018 
//
// © pokoidev 2018 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that manages the shot action of an actor
/// </summary>
public class ShotController : MonoBehaviour {

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public

    public Weapon current_weapon;
    public Transform weapon_socket;


    ///////////////////////////////////////////////////////////////////
    //private


    #endregion

    #region Methods
    ///////////////////////////////////////////////////////////////////
    //public

    public void Shot()
    {
        print("El enemigo hace pium pium");
        Debug.Break();
    }

    public void TakeWeapon(Weapon _w)
    {
        if (current_weapon != null)
        {
            current_weapon.transform.parent = WeaponManager.Instance.transform;
            current_weapon.transform.position = new Vector3(current_weapon.transform.position.x, 0, current_weapon.transform.position.z);
        }

        current_weapon = _w;
        current_weapon.transform.parent = weapon_socket;
        current_weapon.transform.localPosition = Vector3.zero;
    }

    ///////////////////////////////////////////////////////////////////
    //private


    #endregion

}
