using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class LevelBackHandler : MonoBehaviour
{
    public Button backButton;
    
    void Start()
    {
        backButton.onClick.AddListener(HandleBackButton);
    }

    void HandleBackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
