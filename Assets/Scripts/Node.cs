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

public class Node : MonoBehaviour
{
    public bool    GetVisibleByPlayer { get { return this_cover.Is_visible_for_player; } set { this_cover.Is_visible_for_player = value; } }
    public int     GetOcupation       { get; set; }
    public Vector3 GetPosition        { get; private set; }
    public Node    GetNode            { get { return this; }}

    private float _vision_mulitplier      = 1f;
    private float _disspersion_multiplier = 1f;
    private float _distance_multiplier    = 1f;

    Cover this_cover;

    private void Awake()
    {
        GetPosition = transform.position;
        this_cover  = transform.GetComponent<Cover>();
    }

    /// <summary>
    /// Method to know the Fstar value of a node
    /// </summary>
    /// <returns>The FS tar.</returns>
    /// <param name="enemy_see_player">If set to <c>true</c> enemy see player.</param>
    /// <param name="enemy_position">Enemy position.</param>
    public float GetFStar(EnemyBlackBoard.PerceptionSystem enemy_see_player, Vector3 enemy_position)
    {
        float to_return = 0;

        switch (enemy_see_player)
        {
            case EnemyBlackBoard.PerceptionSystem.seing_player:     to_return = ((CalculateG(enemy_position) * _distance_multiplier) + (CalculateVisionIndex() * _vision_mulitplier) + (CalculateDisspersionIndex() * _disspersion_multiplier)); break;
            case EnemyBlackBoard.PerceptionSystem.searching_player: to_return = ((CalculateG(enemy_position) * _distance_multiplier) + (CalculateDisspersionIndex() * _disspersion_multiplier)); break;
        }

        return to_return;
    }

    private float CalculateG(Vector3 position) { return Mathf.Clamp((Vector3.Distance(position, GetPosition) * 0.01f), 0, 1); }
    private float CalculateVisionIndex()       { return GetVisibleByPlayer ? 1 : 0; }
    private float CalculateDisspersionIndex()  { return GetOcupation > 0 ? 1 : 0; }

}
