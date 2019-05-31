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
/// Script that manages the count of covers
/// This script should be attached on a parent which children in hierarchy has Cover components
/// </summary>
public class CoverChecker : MonoBehaviour {

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public



    ///////////////////////////////////////////////////////////////////
    //private

    /// <summary>    
    /// An array with all the covers
    /// </summary>
    Cover[] Covers;

    #endregion
    
    #region Methods
    ///////////////////////////////////////////////////////////////////
    //public



    ///////////////////////////////////////////////////////////////////
    //private

    /// <summary>
    /// At the first frame
    /// </summary>
    void Start()
    {
        //Save all this gameobject children covers
        Covers = GetComponentsInChildren<Cover>();

        //Add the covers to the main list of covers
        AddCoversToMainList();
    }

    /// <summary>
    /// Add all this gameobject children covers to the main list of covers
    /// </summary>
    void AddCoversToMainList()
    {
        foreach (var c in Covers)
        {
            MainController.Instance.cover_list.Add(c.gameObject);
        }
    }
    
    #endregion
       
}

