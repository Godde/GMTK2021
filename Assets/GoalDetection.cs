using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    [SerializeField]
    Rigidbody ball1;
    [SerializeField]
    Rigidbody ball2;
    float fieldLength = 30;

    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {

        int ball1Goal = CheckGoal(ball1);
        int ball2Goal = CheckGoal(ball2);

        GamestateScript.Instance.Goal(ball1Goal, 1);

        GamestateScript.Instance.Goal(ball2Goal, 2);

    }

    int CheckGoal(Rigidbody ball) //returns 0, no goal. 1 playerOne. 2 playerTwo
    {
        //Debug.Log(ball.position.x);
        if(ball.position.x > fieldLength/2 )
        {
            return 2;
        }
        if(ball.position.x < -fieldLength / 2)
        {
            return 1;
        }
        return 0;
    }
}
