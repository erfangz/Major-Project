using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    /// <summary>
    /// Opens a selected Sub Menu from the Main Menu Button
    /// </summary>
    /// <param name="Menu">The menu to be opened on pressing the relevant button</param>
    public void ToggleMenu(GameObject Menu)
    {
        Menu.SetActive(true);
    }
}
