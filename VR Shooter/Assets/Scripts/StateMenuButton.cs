using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMenuButton : MonoBehaviour
{
    public TextMesh StateText;
    string currentText;
    // Start is called before the first frame update
    void Start()
    {
        currentText = StateText.text;
    }

    // Update is called once per frame
    public void Selected()
    {
        StateText.text = "Play again?";
    }
    public void Moveout()
    {
        StateText.text = currentText;
    }
    public void Click()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }
}
