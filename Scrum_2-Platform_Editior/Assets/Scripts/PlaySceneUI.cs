using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlaySceneUI : MonoBehaviour
{
    void Start()
    {
        UIDocument uiDoc = GetComponent<UIDocument>();
        if (uiDoc == null)
        {
            uiDoc = FindAnyObjectByType<UIDocument>();
        }

        if (uiDoc != null && uiDoc.rootVisualElement != null)
        {
            var backButton = uiDoc.rootVisualElement.Q<Button>("backbutton");
            if (backButton == null)
            {
                backButton = uiDoc.rootVisualElement.Q<Button>("BackButton");
            }

            if (backButton != null)
            {
                backButton.clicked += () =>
                {
                    SceneManager.LoadScene("switch-build-play");
                };
                Debug.Log("Terug-knop succesvol gekoppeld!");
            }
            else
            {
                Debug.LogError("Kon geen knop vinden met naam 'backbutton' in de UI!");
            }
        }
        else
        {
            Debug.LogError("Geen UIDocument gevonden in de speel-scène!");
        }
    }
}