using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UiControllerMultiplayer : MonoBehaviour
{
    public TextField inputField;
    public Button addButton;
    public Button backButton;
    public Button startButton;
    public ScrollView playerScrollView;
    public List<string> PlayerList;
    // Start is called before the first frame update
    void Start()
    {
        PlayerList = new List<string>();

        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("BackButton");
        inputField = root.Q<TextField>("NameTextField");
        addButton = root.Q<Button>("AddButton");
        playerScrollView = root.Q<ScrollView>("PlayerScrollView");
        startButton = root.Q<Button>("StartButton");
        
        addButton.clicked += addButtonPressed;
        backButton.clicked += BackButtonPressed;
        startButton.clicked += StartButtonPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addButtonPressed()
    {
        PlayerList.Add(inputField.text);
        var label = new Label();
        label.text = inputField.text;
        playerScrollView.Add(label);
    }

    void BackButtonPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void StartButtonPressed()
    {
        ModeController.PlayerList = PlayerList;
        SceneManager.LoadScene("JengaAr");
    }
}
