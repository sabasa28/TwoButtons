using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInitialScreen : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] GameObject[] nonMobileObjects;
    [SerializeField] GameObject[] mobileObjects;

    private void Start()
    {
        if (CheckMobile.isMobile())
        {
            foreach (GameObject go in nonMobileObjects)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in mobileObjects)
            {
                go.SetActive(true);
            }
        }
    }
    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartGame();
        }
    }
    public void StartGame()
    { 
        SceneManager.LoadScene("Gameplay");
    }
}
