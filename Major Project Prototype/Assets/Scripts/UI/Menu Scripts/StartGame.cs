using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    #region Objects
    // safe file reference

    #endregion

    #region Methods
    private void Start()
    {
        // check if save file exists
    }

    /// <summary>
    /// Start a new game
    /// </summary>
    public void NewGameStart()
    {
        // without existing save file: simply start the game from beginning
        SceneManager.LoadSceneAsync("");

        // with existing safe file: prompt player about existing save file, get confirmation whether new game should be started or not
    }

    /// <summary>
    /// Start an existing game save and continue playing
    /// </summary>
    public void ExistingGameStart()
    {
        // load save data

        // start saved game
    }
    #endregion
}
