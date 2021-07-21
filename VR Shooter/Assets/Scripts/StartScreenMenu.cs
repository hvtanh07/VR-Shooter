using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreenMenu : MonoBehaviour
{
    [SerializeField]
    private string nextscene;
    public Player player;
    public void PlayGame()
    {
        SceneManager.LoadScene(nextscene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
