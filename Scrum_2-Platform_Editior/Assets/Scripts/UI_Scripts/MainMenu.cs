using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    Button start;
    Button editor;
    Button quit;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        start = root.Q<Button>("Start");
        editor = root.Q<Button>("Editor");
        quit = root.Q<Button>("Quit");

        start.clicked += OnStartClicked;
        editor.clicked += OnEditorClicked;
        quit.clicked += OnQuitClicked;
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene("LevelPlay");
    }

    void OnEditorClicked()
    {
        PlayerPrefs.SetInt("ClearLevelOnStart", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("switch-build-play");
    }

    void OnQuitClicked()
    {
        Application.Quit();
    }
}