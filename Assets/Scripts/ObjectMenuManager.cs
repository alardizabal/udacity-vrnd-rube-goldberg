using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectMenuManager : MonoBehaviour
{
    public SteamVR_LoadLevel loadLevel;
    public List<GameObject> objectList; // handled automatically at start
    public List<GameObject> objectPrefabList; // set manually in inspector and MUST match order of scene menu objects
    public List<Text> objectTextList;
    private List<string> originalObjectTextList = new List<string>();
    public int currentObject = 0;
    private Dictionary<int, int> objectLimits;
    private string currentScene;

    void Start()
    {
        foreach (Text text in objectTextList)
        {
            originalObjectTextList.Add(text.text);
        }
        ConfigureScene();
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
    }

    public void ConfigureScene()
    {
        objectLimits = new Dictionary<int, int>();
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level 1")
        {
            objectLimits.Add(0, 4);
            objectLimits.Add(1, 4);
            objectLimits.Add(2, 4);
            objectLimits.Add(3, 4);
        }
        else if (currentScene == "Level 2")
        {
            objectLimits.Add(0, 3);
            objectLimits.Add(1, 2);
            objectLimits.Add(2, 2);
            objectLimits.Add(3, 1);
        }
        else if (currentScene == "Level 3")
        {
            objectLimits.Add(0, 2);
            objectLimits.Add(1, 1);
            objectLimits.Add(2, 2);
            objectLimits.Add(3, 3);
        }
        else if (currentScene == "Level 4")
        {
            objectLimits.Add(0, 1);
            objectLimits.Add(1, 2);
            objectLimits.Add(2, 1);
            objectLimits.Add(3, 2);
        }

        int i = 0;
        foreach (Text textLabel in objectTextList)
        {
            textLabel.text = originalObjectTextList[i] + "  (" + objectLimits[i] + ")";
            i++;
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
        if (objectLimits[currentObject] != 0)
        {
            objectLimits[currentObject]--;
            objectTextList[currentObject].text = originalObjectTextList[currentObject] + "  (" + objectLimits[currentObject] + ")";
            Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
        }
    }
}
