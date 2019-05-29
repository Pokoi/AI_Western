using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController), typeof(EnemyView))]
public class DecisionMaker : MonoBehaviour
{

    public float     seconds_between_cast = 1f;
    public Transform weapon_socket;
       

    public EnemyBlackBoard    blackboard;
    public MovementController locomotion;
    public EnemyView          perception;

    enum items { cover, weapon};
    items looking_for;



    public void MakeADecision()
    {
        switch (blackboard.GetPerceptionState())
        {
            case EnemyBlackBoard.PerceptionSystem.seing_player:
                switch (blackboard.GetVisibleState())
                {
                    case EnemyBlackBoard.VisibleStates.visible_by_player:
                        switch (blackboard.GetCoverState())
                        {
                            case EnemyBlackBoard.CoverState.covered:
                                switch (blackboard.GetArmedState())
                                {
                                    case EnemyBlackBoard.ArmedState.armed:    Shot s_task = new Shot(blackboard); s_task.Execute();break;
                                    case EnemyBlackBoard.ArmedState.disarmed: SearchWeapon sw_task = new SearchWeapon(blackboard); looking_for = items.weapon;  sw_task.Execute(); break;
                                }
                                break;
                            case EnemyBlackBoard.CoverState.uncovered: SearchCover sc_task = new SearchCover(blackboard); looking_for = items.cover;  sc_task.Execute(); break;                                
                        }
                        break;
                    case EnemyBlackBoard.VisibleStates.non_visible:
                        switch (blackboard.GetArmedState())
                        {
                            case EnemyBlackBoard.ArmedState.armed:    Shot s_task = new Shot(blackboard); s_task.Execute(); break;
                            case EnemyBlackBoard.ArmedState.disarmed: SearchWeapon sw_task = new SearchWeapon(blackboard); looking_for = items.weapon; sw_task.Execute(); break;
                        }
                        break;
                }
                break;

            case EnemyBlackBoard.PerceptionSystem.searching_player:
                switch (blackboard.GetArmedState())
                {
                    case EnemyBlackBoard.ArmedState.armed:     SearchCover sc_task  = new SearchCover(blackboard);  looking_for = items.cover; sc_task.Execute(); break;                        
                    case EnemyBlackBoard.ArmedState.disarmed:  SearchWeapon sw_task = new SearchWeapon(blackboard); looking_for = items.weapon; sw_task.Execute(); break;                        
                }
                break;
        }
    }

    public void OnSeePlayer() { blackboard.SetPerceptionState(EnemyBlackBoard.PerceptionSystem.seing_player);}

    public void OnSeeBetterWeapon(Weapon desired_weapon) { blackboard.SetDesiredWeapon(desired_weapon); }

    public void ItemFound()
    {
        switch (looking_for)
        {
            case items.cover:  blackboard.SetCoverState(EnemyBlackBoard.CoverState.covered); blackboard.UpdateNodeOcupation(+1); break;
            case items.weapon: blackboard.SetArmedState(EnemyBlackBoard.ArmedState.armed); TakeWeapon task = new TakeWeapon(blackboard); task.Execute();  break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        blackboard = new EnemyBlackBoard();
        blackboard.SetDecisionMaker(this);

        locomotion = transform.GetComponent<MovementController>();
        locomotion.SetDecisionMaker(this);
        perception = transform.GetComponent<EnemyView>();       

        StartCoroutine(CastingCoroutine(seconds_between_cast));
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CastingCoroutine(float seconds)
    {
        while (true)
        {
            perception.CastTheRay();
            yield return new WaitForSeconds(seconds);
        }
    }

    


}
