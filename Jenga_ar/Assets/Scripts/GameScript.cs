using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public static GameObject SelectedBlock = null;
    public static GameObject DeleteBlock = null;

    public Material Unselected;
    public Material Selected;

    private static float y = 30.7f;
    private Vector3 Spawnposition = new Vector3(4.5f, 30.7f, 0f); //4.5f, 30.7f, -8f

    private GameObject PreviouslySelectedBlock = null;
    GameObject uiGameOver = null;

    private int layerRot = 0;
    private int layerCount = 0;

    //playercount zal grootte zijn van list van players
    private int playerCount = 0;
    private int playerNumber = 0;

    private List<string> playerList = ModeController.PlayerList;

    private bool gameEnd = false;
    private int endCount = 0;

    private string gameMode = ModeController.mode;
    private int singlepayerScore = 0;
    private int camRotationCount = 0;

    public Button takeOutButton;
    public Button retryButton;
    public Button continueButton;
    public Button rotateButton;
    public Label modeLabel;
    public Label footerNumberOrName;
    public Label footerScoreOrPlayer;
    public Label gameOverLabel;

    Camera arCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main;

        GameObject uiDoc = GameObject.FindWithTag("UIDoc");
        var root = uiDoc.GetComponent<UIDocument>().rootVisualElement;
        takeOutButton = root.Q<Button>("TakeOutButton");
        rotateButton = root.Q<Button>("RotateButton");
        modeLabel = root.Q<Label>("modeLabel");
        footerNumberOrName = root.Q<Label>("footerNumberOrName");
        footerScoreOrPlayer = root.Q<Label>("footerScoreOrPlayer");
        takeOutButton.clicked += TakeBlockOut;
        rotateButton.clicked += RotateTower;

        modeLabel.text = ModeController.mode;

        uiGameOver = GameObject.FindWithTag("GameOverScreen");
        uiGameOver.SetActive(false);
        if(gameMode == "Multiplayer")
        {
            playerCount = playerList.Count;
            footerScoreOrPlayer.text = "Player: ";
            footerNumberOrName.text = playerList[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("s"))
        {
            singlepayerScore = 0;
            gameMode = "Singleplayer";
            Debug.Log("Singleplayer");
        }

        if (Input.GetKeyDown("m"))
        {
            gameMode = "Multiplayer";
            Debug.Log("Multiplayer");
        }


        // checking if game can still be played
        if (gameEnd == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Touch touch = Input.GetTouch(0);
                //// dertermine what block is selected
                //Ray ray = arCamera.ScreenPointToRay(touch.position);

                // dertermine what block is selected
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    SelectedBlock = hit.transform.gameObject;
                }

                // Reseting last selected block
                if (PreviouslySelectedBlock != null)
                {
                    PreviouslySelectedBlock.GetComponent<MeshRenderer>().material = Unselected;
                    PreviouslySelectedBlock.tag = "NotSelected";

                    // Give selected block select color
                    SelectedBlock.GetComponent<MeshRenderer>().material = Selected;

                    // Give selected block selected tag
                    SelectedBlock.tag = "Selected";
                }

                // Save previous block
                PreviouslySelectedBlock = SelectedBlock;

            }

            // Delete selected block
            if (Input.GetKeyDown("d"))
            {

            }
        }

        // If the game is done
        else if (gameEnd == true)
        {
            


        }
         
    }

    void TakeBlockOut()
    {
        footerScoreOrPlayer.text = "Balancing tower...";
        footerNumberOrName.text = "";
        Debug.Log("Er is op het knopje geklikt :)");
                // Reseting last selected block
                if (PreviouslySelectedBlock != null)
                {
                    PreviouslySelectedBlock.GetComponent<MeshRenderer>().material = Unselected;
                }

                // Find Selected block
                DeleteBlock = GameObject.FindGameObjectWithTag("Selected");
                Debug.Log(DeleteBlock.name);

                //determine rotation
                Vector3 angles = DeleteBlock.transform.rotation.eulerAngles;
                float rotation = angles.y;
  

                // rotation 0 means horizontal
                if (layerRot == 0)
                {
                    Debug.Log("Hor");
                    // determine spawnposition based on layercount
                    switch (layerCount)
                    {
                        case 0:
                            Spawnposition = new Vector3(4.5f, y, -5); ;
                            break;
                        case 1:
                            Spawnposition = new Vector3(4.5f, y, -8); ;
                            break;
                        case 2:
                            Spawnposition = new Vector3(4.5f, y, -11);
                            break;
                    }

                    // If block is oriented in the right position just move, else rotate and move
                    if (rotation >= 0 && rotation < 45)
                    {
                        DeleteBlock.transform.position = Spawnposition;
                    }
                    else
                    {
                        DeleteBlock.transform.position = Spawnposition;
                        DeleteBlock.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    // Upp amount of blocks on the top layer
                    layerCount++;
                }

                // Same but for vertical layer
                else
                {
                    // determine spawnposition based on layercount
                    switch (layerCount)
                    {
                        case 0:
                            Spawnposition = new Vector3(1.5f, y, -8);
                            break;
                        case 1:
                            Spawnposition = new Vector3(4.5f, y, -8);
                            break;
                        case 2:
                            Spawnposition = new Vector3(7.5f, y, -8);
                            break;
                    }
                    Debug.Log("Vert");
                    if (rotation >= 0 && rotation < 45)
                    {
                        DeleteBlock.transform.position = Spawnposition;
                        DeleteBlock.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else
                    {
                        DeleteBlock.transform.position = Spawnposition;
                    }
                    layerCount++;
                }

                // If 3 blocks on top layer initiate different top layer orientation
                if (layerCount == 3)
                {
                    layerCount = 0;
                    if (layerRot == 0)
                    {
                        layerRot = 1;
                        y += 1.6f;
                    }
                    else
                    {
                        layerRot = 0;
                        y += 1.6f;
                    }
                }

                // Reseting last selected block
                if (PreviouslySelectedBlock != null)
                {
                    PreviouslySelectedBlock.GetComponent<MeshRenderer>().material = Unselected;
                    PreviouslySelectedBlock.tag = "NotSelected";
                }

                takeOutButton.style.display = DisplayStyle.None;
                //Here buttons have to get disables

                if (gameMode == "Multiplayer")
                {
                    // waiting till next player can play
                    Invoke("NextPlayer", 3);
                }

                else if (gameMode == "Singleplayer")
                {
                    Invoke("WaitSinglePlayer", 0);
                    singlepayerScore += 10;
                    Debug.Log("Uw huidige score is: " + singlepayerScore);
                }
    }

    void RotateTower()
    {
        if (camRotationCount == 3)
            {
                camRotationCount = 0;
            }
            else
            {
                camRotationCount++;
            }

            switch (camRotationCount)
            {
                case 0:
                    Camera.main.transform.rotation = Quaternion.Euler(29,0,0);
                    Camera.main.transform.position = new Vector3(4.5f, 50, -45);
                    break;

                case 1:
                    Camera.main.transform.rotation = Quaternion.Euler(29, 90, 0);
                    Camera.main.transform.position = new Vector3(-34.5f, 50, -10);
                    break;

                case 2:
                    Camera.main.transform.rotation = Quaternion.Euler(29, 180, 0);
                    Camera.main.transform.position = new Vector3(4.5f, 50, 30);
                    break;

                case 3:
                    Camera.main.transform.rotation = Quaternion.Euler(29, -90, 0);
                    Camera.main.transform.position = new Vector3(44.5f, 50, -10);
                    break;
            }
    }

    void WaitSinglePlayer()
    {
        footerScoreOrPlayer.text = "Score:";
        footerNumberOrName.text = singlepayerScore.ToString();
        takeOutButton.style.display = DisplayStyle.Flex;
    }

    void NextPlayer()
    {
        // Checking if last player of the list played to reset for next round
        if (playerNumber == playerCount)
        { 
            playerNumber = 0;
        }

        // next player
        else
        {
            playerNumber++;
            if(playerNumber == 3){
                playerNumber = 0;
            }
            // Playernumber will be the index of the playerlist to get the palyername
            Debug.Log("Het is de beurt van: " + playerList[playerNumber]);
            footerNumberOrName.text = playerList[playerNumber];
            footerScoreOrPlayer.text = "Player: ";

        }
        
        // Buttons have to get enabled again
        takeOutButton.style.display = DisplayStyle.Flex;
    }

    void retryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void continueButtonPressed(){
        SceneManager.LoadScene("MainMenuScene");
    }

// Communication with trigger to know when tower has fallen
    public void EndGame()
    {
        //gameEnd = true;
        //uiGameOver.SetActive(true);
        //var gameover = uiGameOver.GetComponent<UIDocument>().rootVisualElement;
        //retryButton = gameover.Q<Button>("ReplayButton");
        //continueButton = gameover.Q<Button>("ContinueButton");
        //gameOverLabel = gameover.Q<Label>("ScoreOrLoserLabel");
        //retryButton.clicked += retryButtonPressed;
        //continueButton.clicked += continueButtonPressed;
        //// making sure endsequence is only triggered once
        //if (endCount == 0 && gameMode == "Multiplayer")
        //{
        //    // Log has to be replaced with UI messsage
        //    // Playernumber will be the index of the playerlist to get the palyername
        //    Debug.Log("Player" + playerNumber + " lost the game!");
        //    gameOverLabel.text = "Player " + playerList[playerNumber] + " lost the game!";
        //}

        //if (endCount == 0 && gameMode == "Singleplayer")
        //{
        //    Debug.Log("GAME OVER! Je behaalde score is: "+ singlepayerScore);
        //    gameOverLabel.text = "Score: "+ singlepayerScore;
        //    // get highscore from playerfrefs
        //    int highscore = PlayerPrefs.GetInt("highscore");

        //    // check if the played score is higher than the highscore
        //    if (highscore < singlepayerScore)
        //    {
        //        // if true then change the highscore to the played score
        //        PlayerPrefs.SetInt("highscore",singlepayerScore);
        //        Debug.Log("GAME OVER! Je hebt je highscore verbeterd naar: " + singlepayerScore);
        //    }
        //    else
        //    {   
        //        // else just leave the highscore
        //        Debug.Log("GAME OVER! De highscore is nog steeds: " + highscore);
        //    }
        //}
        //endCount++;
    }
}