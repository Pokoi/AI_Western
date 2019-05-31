//
// Author: Jesus 'Pokoi' Villar 
//
// Creation date: May 29th 2019
// Last modification date: May 29th 2019 
//
// © pokoidev 2019 (pokoidev.com)
// Creative Commons License:
// Attribution 4.0 International (CC BY 4.0)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackBoard
{

    public enum PerceptionSystem { seing_player, searching_player }
    public enum VisibleStates    { visible_by_player, non_visible }
    public enum CoverState       { covered, uncovered }
    public enum ArmedState       { armed, disarmed }

    
    PerceptionSystem state;    
    VisibleStates    this_enemy_visible_state;    
    CoverState       this_enemy_cover_state;
    ArmedState       this_enemy_armed_state;


    List<Node>    known_nodes  ;
    List<Weapon>  known_weapons;

    Weapon        current_weapon;
    Weapon        desired_weapon;
    Node          current_node;
    Node          from_node;
    Vector3       enemy_position;
    DecisionMaker this_enemy_decision_maker;



    public EnemyBlackBoard()
    {
        known_nodes    = new List<Node>();
        known_weapons  = new List<Weapon>();
        current_weapon = null;
        this_enemy_armed_state = ArmedState.disarmed;
    }

    //----------------------------------------------------------------------------
    // Getters Methods
    public Weapon GetWeapon() { return current_weapon;}
    
    public PerceptionSystem GetPerceptionState() { return state;}
    public VisibleStates GetVisibleState()       { return this_enemy_visible_state; }
    public CoverState GetCoverState()            { return this_enemy_cover_state;}
    public ArmedState GetArmedState()            { return this_enemy_armed_state;}
    public Node GetCurrentNode()                 { return current_node; }
    public Vector3 GetEnemyPosition()            { return enemy_position; }
    public Node GetFromNode()                    { return from_node; }
    public List<Node> GetKnownNodes()            { return known_nodes; }
    public List<Weapon> GetKnownWeapons()        { return known_weapons; }
    public DecisionMaker GetDecisionMaker()      { return this_enemy_decision_maker; }
    public Weapon GetDesiredWeapon()             { return desired_weapon; }


    //------------------------------------------------------------------------------
    // Setters Methods
    public void SetWeapon(Weapon _w) { current_weapon = _w; OnChange(); }
    public void SetPerceptionState(PerceptionSystem _p) { state = _p; OnChange(); }
    public void SetVisibleState(VisibleStates _v)       { this_enemy_visible_state = _v; OnChange(); }
    public void SetCoverState(CoverState _c)            { this_enemy_cover_state = _c; OnChange(); }
    public void SetArmedState(ArmedState _a)            { this_enemy_armed_state = _a; OnChange(); }
    public void SetCurrentNode(Node _n)                 { current_node = _n;}
    public void SaveEnemyPosition(Vector3 _v)           { enemy_position = _v; }
    public void SetFromNode(Node _n)                    { from_node = _n; }
    public void SetDecisionMaker(DecisionMaker _d)      { this_enemy_decision_maker = _d; }
    public void SetDesiredWeapon(Weapon _w)             { desired_weapon = _w; OnChange(); }


    //--------------------------------------------------------------------------------
    // Lists Management
    public void AddKnownCoverNode (Node _n)   { if (!known_nodes.Contains(_n)) known_nodes.Add(_n); }
    public void AddKnownWeapon    (Weapon _w) { if (!known_weapons.Contains(_w)) known_weapons.Add(_w); }
    public void ClearKnownNodes()   { known_nodes.Clear(); }
    public void ClearKnownWeapons() { known_weapons.Clear(); }


    //--------------------------------------------------------------------------------
    // Other Methods
    public void UpdateNodeOcupation(int amount) { if(current_node != null) current_node.GetOcupation += amount; }
    public bool IsBetterWeapon(Weapon _w) { return (current_weapon != null) ? _w.GetScore(GetEnemyPosition()) > current_weapon.GetScore(GetEnemyPosition()) : true; }
    void OnChange() { this_enemy_decision_maker.MakeADecision(); } 


}
