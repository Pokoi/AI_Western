//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: October 19th 2018
// Last modification date: May 29th 2019 
//
// © pokoidev 2018 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script that manages the movement of the enemies
/// This script should be attached on an enemy
/// </summary>
public class MovementController : MonoBehaviour {


    /// <summary>
    /// Movement speed
    /// </summary>
    public float mov_speed;
    /// <summary>
    /// The angle to rotate on each rotation 
    /// </summary>
    public float angles_to_rotate;

    /// <summary>
    /// Final position X coordinate
    /// </summary>
    float x_position_to_check;
    /// <summary>
    /// Final position Z coordinate
    /// </summary>
    float z_position_to_check;
    /// <summary>
    /// The direction of the movement
    /// </summary>
    Vector3 direction;
    /// <summary>
    /// Reference to the controller of this enemy
    /// </summary>
    DecisionMaker decision_maker;
    /// <summary>
    /// If this enemy is able to move.
    /// </summary>
    bool able_to_move;
    Node from_node = null;
    Transform my_transform;

  
    //------------------------------------------------------------------------------------------------
    // Movement Management

    public void Rotate() { my_transform.eulerAngles = new Vector3(my_transform.eulerAngles.x, my_transform.eulerAngles.y + angles_to_rotate, my_transform.eulerAngles.z); }
    public void Stop() { able_to_move = false;  }   
    public void ResumeMovement() { able_to_move = true; }
    public void MoveToPosition(Vector3 target_position)
    {
        x_position_to_check = target_position.x;
        z_position_to_check = target_position.z;       
        direction = new Vector3(target_position.x, my_transform.position.y, target_position.z);
        my_transform.LookAt(direction);

        ResumeMovement();
    }


    //-----------------------------------------------------------------------------------------------

    public void SetDecisionMaker(DecisionMaker _dm) { decision_maker = _dm; }
    
    private void Awake()
    {
        my_transform = transform;

        //Reset the Final position coordinates
        x_position_to_check   = my_transform.position.x;
        z_position_to_check   = my_transform.position.z;
               
    }
        
    private void Update()
    {
        if (able_to_move)
        {
            //If the enemy is at the destination
            if (Mathf.Abs(x_position_to_check - my_transform.position.x) <= 0.1 && Mathf.Abs(z_position_to_check - my_transform.position.z) <= 0.1) { decision_maker.ItemFound();}
            else
            {
                //Moves to the destination
                my_transform.position = Vector3.MoveTowards(my_transform.position, direction, mov_speed * Time.deltaTime);
                decision_maker.blackboard.SaveEnemyPosition(my_transform.position);
            }
        }
    }
    
}
