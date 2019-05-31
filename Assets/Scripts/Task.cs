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

public abstract class Task
{
    protected DecisionMaker decision_maker_reference;
    protected EnemyBlackBoard blackboard_reference;
    protected string name;

    public Task(EnemyBlackBoard _b) { blackboard_reference = _b; decision_maker_reference = _b.GetDecisionMaker(); }
    public abstract void Execute(); 
    public override string ToString() { return name; }
}

public class SearchCover : Task
{    
    public SearchCover(EnemyBlackBoard _b) : base(_b){ name = "Search Cover"; }

    public override void Execute()
    {
        CanvasManager.Instance.SetCurrentTask(this.ToString());
        GetNewDestination(blackboard_reference.GetKnownNodes());
    }
    private void GetNewDestination(List<Node> nodes)
    {     
        foreach (var go in MainController.Instance.cover_list) { go.GetComponent<Cover>().SetText(""); }
        if (nodes.Count != 0)
        {

            float minimum_f_star = -1;
            Node  current_node   = null;

            foreach (Node node in nodes)
            {                
                float node_f_star = node.GetFStar(blackboard_reference.GetPerceptionState(), blackboard_reference.GetEnemyPosition());

                if ((minimum_f_star == -1 || node_f_star < minimum_f_star) && node != blackboard_reference.GetCurrentNode() && node != blackboard_reference.GetFromNode())
                {
                    minimum_f_star = node_f_star;
                    current_node = node;
                }
            }

            if (current_node != null)
            {
                blackboard_reference.SetFromNode(blackboard_reference.GetCurrentNode());
                blackboard_reference.UpdateNodeOcupation(-1);
                blackboard_reference.SetCurrentNode(current_node);
                decision_maker_reference.locomotion.MoveToPosition(current_node.GetPosition);                
            }
            else{ decision_maker_reference.locomotion.Rotate(); }
        }
    }
}

public class SearchWeapon : Task
{
    public SearchWeapon(EnemyBlackBoard _b) : base(_b){ name = "Search Weapon"; }

    public override void Execute()
    {
        CanvasManager.Instance.SetCurrentTask(this.ToString());
        GetNewWeapon(blackboard_reference.GetKnownWeapons(), blackboard_reference.GetWeapon());
    }

    private void GetNewWeapon(List<Weapon> weapons, Weapon _current_weapon)
    {
        if (weapons.Count != 0)
        {
            Weapon current_weapon = _current_weapon;

            foreach (Weapon weapon in weapons)
            {
                if (current_weapon == null || weapon.GetScore(blackboard_reference.GetEnemyPosition()) < current_weapon.GetScore(blackboard_reference.GetEnemyPosition()))
                    current_weapon = weapon;
            }

            if (current_weapon != null)
            {
                decision_maker_reference.locomotion.MoveToPosition(current_weapon.GetPosition);
            }
            else { decision_maker_reference.locomotion.Rotate(); }
        }
    }

}

public class Shot : Task
{
    public Shot(EnemyBlackBoard _b) : base(_b){ name = "Shot"; }

    public override void Execute() { CanvasManager.Instance.SetCurrentTask(this.ToString()); Debug.Break();}
}

public class TakeWeapon : Task
{
    public TakeWeapon(EnemyBlackBoard _b) : base(_b) { name = "Take Weapon"; }
    public override void Execute()
    {
        CanvasManager.Instance.SetCurrentTask(this.ToString());
        Taken(blackboard_reference.GetDesiredWeapon());
    }
    void Taken(Weapon _w)
    {
        
        if (blackboard_reference.GetWeapon() != null)
        {
            blackboard_reference.GetWeapon().transform.parent = WeaponManager.Instance.transform;
            blackboard_reference.GetWeapon().transform.position = new Vector3(blackboard_reference.GetWeapon().transform.position.x, 0, blackboard_reference.GetWeapon().transform.position.z);
        }

        blackboard_reference.SetWeapon(_w);
        blackboard_reference.GetWeapon().transform.parent = decision_maker_reference.weapon_socket;
        blackboard_reference.GetWeapon().transform.localPosition = Vector3.zero;
    }
}