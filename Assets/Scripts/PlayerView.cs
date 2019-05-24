//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: October 19th 2018
// Last modification date: April 1st 2019 
//
// © pokoidev 2018 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that manages the view of the player
/// This script is attached to the player
/// </summary>
public class PlayerView : MonoBehaviour {

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public

    /// <summary>
    /// The negative angle of view (left to the view axis)
    /// </summary>
    public float min_angle_ray;

    /// <summary>
    /// The positive angle of view (right to the view axis)
    /// </summary>
    public float max_angle_ray;

    /// <summary>
    /// The angle between each casted ray
    /// </summary>
    public float angle_between_rays;

    /// <summary>
    /// The seconds between each view casting
    /// </summary>
    public float seconds_between_casts;

    /// <summary>
    /// The origin of the eye rays
    /// </summary>
    public Transform eye_ray_origin;

    /// <summary>
    /// The origin of the foot rays
    /// </summary>
    public Transform foot_ray_origin;

    ///////////////////////////////////////////////////////////////////
    //private


    #endregion


    #region Methods
    ///////////////////////////////////////////////////////////////////
    //public


    ///////////////////////////////////////////////////////////////////
    //private


    /// <summary>
    /// At the first frame
    /// </summary>
    void Start () {

        //Start the coroutine that cast the rays
        StartCoroutine(CastingCoroutine(seconds_between_casts));
	}

    /// <summary>
    /// Method called by the courutine that cast the rays
    /// </summary>
    void CastTheRay()
    {
        //The hit of the raycast
        RaycastHit hit;

        //Set the ray casting direction
        Vector3 direction = Quaternion.Euler(0, min_angle_ray, 0) * transform.forward;

        byte iterator;

        //Reset the "is_visible_for_player" value of all covers 
        MainController.Instance.ResetAllCovers();

        //Loop that cast (max_angle_ray - min_angle_ray)/angle_between_rays rays
        for (iterator = (byte)((max_angle_ray - min_angle_ray) / angle_between_rays); iterator > 0; iterator--)
        {
            Debug.DrawRay(foot_ray_origin.position, direction * 10f, Color.cyan, seconds_between_casts);
            //If hits a cover
            if (Physics.Raycast(foot_ray_origin.position, direction, out hit) && hit.collider.GetComponent<Cover>())
            {
                //Set this cover as visible for the player
                hit.collider.GetComponent<Node>().GetVisibleByPlayer = true;
            }

            //If hits an enemy
            else if (Physics.Raycast(eye_ray_origin.position, direction, out hit) && hit.transform.CompareTag("enemy"))
            {
                //Set this enemy as visble for the player
                hit.transform.GetComponentInParent<EnemyController>().visible_for_player = true;
            }

            //Update ray casting direction
            direction = Quaternion.Euler(0, angle_between_rays, 0) * direction;
        }
    }

    /// <summary>
    /// Courutine that calls the method "CastTheRay" each given seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator CastingCoroutine(float seconds)
    {
        while (true)
        {
            CastTheRay();
            yield return new WaitForSeconds(seconds);
           
        }

    }

    #endregion
}
