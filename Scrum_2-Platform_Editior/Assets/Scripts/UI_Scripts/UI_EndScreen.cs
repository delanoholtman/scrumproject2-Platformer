using System.ComponentModel;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI_EndScreen : MonoBehaviour
{

    Label time;
    Button Back;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        time = root.Q<Label>("Time");
        Back = root.Q<Button>("back");

        Back.clicked += OnBackClicked;
    }

    void Update()
    {
        time.text = Mathf.Floor(GameManager.Instance.Timer).ToString();
    }

    void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}