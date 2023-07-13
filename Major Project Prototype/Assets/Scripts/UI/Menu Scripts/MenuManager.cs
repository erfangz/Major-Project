using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Manages Main Menu Functions and Interactions + Sub Menus accessible from Main Menu
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Objects
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject returnButton;

    [Header("Main Menu Sub Menus")]
    [SerializeField]
    private GameObject newGameScreen;
    [SerializeField]
    private GameObject continueGameScreen;
    [SerializeField]
    private GameObject optionsScreen;
    [SerializeField]
    private GameObject quitGamePopUp;
    #endregion

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    #region Button Methods
    // functions open the respective menu while disabling the main menu

    //[Button]
    //public void NewGame()
    //{
    //    mainMenu.SetActive(false);
    //    newGameScreen.SetActive(true);
    //    returnButton.SetActive(true);
    //}

    //[Button]
    //public void ContinueGame()
    //{
    //    mainMenu.SetActive(false);
    //    continueGameScreen.SetActive(true);
    //    returnButton.SetActive(true);
    //}

    [Button]
    public void Options()
    {
        mainMenu.SetActive(false);
        optionsScreen.SetActive(true);
        returnButton.SetActive(true);
    }

    [Button]
    public void PromptQuitGame()
    {
        quitGamePopUp.SetActive(true);
    }

    /// <summary>
    /// Returns to Main Menu
    /// </summary>
    [Button]
    public void Return()
    {
        // disable all other menus
        continueGameScreen.SetActive(false);
        newGameScreen.SetActive(false);
        optionsScreen.SetActive(false);
        quitGamePopUp.SetActive(false);

        // enable main menu
        mainMenu.SetActive(true);

        // return button deactivated when in main menu
        returnButton.SetActive(false);
    }
    #endregion
}
