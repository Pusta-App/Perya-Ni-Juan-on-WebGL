
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PNJ_PlayGames_Logic : MonoBehaviour
{
    public Button signIn;
    public Button signOut;
    public Button achievement;
    public Button leaderboard;

    void OnEnable()
    {
        //CustomReference.Access.pglogic = this;
        // if( !CustomReference.Access.playGames.isSignedIn )
        // {
        //     StartCoroutine(Authenticating());
        // }

        // else
        // {
        //     achievement.interactable = true;
        //     leaderboard.interactable = true;
        // }
    }

    private float authTimer = 0f;
    

    public void Deauthenticated()
    {
        signIn.interactable = true;
        signIn.gameObject.SetActive(true);
        signOut.interactable = false;
        signOut.gameObject.SetActive(false);
        achievement.interactable = false;
        leaderboard.interactable = false;
    }
}
