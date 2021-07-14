using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterDelay : MonoBehaviour
{
    [SerializeField]
    private float delaytimer = 79f;
    [SerializeField]
    private string nextscene;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delaytimer)
        {
            SceneManager.LoadScene(nextscene);
        }
    }
}
