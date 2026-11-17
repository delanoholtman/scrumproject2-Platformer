using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    Vector2 pos;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        foreach (var uiDoc in FindObjectsOfType<UIDocument>())
        {
            if (uiDoc != null && uiDoc.rootVisualElement != null)
            {
                var focused = uiDoc.rootVisualElement.panel?.focusController?.focusedElement;
                if (focused != null && (focused.GetType().Name.Contains("TextField") || focused.GetType().Name.Contains("TextInput")))
                {
                    return;
                }
            }
        }

        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected.GetComponent<TMPro.TMP_InputField>() != null ||
                selected.GetComponent<UnityEngine.UI.InputField>() != null)
            {
                return;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            pos += new Vector2(0, 5) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos += new Vector2(0, -5) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos += new Vector2(-5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos += new Vector2(5, 0) * Time.deltaTime;
        }

        transform.position = new Vector3(pos.x, pos.y, -10);
    }
}