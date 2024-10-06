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
        Debug.Log("Test");
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
