using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UiControllerSinglePlayer : MonoBehaviour
{
    public Button startButton;
    public Button backButton;
    public Label highscoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("StartButton");
        backButton = root.Q<Button>("BackButton");
        highscoreLabel = root.Q<Label>("highScoreNumber");

        startButton.clicked += StartButtonPressed;
        backButton.clicked += BackButtonPressed;


        // get highscore from playerfrefs
        int highscore = PlayerPrefs.GetInt("highscore");
        highscoreLabel.text = highscore.ToString();
    }

    void StartButtonPressed() {
        SceneManager.LoadScene("JengaAr");
    }

    void BackButtonPressed() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
