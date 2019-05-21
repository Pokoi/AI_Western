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
/// The main controller of the system
/// </summary>
public class MainController : MonoBehaviour {
 
    
    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static MainController instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static MainController Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase


    }

    #endregion

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public

    /// <summary>
    /// All cover list
    /// </summary>
    public List<GameObject> cover_list = new List<GameObject>();

    ///////////////////////////////////////////////////////////////////
    //private


    #endregion

    #region Methods
    ///////////////////////////////////////////////////////////////////
    //public

    /// <summary>
    /// Method that resets all covers visibility state
    /// </summary>
    public void ResetAllCovers()
    {
        foreach (var cover in cover_list)
        {
            cover.GetComponent<Cover>().Is_visible_for_player = false;
        }
    }

    ///////////////////////////////////////////////////////////////////
    //private


    #endregion
}
