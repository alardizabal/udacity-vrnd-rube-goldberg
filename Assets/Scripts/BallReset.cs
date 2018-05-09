using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    public StarManager starManager;
    public GameObject pedestal;
    public GameObject ball;
    private Transform ballResetPosition;
    private string currentLevel;

    void Start()
    {
        currentLevel = "Level 1";
        for (int i = 0; i < this.pedestal.transform.childCount; i++)
        {
            Transform child = this.pedestal.transform.GetChild(i);
            if (child.name == "BallStartPosition")
            {
                this.ballResetPosition = child.transform;
                break;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Rigidbody ballRigidBody = this.ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            this.ball.transform.position = this.ballResetPosition.position;
            starManager.ResetStars();
            Debug.Log("Ground");
        }
        else if (collision.collider.CompareTag("Star"))
        {
            GameObject starObject = collision.gameObject;
            starObject.SetActive(false);
        }
        else if (collision.collider.CompareTag("Goal"))
        {
            Rigidbody ballRigidBody = this.ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            this.ball.transform.position = this.ballResetPosition.position;
            LoadLevel();
            Debug.Log("Goal!");
        }
    }

    void LoadLevel()
    {
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
        SteamVR_LoadLevel.Begin(currentLevel);
    }
}
