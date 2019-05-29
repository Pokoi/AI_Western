using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController), typeof(EnemyView))]
public class DecisionMaker : MonoBehaviour
{

    public float seconds_between_cast = 1f;
       

    public EnemyBlackBoard    blackboard;
    public MovementController locomotion;
    public EnemyView          perception;

    enum items { cover, weapon};
    items looking_for;
       

    public void OnSeePlayer()
    {

    }

    public void OnSeeBetterWeapon()
    {

    }

    public void ItemFound()
    {
        switch (looking_for)
        {
            case items.cover:  blackboard.SetCoverState(EnemyBlackBoard.CoverState.covered); blackboard.UpdateNodeOcupation(+1); break;
            case items.weapon: blackboard.SetArmedState(EnemyBlackBoard.ArmedState.armed);   break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        blackboard = new EnemyBlackBoard();
        blackboard.SetDecisionMaker(this);

        locomotion = transform.GetComponent<MovementController>();
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

    void MakeADecision()
    {

    }


}
