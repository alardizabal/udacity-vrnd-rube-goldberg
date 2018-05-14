using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    public ObjectMenuManager objectMenuManager;
    public StarManager starManager;
    public GameObject pedestal;
    public GameObject ball;
    public GameObject messageBoard;
    public bool isCheating;
    private Transform ballResetPosition;
    private string currentLevel;

    void Start()
    {
        currentLevel = "Level 1";
        for (int i = 0; i < pedestal.transform.childCount; i++)
        {
            Transform child = pedestal.transform.GetChild(i);
            if (child.name == "BallStartPosition")
            {
                ballResetPosition = child.transform;
                ball.GetComponent<Renderer>().material.color = Color.white;
                break;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            ball.transform.position = ballResetPosition.position;
            ball.GetComponent<Renderer>().material.color = Color.white;
            starManager.ResetStars();
            isCheating = false;
        }
        else if (collision.collider.CompareTag("Goal"))
        {
            Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            ball.transform.position = ballResetPosition.position;
            if (starManager.hasCollectedAllStars())
            {
                LoadLevel();
            }
            else
            {
                ball.transform.position = ballResetPosition.position;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Star"))
        {
            GameObject starObject = collider.gameObject;
            starObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Platform"))
        {
            if (ball.transform.parent != null)
            {
                ball.GetComponent<Renderer>().material.color = Color.red;
                isCheating = true;
            }
        }
    }

    void LoadLevel()
    {
        objectMenuManager.configureScene();
        if (currentLevel == "Level 1")
        {
            currentLevel = "Level 2";
        }
        else if (currentLevel == "Level 2")
        {
            currentLevel = "Level 3";
        }
        else if (currentLevel == "3")
        {
            currentLevel = "Level 4";
        }
        else if (currentLevel == "4")
        {
            messageBoard.SetActive(true);
        }
        isCheating = false;
        if (currentLevel != "Level 4")
        {
            SteamVR_LoadLevel.Begin(currentLevel);
        }
    }
}
