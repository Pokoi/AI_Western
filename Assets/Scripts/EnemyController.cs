//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: April 1st 2019
// Last modification date: April 1st 2019 
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
    public bool visible_for_player { get; set; }
    public Node current_node;

    List<Node>         known_nodes          = new List<Node>();
    MovementController Locomotion;
    EnemyView          this_enemy_view;
    ShotController     this_enemy_shot_controller;

    enum PerceptionSystem { seing_player, searching_player        }
    enum CoverState       { half_covered, full_covered, uncovered }
    enum MovementStates   { moving, at_destination, stoped        }

    PerceptionSystem state;
    CoverState       this_enemy_cover_state;
    MovementStates   this_enemy_movement_state;

    void Awake()
    {
        Locomotion                = GetComponent<MovementController>();
        this_enemy_view           = GetComponent<EnemyView>();
        this_enemy_cover_state    = CoverState.uncovered;
        this_enemy_movement_state = MovementStates.at_destination;

    }

    private void Start()
    {
        StartCoroutine(CastingCoroutine(seconds_between_cast));
    }

    void Update()
    {
        MakeADecision();
    }

    /// <summary>
    /// Adds the given node to the list of known nodes.
    /// </summary>
    /// <param name="node">Node.</param>
    public void AddNode(Node node){
        if (!known_nodes.Contains(node)) known_nodes.Add(node);
    }

    /// <summary>
    /// Clears the node list.
    /// </summary>
    public void ClearNodes()
    {
        known_nodes.Clear();
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
                switch (this_enemy_cover_state)
                {
                    case CoverState.full_covered:
                        this_enemy_shot_controller.Shot();
                        break;
                    case CoverState.half_covered:
                        if (visible_for_player) Locomotion.GetNewDestination(known_nodes);
                        else this_enemy_shot_controller.Shot();
                        break;
                    case CoverState.uncovered:
                        Locomotion.GetNewDestination(known_nodes);
                        break;
                }
                break;

            case PerceptionSystem.searching_player:

                switch (this_enemy_movement_state)
                {
                    case MovementStates.at_destination:
                        Locomotion.GetNewDestination(known_nodes);
                        this_enemy_movement_state = MovementStates.moving;
                        break;
                    case MovementStates.moving:
                        break;
                    case MovementStates.stoped:
                        Locomotion.GetNewDestination(known_nodes);
                        break;
                }

                break;
        }
    }

    /// <summary>
    /// Courutine that calls the method "CastTheRay" of the view system each given seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator CastingCoroutine(float seconds)
    {
        while (true)
        {
            this_enemy_view.CastTheRay();
            yield return new WaitForSeconds(seconds);
        }
       
    }


}
