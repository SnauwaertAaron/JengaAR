using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UiControllerInGame : MonoBehaviour
{
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("BackButton");

        backButton.clicked += BackButtonPressed;
    }

    void BackButtonPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
