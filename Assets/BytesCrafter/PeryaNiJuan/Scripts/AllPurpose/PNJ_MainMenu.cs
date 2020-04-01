
using UnityEngine;

public class PNJ_MainMenu : MonoBehaviour
{
    void OnEnable()
    {
        CustomReference.Access.gameState = PNJ_GameState.MainMenu;
    }
}
