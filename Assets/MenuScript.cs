using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] GameObject hotkeysParent;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1_Ofek");
    }

    public void ShowHotkeys()
    {
        hotkeysParent.SetActive(!hotkeysParent.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}
