//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: April 1st 2019
// Last modification date: May 29th 2019 
//
// © pokoidev 2018 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float seconds_between_cast = 1f;
    public Node current_node;

     


    void Update()
    {
        MakeADecision();
    }
    


    /// <summary>
    /// Method called when the enemy is at the destination
    /// </summary>
    public void AtDestination() 
    { 
        this_enemy_movement_state = MovementStates.at_destination;
    }

    /// <summary>
    /// Method that updates the flag of seeing the player
    /// </summary>
    public void SeeingPlayer()
    {
        if (state != PerceptionSystem.seing_player) state = PerceptionSystem.seing_player;
    }

    /// <summary>
    /// Method that updates the flag of seeing the player.
    /// </summary>
    public void NotSeeingPlayer()
    {
        state = PerceptionSystem.searching_player;
    }

    /// <summary>
    /// Updates the current node ocupation.
    /// </summary>
    /// <param name="amount">Amount.</param>
    public void UpdatesNodeOcupation(int amount)
    {
        if(current_node != null) current_node.GetOcupation += amount;
    }

    /// <summary>
    /// Method that makes the decision of the enemy behaviour
    /// </summary>
    public void MakeADecision()
    {
        switch (state)
        {
            case PerceptionSystem.seing_player:

                if (visible_for_player)
                {
                    switch (this_enemy_cover_state)
                    {
                        case CoverState.full_covered:
                            if (this_enemy_armed_state == ArmedState.armed) this_enemy_shot_controller.Shot();
                            else SearchWeapon();
                            break;
                        case CoverState.half_covered:
                            if (this_enemy_armed_state == ArmedState.armed) this_enemy_shot_controller.Shot();
                            else SearchWeapon();
                            break;
                        case CoverState.uncovered:
                            SearchCover();
                            break;
                    }
                }
                else SearchWeapon();
              
                break;

            case PerceptionSystem.searching_player:
                if (this_enemy_armed_state == ArmedState.armed)
                {
                    switch (this_enemy_movement_state)
                    {
                        case MovementStates.at_destination:
                            this_enemy_cover_state = CoverState.full_covered;
                            Locomotion.GetNewDestination(known_nodes);
                            this_enemy_movement_state = MovementStates.moving;
                            break;
                        case MovementStates.moving:
                            this_enemy_cover_state = CoverState.uncovered;
                            break;
                        case MovementStates.stoped:
                            Locomotion.GetNewDestination(known_nodes);
                            break;
                    }
                }
                else SearchWeapon();

                break;
        }
    }



    /// <summary>
    /// Searchs a cover .
    /// </summary>
    void SearchCover()
    {
        Locomotion.GetNewDestination(known_nodes);
    }

    void SearchWeapon()
    {
        Locomotion.GetNewWeapon(known_weapons, this_enemy_shot_controller.current_weapon);
    }


}
