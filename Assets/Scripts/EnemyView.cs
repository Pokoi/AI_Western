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
/// Script that manages the enemy vision
/// This script should be attached on an enemy
/// </summary>
public class EnemyView : MonoBehaviour {

    [Space]
    [Header("Angulaciones del rayo")]
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

    [Space]
    [Header("Orígenes del rayo")]
    /// <summary>
    /// The origin of the eye rays
    /// </summary>
    public Transform eye_ray_origin;
    /// <summary>
    /// The origin of the foot rays
    /// </summary>
    public Transform foot_ray_origin;

    /// <summary>
    /// If this enemy is being seen by the player
    /// </summary>
    private bool im_visible;
    /// <summary>
    /// The controller of this enemy.
    /// </summary>
    EnemyController controller_of_this_enemy;



    /// <summary>
    /// Method that casts the rays
    /// </summary>
    public void CastTheRay()
    {
        controller_of_this_enemy.ClearNodes();

        //The hit of the raycast
        RaycastHit hit;

        //Set the ray casting direction
        Vector3 direction = Quaternion.Euler(0, min_angle_ray, 0) * transform.forward;
        byte iterator;

        bool seing_player = false;

        //Loop that cast (max_angle_ray - min_angle_ray)/angle_between_rays rays
        for (iterator = (byte)((max_angle_ray - min_angle_ray) / angle_between_rays); iterator > 0; iterator--)
        {
            //If hits the player
            if (Physics.Raycast(eye_ray_origin.position, direction, out hit) && hit.transform.CompareTag("player"))
            {
                seing_player = true;
            }

            //If hits a cover
            if (Physics.Raycast(foot_ray_origin.position, direction, out hit) && hit.collider.GetComponent<Cover>())
            {
                controller_of_this_enemy.AddNode(hit.collider.GetComponent<Node>().GetNode);
            }

            //Update ray direction
            direction = Quaternion.Euler(0, angle_between_rays, 0) * direction;
        }

        if (seing_player) controller_of_this_enemy.SeeingPlayer();
        else              controller_of_this_enemy.NotSeeingPlayer();

       
    }
    /// <summary>
    /// At the first frame
    /// </summary>
    void Awake()    {

        controller_of_this_enemy = GetComponent<EnemyController>();
    }

}
