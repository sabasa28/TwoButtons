using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IfMobileChangeText : MonoBehaviour
{
    [SerializeField] string textForMobile;
    [SerializeField] GameObject objectForMobile;
    bool shouldTakeInput = false;
    void Start()
    {
        if (CheckMobile.isMobile())
        {
            GetComponent<TextMeshProUGUI>().text = textForMobile;
            objectForMobile.SetActive(true);
            StartCoroutine(TakeGameoverInput());
        }
    }

    IEnumerator TakeGameoverInput()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        shouldTakeInput = true;
    }

    public void RestartGame()
    {
        if (shouldTakeInput)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

}
