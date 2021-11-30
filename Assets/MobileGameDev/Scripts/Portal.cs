using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LoadScene("EndGame");
        } 
    }

   
    
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
