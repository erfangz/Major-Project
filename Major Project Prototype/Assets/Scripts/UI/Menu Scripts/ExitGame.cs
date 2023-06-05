using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [SerializeField]
    private GameObject quitGamePopUp;

    /// <summary>
    /// Closes the game if player presses "yes" button on prompt
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Closes the pop-up when player presses the "no" button
    /// </summary>
    public void DontQuitGame()
    {
        quitGamePopUp.SetActive(false); 
    }
}
