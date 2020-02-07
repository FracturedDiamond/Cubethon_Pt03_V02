using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool replay = false;

    public Text replayText;
    public Text skipText;

    bool gameHasEnded = false;                  // Initialize gameHasEnded to 'false'
    public float restartDelay = 1f;             // Initialize restartDelay to '1f'
    public GameObject completeLevelUI;          // Declare completeLevelUI gameObject
    GameObject player;


    public void Start()
    {
        replayText.text = "";
        skipText.text = "";
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if ( CommandLog.commands.Count > 0 )
        {
            replay = true;
            restartDelay = Time.timeSinceLevelLoad;
        }

    }

    public void Update()
    {
        if (replay == true)
        {
            InstantReplay();
        }
    }

    public void InstantReplay()
    {
        if (CommandLog.commands.Count == 0) { return; }         // If there are no commands no need

        Command m_Command = CommandLog.commands.Peek();         // New instance of command script (Looking inside command log queue)

        if ( Time.timeSinceLevelLoad >= m_Command.timeStamp)    // If the timestamp of command from log is <= time since level loaded
        {
            replayText.text = "Instant Replay";
            skipText.text = "Press 'Space' to skip";
            
            if (Input.GetKey(KeyCode.Space))
            {
                CommandLog.commands.Clear();
                Restart();
            }

            if (CommandLog.commands.Count == 0) { return; }

            m_Command = CommandLog.commands.Dequeue();          // Dequeueing
            m_Command.m_rb = player.GetComponent<Rigidbody>();  // Set rigidbody

            Invoker m_Invoker = new Invoker();                  // Make new invoker
            m_Invoker.SetCommand(m_Command);                    // Call the functions (make it move)
            m_Invoker.ExecuteCommandWithoutEnqueue(m_Command);
        }
    }


    public void CompleteLevel ()                // Function for completing a level
    {
        completeLevelUI.SetActive(true);        // Allow completeLevelUI to be seen
    }




    public void EndGame ()                      // Function to end the game -- public so other scripts can see
    {
        if (gameHasEnded == false)              // Check to see if game has ended yet
        {
            gameHasEnded = true;                // set gameHasEnded to 'true'

            Invoke("Restart", restartDelay);    // Restart the game
        }
    }

    void Restart ()                             // Function to restart the game
    {
        // Reload the active scene (i.e. if you are on level 02 it will restart level 02)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
