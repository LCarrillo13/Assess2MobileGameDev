using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LoadScene("EndGame");
        }
    }
    
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
