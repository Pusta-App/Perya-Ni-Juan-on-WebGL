using UnityEngine;

public class PNJ_PP_BallGroup : MonoBehaviour
{
    public int curBall = 0;
    public Transform spawner;
    public PNJ_PP_BallScript[] balls;
    public Vector3[] ballPos;
    public Vector3 variancePos;
    private Vector3 getVariance 
    {
        get {
            return new Vector3(
                spawner.position.x + Random.Range(-variancePos.x, variancePos.x), 
                spawner.position.y + Random.Range(-variancePos.y, variancePos.y), 
                spawner.position.z + Random.Range(-variancePos.z, variancePos.z));
        }
    }

    public bool allSphereSleep
    {
        get 
        {
            if(balls == null && balls.Length != 3)
            {
                return false;
            }

            else
            {
                if(balls[0].rigidBody.IsSleeping() && balls[1].rigidBody.IsSleeping() && balls[2].rigidBody.IsSleeping())
                {
                    return true;
                }
                
                else
                {
                    return false;
                }
            }
        }
    }

    void OnEnable()
    {
        curBall = 0;
        ballPos = new Vector3[3];
        for(var i = 0; i < ballPos.Length; i++)
        {
            ballPos[i] = balls[i].transform.position;
        }
    }

    public void Reset()
    {
        curBall = 0;
        for(var i = 0; i < ballPos.Length; i++)
        {
            balls[i].isRestingOnPlatfrom = false;
            balls[i].rigidBody.useGravity = false;
            balls[i].transform.position = ballPos[i];
        }
    }

    public bool isAllBallOnPlatform
    {
        get {
            int numRestingOnPlatform = 0;
            for(var i = 0; i < ballPos.Length; i++)
            {
                if(balls[i].isRestingOnPlatfrom)
                {
                    numRestingOnPlatform += 1;
                }
            }
            return numRestingOnPlatform == 3 ? true : false;
        }
    }

    public void DropTheBall()
    {
        if(curBall == 3)
        {
            CustomReference.Access.Debugs("CANT DROP ANY BALL");
            return;
        }

        CustomReference.Access.dropBall.userMenu.BallHadBeenDrop();
        CustomReference.Access.dropBall.state = PNJ_PP_State.ONDRAW;
        balls[curBall].transform.position = getVariance;
        balls[curBall].rigidBody.useGravity = true;
        curBall += 1;

        if( curBall == 3)
        {
            //SHOW RESULT AND DISABLE DROPPING MORE BALLS.
            CustomReference.Access.Debugs("CHECKING RESULT NOW!");
            //CustomReference.Access.dropBall.state = PNJ_PP_State.TORESET;
            CustomReference.Access.dropBall.userMenu.DrosetOnDrop(false, false);
            CustomReference.Access.dropBall.CheckNow();
        }
    }
}

[System.Serializable]
public class PNJ_PP_ResultBall
{
    public bool xTwoRed = false;
    public bool xThreeRed = false;
    public bool xTwoWhite = false;
    public bool xThreeWhite = false;
    public bool xOneStar = false;
    public bool xTwoStar = false; 
}