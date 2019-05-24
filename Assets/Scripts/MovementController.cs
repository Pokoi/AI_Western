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
    EnemyController this_enemy_controller;
    /// <summary>
    /// If this enemy is able to move.
    /// </summary>
    bool able_to_move;
    Node from_node = null;

  
    /// <summary>
    /// Rotates the enemy
    /// </summary>
    public void Rotate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angles_to_rotate, transform.eulerAngles.z);
    }

    /// <summary>
    /// Stop the movement.
    /// </summary>
    public void Stop()
    {
        able_to_move = false;
    }

    /// <summary>
    /// Resumes the movement.
    /// </summary>
    public void ResumeMovement()
    {
        able_to_move = true;
    }

    /// <summary>
    /// Gets the new destination.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    public void GetNewDestination(List<Node> nodes)
    {
        if (nodes.Count != 0)
        {

            float minimum_f_star = -1;
            Node current_node = null;

            foreach (Node node in nodes)
            {
                float node_f_star = node.GetFStar(this_enemy_controller.visible_for_player, transform.position);

                if ((minimum_f_star == -1 || node_f_star < minimum_f_star) && node != this_enemy_controller.current_node && node != from_node)
                {
                    minimum_f_star = node_f_star;
                    current_node = node;
                }
            }

            if (current_node != null)
            {
                from_node = this_enemy_controller.current_node;
                this_enemy_controller.UpdatesNodeOcupation(-1);
                this_enemy_controller.current_node = current_node;
                MoveToCover(current_node.GetPosition);
            }
            else Rotate();

        }
    }

    /// <summary>
    /// At the first frame
    /// </summary>
    private void Start()
    {
        //Reset the Final position coordinates
        x_position_to_check = transform.position.x;
        z_position_to_check = transform.position.z;
        this_enemy_controller = GetComponent<EnemyController>();
    }
    /// <summary>
    /// On each frame
    /// </summary>
    private void Update()
    {
        if (able_to_move)
        {
            //If the enemy is at the destination
            if (Mathf.Abs(x_position_to_check - transform.position.x) <= 0.1 && Mathf.Abs(z_position_to_check - transform.position.z) <= 0.1)
            {
                this_enemy_controller.AtDestination();
                this_enemy_controller.UpdatesNodeOcupation(1);

            }
            else
            {
                //Moves to the destination
                transform.position = Vector3.MoveTowards(transform.position, direction, mov_speed * Time.deltaTime);
            }
        }
    }
    /// <summary>
    /// Method that set the destination position
    /// </summary>
    /// <param name="target_position"></param>
    private void MoveToCover(Vector3 target_position)
    {

        x_position_to_check = target_position.x;
        z_position_to_check = target_position.z;

        direction = new Vector3(target_position.x, transform.position.y, target_position.z);
        transform.LookAt(direction);

        ResumeMovement();
    }


}
