using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UiControllerMainMenu : MonoBehaviour
{

    public Button singleplayerButton;
    public Button multiplayerButton;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        singleplayerButton = root.Q<Button>("SingleplayerButton");
        multiplayerButton = root.Q<Button>("MultiplayerButton");

        singleplayerButton.clicked += SingleplayerButtonPressed;
        multiplayerButton.clicked += MultiplayerButtonPressed;
    }

    void SingleplayerButtonPressed() {
        SceneManager.LoadScene("SinglePlayerScene");
        ModeController.mode = "Singleplayer";
    }

    void MultiplayerButtonPressed() {
        SceneManager.LoadScene("MultiPlayerScene");
        ModeController.mode = "Multiplayer";
    }
}
