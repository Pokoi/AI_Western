//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: January 13th 2019
// Last modification date: May 21th 2019
//
// © pokoidev 2019 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that manages the player movement
/// This should be attached to the player
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    #region Parameters
    ///////////////////////////////////////////////////////////////////
    //public



    ///////////////////////////////////////////////////////////////////
    //private

    /// <summary>
    /// The movement speed of the player
    /// </summary>
    [SerializeField]
    float mov_speed;

    /// <summary>
    /// The rotation speed of the player
    /// </summary>
    [SerializeField]
    float rot_speed;

    /// <summary>
    /// The player transform
    /// </summary>
    Transform my_transform;

    /// <summary>
    /// The player rigidbody
    /// </summary>
    Rigidbody my_rigidbody;

    /// <summary>
    /// The rotation angle
    /// </summary>
    float y_rotation;

    /// <summary>
    /// The movement speed vector
    /// </summary>
    Vector3 speed_vector;
     


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
        my_transform = transform;
        my_rigidbody = GetComponentInChildren<Rigidbody>();
    }

    /// <summary>
    /// Each frame
    /// </summary>
    void Update()
    {

        // Set the position
        // With transform
        // my_transform.position += my_transform.forward * Time.deltaTime * mov_speed * Input.GetAxis("Vertical");
        // my_transform.position += my_transform.right * Time.deltaTime * mov_speed * Input.GetAxis("Horizontal");

        // With rigidbody       
        speed_vector = my_transform.forward * Input.GetAxis("Vertical");
        my_rigidbody.velocity = speed_vector * mov_speed;

        //Set the rotation
        //y_rotation += (Input.GetAxis("Mouse X") * rot_speed);
        //my_transform.rotation = Quaternion.Euler(my_transform.eulerAngles.x, y_rotation, my_transform.eulerAngles.z);

        y_rotation += (Input.GetAxis("Horizontal") * rot_speed);
        my_transform.rotation = Quaternion.Euler(my_transform.eulerAngles.x, y_rotation, my_transform.eulerAngles.z);

    }


    #endregion
}

