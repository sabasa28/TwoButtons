using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpiderPlayer : MonoBehaviour
{
    [SerializeField] Transform rightArm;
    [SerializeField] Transform leftArm;
    [SerializeField] SpringRope springRopePrefab;
    Rigidbody2D rb;
    [SerializeField] SpringRope leftInstantiatedRope;
    [SerializeField] SpringRope rightInstantiatedRope;
    [SerializeField] float nextHeightToSpawnNewStages;
    [SerializeField] float heightBetweenSpawnOfStages;
    [SerializeField] StageGenerator stageGenerator;
    bool stunned = false;
    [SerializeField] float stunTime;
    int maxHeight = 0;
    [SerializeField] TextMeshProUGUI maxHeightText;
    [SerializeField] TextMeshProUGUI distanceToLavaText;
    [SerializeField] GameObject endgamePanel;
    [SerializeField] TextMeshProUGUI maxHeightEndscreenText;

    void Start()
    {
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((int)transform.localPosition.y > maxHeight)
        {
            maxHeight = (int)transform.localPosition.y;
            maxHeightText.text = maxHeight.ToString();
        }
        if (transform.position.y > nextHeightToSpawnNewStages)
        {
            stageGenerator.InstantiateStagePiece();
            nextHeightToSpawnNewStages += heightBetweenSpawnOfStages;
        }
        if (stunned)
        {
            if (leftInstantiatedRope)
            {
                Destroy(leftInstantiatedRope.gameObject);
            }
            if (rightInstantiatedRope)
            {
                Destroy(rightInstantiatedRope.gameObject);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (leftInstantiatedRope)
            {
                Destroy(leftInstantiatedRope.gameObject);
            }
            leftInstantiatedRope = Instantiate(springRopePrefab, leftArm.position, Quaternion.identity);
            leftInstantiatedRope.Initialize(leftArm.up, rb);
        }
        if (leftInstantiatedRope && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Mouse0)))
        {
            Destroy(leftInstantiatedRope.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (rightInstantiatedRope)
            {
                Destroy(rightInstantiatedRope.gameObject);
            }
            rightInstantiatedRope = Instantiate(springRopePrefab, rightArm.position, Quaternion.identity);
            rightInstantiatedRope.Initialize(rightArm.up, rb);
        }
        if (rightInstantiatedRope && (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Mouse1)))
        {
            Destroy(rightInstantiatedRope.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            endgamePanel.SetActive(true);
            Time.timeScale = 0.0f;
            maxHeightEndscreenText.text = "You traveled " + maxHeight.ToString();
            StartCoroutine(TakeGameoverInput());
        }
        if (collision.gameObject.CompareTag("StunningWaterfall") && stunned == false)
        {
            stunned = true;
            StartCoroutine(StunnedCooldown());
            //play stunned animation
        }
    }

    IEnumerator StunnedCooldown()
    {
        yield return new WaitForSeconds(stunTime);
        stunned = false;
        //stop stunned animation
    }

    public void UpdateDistanceToLavaText(float lavaPosY)
    {
        distanceToLavaText.text = ((int)(Mathf.Abs(lavaPosY - transform.position.y))).ToString(); //lol
    }

    IEnumerator TakeGameoverInput()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                SceneManager.LoadScene("Gameplay");
            }
            yield return null;
        }
    }

}
