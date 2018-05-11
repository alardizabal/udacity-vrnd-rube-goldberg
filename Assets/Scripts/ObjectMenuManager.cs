using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMenuManager : MonoBehaviour
{
    public SteamVR_LoadLevel loadLevel;
    public List<GameObject> objectList; // handled automatically at start
    public List<GameObject> objectPrefabList; // set manually in inspector and MUST match order of scene menu objects
    public int currentObject = 0;
    private Dictionary<int, int> objectLimits = new Dictionary<int, int>();
    private string currentScene;
    
    void Start()
    {
        configureScene();
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
    }

    void configureScene()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level 1")
        {
            objectLimits.Add(0, 3);
            objectLimits.Add(1, 3);
            objectLimits.Add(2, 3);
            objectLimits.Add(3, 3);
        }
    }

    public void HideObjects()
    {
        objectList[currentObject].SetActive(false);
    }

    public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }

    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }

    public void SpawnCurrentObject()
    {
        Debug.Log("Spawn object");
        Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
    }
}
