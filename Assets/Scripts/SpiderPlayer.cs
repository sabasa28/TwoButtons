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
    bool overloaded = false;
    bool jetpackEquipped = false;
    Coroutine overloadCoroutine = null;
    Coroutine jetpackCoroutine = null;
    [SerializeField] float velocityCap;
    [SerializeField] float jetpackForce;
    int jetpackInput = 0;
    [SerializeField] GameObject brokenFX;
    [SerializeField] Animator anthenaAnimatorL;
    [SerializeField] Animator anthenaAnimatorR;
    [SerializeField] Animator boosterAnimator;
    [SerializeField] Animator faceAnimator;

    void Start()
    {
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (jetpackEquipped)
        {
            Vector2 jetpackDir = new Vector2(jetpackInput, 1);
            rb.MovePosition(rb.position + jetpackDir * jetpackForce * Time.fixedDeltaTime);
        }
    }
    void Update()
    {
        if (rb.velocity.magnitude > velocityCap) //cap velocity para no atravezar colliders
        {
            rb.velocity = rb.velocity.normalized * velocityCap;
        }
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
        if (jetpackEquipped)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Mouse0))
            {
                jetpackInput = -1;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse1))
            {
                jetpackInput = 1;
            }
            else
            {
                jetpackInput = 0;
            }
        }
        if (stunned || jetpackEquipped)
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
            leftInstantiatedRope.Initialize(leftArm.up, rb, overloaded);
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
            rightInstantiatedRope.Initialize(rightArm.up, rb, overloaded);
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
        brokenFX.SetActive(true);
        faceAnimator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunTime);
        stunned = false;
        brokenFX.SetActive(false);
        faceAnimator.SetBool("Stunned", false);
        //stop stunned animation
    }

    public void UpdateDistanceToLavaText(float lavaPosY)
    {
        distanceToLavaText.text = ((int)(Mathf.Abs(lavaPosY - transform.position.y))).ToString(); //lol
    }

    IEnumerator TakeGameoverInput()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                SceneManager.LoadScene("Gameplay");
            }
            yield return null;
        }
    }

    public void Overload(float activeTime)
    {
        if (overloaded)
        {
            StopCoroutine(overloadCoroutine);
        }
        else
        { 
            if (leftInstantiatedRope)
            {
                leftInstantiatedRope.EnableOverload();
            }
            if (rightInstantiatedRope)
            {
                rightInstantiatedRope.EnableOverload();
            }
        }
        overloaded = true;
        overloadCoroutine = StartCoroutine(DisableOverload(activeTime));
    }

    IEnumerator DisableOverload(float activeTime)
    {
        anthenaAnimatorL.SetBool("Boosted", true);
        anthenaAnimatorR.SetBool("Boosted", true);
        faceAnimator.SetBool("Overload", true);
        yield return new WaitForSeconds(activeTime);
        overloaded = false;
        if (leftInstantiatedRope)
        {
            leftInstantiatedRope.DisableOverload();
        }
        if (rightInstantiatedRope)
        {
            rightInstantiatedRope.DisableOverload();
        }
        anthenaAnimatorL.SetBool("Boosted", false);
        anthenaAnimatorR.SetBool("Boosted", false);
        faceAnimator.SetBool("Overload", false);
    }

    public void EquipJetpack(float activeTime)
    {
        if (jetpackEquipped)
        {
            StopCoroutine(jetpackCoroutine);
        }
        jetpackEquipped = true;
        jetpackCoroutine = StartCoroutine(DisableJetpack(activeTime));
    }

    IEnumerator DisableJetpack(float activeTime)
    {
        boosterAnimator.SetBool("Boosted", true);
        faceAnimator.SetBool("Jetpack", true);
        yield return new WaitForSeconds(activeTime);
        jetpackEquipped = false;
        rb.velocity = Vector2.zero;
        boosterAnimator.SetBool("Boosted", false);
        faceAnimator.SetBool("Jetpack", false);
    }

}
