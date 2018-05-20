using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public List<GameObject> stars;

    public void ResetStars()
    {
        foreach (GameObject star in stars)
        {
            star.SetActive(true);
        }
    }

    public bool HasCollectedAllStars()
    {
        foreach (GameObject star in stars)
        {
            if (star.activeSelf) return false;
        }
        return true;
    }
}
