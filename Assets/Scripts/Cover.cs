//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: October 19th 2018
// Last modification date: January 12th 2019 
//
// © pokoidev 2018 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
/// <summary>
/// Class that manages cover funcionality
/// This script sould be attached at every cover point
/// </summary>
public class Cover : MonoBehaviour
{

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public 

    /// <summary>
    /// Public access to im_visible value
    /// </summary>
    public bool Is_visible_for_player
    {
        set
        {
            im_visible = value;

            renderer.material = (value) ? pink_material : green_material;            
        }

        get
        {
            return im_visible;
        }
    }


    ///////////////////////////////////////////////////////////////////
    //private 

    /// <summary>
    /// If this cover is being seen by the player
    /// </summary>
    private bool im_visible = false;

    /// <summary>
    /// If this cover is empty
    /// </summary>
    private bool im_empty = false;

    /// <summary>
    /// The mesh renderer of this object
    /// </summary>
    private MeshRenderer renderer;

    [SerializeField]
    /// <summary>
    /// The materials
    /// </summary>
    private Material green_material, pink_material;

    #endregion

    #region Methods
    ///////////////////////////////////////////////////////////////////
    //public 


    ///////////////////////////////////////////////////////////////////
    //private 

    /// <summary>
    /// Inizialization
    /// </summary>
    private void Awake()
    {
        renderer = transform.GetComponent<MeshRenderer>();
    }

    #endregion

}
