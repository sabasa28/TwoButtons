using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    bool leftFootCompressed = false;
    bool rightFootCompressed = false;
    [SerializeField] float feetCompressSpeed;
    [SerializeField] float feetDecompressSpeed;
    [SerializeField] float feetForce;
    [SerializeField] Foot leftFootCollisionChecker;
    [SerializeField] Foot rightFootCollisionChecker;
    Vector3 leftFootPositionOffset;
    Vector3 rightFootPositionOffset;
    Quaternion leftFootInitialRot;
    Quaternion rightFootInitialRot;
    float timeStill = 0.0f;
    Vector3 lastPos;
    void Start()
    {
        lastPos = transform.position;
        //leftFootPositionOffset = transform.position - leftFoot.transform.position;
        //rightFootPositionOffset = transform.position - rightFoot.transform.position;
        //leftFootInitialRot = leftFoot.rotation;
        //rightFootInitialRot = rightFoot.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !leftFootCollisionChecker.IsColliding() && !rightFootCollisionChecker.IsColliding() && transform.position == lastPos)
        {
            timeStill += Time.deltaTime;
            if (timeStill > 1.0f)
            {
                leftFoot.localPosition = new Vector3(leftFoot.localPosition.x, -leftFoot.localPosition.y, leftFoot.localPosition.z);
                leftFoot.localEulerAngles = new Vector3(leftFoot.localEulerAngles.x, leftFoot.localEulerAngles.y, 180 - leftFoot.localEulerAngles.z);
                rightFoot.localPosition = new Vector3(rightFoot.localPosition.x, -rightFoot.localPosition.y, rightFoot.localPosition.z);
                rightFoot.localEulerAngles = new Vector3(rightFoot.localEulerAngles.x, rightFoot.localEulerAngles.y, 180 - rightFoot.localEulerAngles.z);
            }
        }
        else
        {
            timeStill = 0.0f;
        }
        lastPos = transform.position;
        //leftFoot.position = transform.position - leftFootPositionOffset;
        //rightFoot.position = transform.position - rightFootPositionOffset;
        //leftFoot.rotation = leftFootInitialRot;
        //rightFoot.rotation = rightFootInitialRot;
        if (Input.GetKey(KeyCode.A))
        {
            leftFootCompressed = true;
            leftFoot.transform.localScale = new Vector3(leftFoot.transform.localScale.x, leftFoot.transform.localScale.y - feetCompressSpeed * Time.deltaTime, leftFoot.transform.localScale.z); 
            if (leftFoot.transform.localScale.y <= 0.1f)
            {
                leftFoot.transform.localScale = new Vector3(leftFoot.transform.localScale.x, 0.1f, leftFoot.transform.localScale.z);
            }
        }
        else if (leftFoot.transform.localScale.y < 1.0f)
        {
            if (leftFootCompressed && leftFootCollisionChecker.IsColliding())
            {
                playerRigidbody.AddForce((Vector2)(leftFoot.transform.up) * (1 - leftFoot.transform.localScale.y) * feetForce);
                Debug.Log("Added force by left foot: " + (Vector2)(leftFoot.transform.up) * (1 - leftFoot.transform.localScale.y) * feetForce);
                Debug.Log("Magnitude: " + ((Vector2)(transform.position - leftFoot.transform.position).normalized * (1 - leftFoot.transform.localScale.y) * feetForce).magnitude);
                leftFootCompressed = false;
            }
            leftFoot.transform.localScale = new Vector3(leftFoot.transform.localScale.x, leftFoot.transform.localScale.y + feetDecompressSpeed * Time.deltaTime, leftFoot.transform.localScale.z);
            if (leftFoot.transform.localScale.y > 1.0f)
            {
                leftFoot.transform.localScale = Vector3.one;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightFootCompressed = true;
            rightFoot.transform.localScale = new Vector3(rightFoot.transform.localScale.x, rightFoot.transform.localScale.y - feetCompressSpeed * Time.deltaTime, rightFoot.transform.localScale.z);
            if (rightFoot.transform.localScale.y <= 0.1f)
            {
                rightFoot.transform.localScale = new Vector3(rightFoot.transform.localScale.x, 0.1f, rightFoot.transform.localScale.z);
            }
        }
        else if (rightFoot.transform.localScale.y < 1.0f)
        {
            if (rightFootCompressed && rightFootCollisionChecker.IsColliding())
            {
                playerRigidbody.AddForce((Vector2)(rightFoot.transform.up) * (1 - rightFoot.transform.localScale.y) * feetForce);
                Debug.Log("Added force by right foot: " + (Vector2)(rightFoot.transform.up) * (1 - rightFoot.transform.localScale.y) * feetForce);
                Debug.Log("Magnitude: " + ((Vector2)(transform.position - rightFoot.transform.position).normalized * (1 - rightFoot.transform.localScale.y) * feetForce).magnitude);
                rightFootCompressed = false;
            }
            rightFoot.transform.localScale = new Vector3(rightFoot.transform.localScale.x, rightFoot.transform.localScale.y + feetDecompressSpeed * Time.deltaTime, rightFoot.transform.localScale.z);
            if (rightFoot.transform.localScale.y > 1.0f)
            {
                rightFoot.transform.localScale = Vector3.one;
            }
        }
    }


}
