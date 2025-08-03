using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public Material[] skyboxes;
    public GameObject[] uiObjectsList;
    public GameObject uiAbout;
    public GameObject mainMenuUi;
    private void Start()
    {
        // Set the skybox material
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
        }



    }

    public void StartGame()
    {
        // Load the first scene in the build settings
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    public void OpenUi(GameObject uiObject)
    {
        foreach (GameObject obj in uiObjectsList)
        {
                obj.SetActive(false); // Close other UI objects
        }
        uiObject.SetActive(true);
    }

    public void CloseUi()
    {
        foreach (GameObject obj in uiObjectsList)
        {
            obj.SetActive(false); // Close other UI objects
        }
        mainMenuUi.SetActive(true);
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        
        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
