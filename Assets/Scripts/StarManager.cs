using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{

    public List<GameObject> stars;

    // Use this for initialization
    void Start()
    {

    }

    public void ResetStars()
    {
        foreach (GameObject star in stars)
        {
            star.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
