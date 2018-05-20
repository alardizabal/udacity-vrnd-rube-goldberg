using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallReset : MonoBehaviour
{
    public ObjectMenuManager objectMenuManager;
    public StarManager starManager;
    public GameObject pedestal;
    public GameObject ball;
    public GameObject messageBoard;
    public bool isCheating;
    private Transform ballResetPosition;
    public bool wasThrownFromOutsidePlatform = false;
    public bool isOutsidePlatform;

    void Start()
    {
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
            wasThrownFromOutsidePlatform = false;
            starManager.ResetStars();
            isCheating = false;
        }
        else if (collision.collider.CompareTag("Goal") && !wasThrownFromOutsidePlatform)
        {
            Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            ball.transform.position = ballResetPosition.position;
            if (starManager.HasCollectedAllStars())
            {
                LoadLevel();
            }
            else
            {
                starManager.ResetStars();
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
        } else if (collider.gameObject.CompareTag("Platform"))
        {
            isOutsidePlatform = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Platform"))
        {
            isOutsidePlatform = true;
            if (ball.transform.parent != null)
            {
                ball.GetComponent<Renderer>().material.color = Color.red;
                isCheating = true;
            }
        }
    }

    void LoadLevel()
    {
        objectMenuManager.ConfigureScene();
        string currentLevel = SceneManager.GetActiveScene().name;
        if (currentLevel == "Level 1")
        {
            SteamVR_LoadLevel.Begin("Level 2");
        }
        else if (currentLevel == "Level 2")
        {
            SteamVR_LoadLevel.Begin("Level 3");
        }
        else if (currentLevel == "Level 3")
        {
            SteamVR_LoadLevel.Begin("Level 4");
        }
        else if (currentLevel == "Level 4")
        {
            messageBoard.SetActive(true);
        }
        isCheating = false;
    }
}
