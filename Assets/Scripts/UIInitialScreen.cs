using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInitialScreen : MonoBehaviour
{
    float timer = 0.0f;
    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
