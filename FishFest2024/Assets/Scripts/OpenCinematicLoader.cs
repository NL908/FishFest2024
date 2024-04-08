using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCinematicLoader : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartingScreen", LoadSceneMode.Single);
    }
}
