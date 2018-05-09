using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    public GameObject pedestal;
    public GameObject ball;
    private Transform ballResetPosition;

    //private LevelProgressionManager levelProgressionManager;

    void Start()
    {
        for (int i = 0; i < this.pedestal.transform.childCount; i++)
        {
            Transform child = this.pedestal.transform.GetChild(i);
            if (child.name == "BallStartPosition")
            {
                this.ballResetPosition = child.transform;
                break;
            }
        }

        //this.levelProgressionManager = this.GetComponent<LevelProgressionManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Rigidbody ballRigidBody = this.ball.GetComponent<Rigidbody>();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            this.ball.transform.position = this.ballResetPosition.position;
            Debug.Log("Ground");

            //this.levelProgressionManager.ResetLevelProgression();
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
        SteamVR_LoadLevel.Begin("Level 2");
    }
}
