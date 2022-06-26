using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject HelpPanel;

    private void Start()
    {
        MouseManager.Show(true);
        MouseManager.Lock(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Help()
    {
        HelpPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        HelpPanel.SetActive(false);
    }
}
