using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCenter : MonoBehaviour
{
    public Text GameOver;
    public Text YouWin;
    public Button Restart;
    public Button Restart2;
    public Button Next;
    public GameObject HoldAndDrag;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }     
    }

    public void EnableGameOver()
    { 
        GameOver.gameObject.SetActive(true);
    }

    public void EnableYouWin()
    {
        YouWin.gameObject.SetActive(true);
    }

    public void EnableRestart()
    {
        Restart.gameObject.SetActive(true);
    }

    public void EnableRestart2()
    {
        Restart2.gameObject.SetActive(true);
    }

    public void EnableNext()
    {
        Next.gameObject.SetActive(true);
    }

    public void CloseHAD()
    {
        HoldAndDrag.SetActive(false);
    }


}
