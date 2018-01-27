using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages & sequences all game stages / waves
 * @class StageManager
 */
public class StageManager : MonoBehaviour
{
    public GameObject       musicPlayer;
    public bool             paused;
    public StageDatabase    database;
    public int              hpGetBack;

    private enum ManagerState
    {
        GameNone,
        GameBegin,
        GameRunning,
        GamePaused,
        GameEnd,
    }

    private enum StageState
    {
        StageNone,
        StageBegin,
        StageRunning,
        StageEnd,
        StageTimeout,
    }

    // States
    ManagerState managerState = ManagerState.GameNone;
    StageState   stageState   = StageState.StageNone;

    // Attributes
    private WaveManager waveManager       = null;
    private Stage       currentStage      = null;
    private int         currentStageIndex = 0;

    // Coroutines

    /**
     * Start method, not used
     */
    void Start()
    {
        Init();
    }

    /**
     * Initializes the stage manager
     */
    public void Init()
    {
        if (!database)
        {
            Debug.LogError("Database reference null, aborting...");
        }

        // Startup debug print
        Debug.Log("Stage manager initialization : ");
        Debug.Log("    - Database name : "  + database.ToString());
        Debug.Log("    - Stage count : "    + database.stages.Count.ToString());
        Debug.Log("    - Total waves : "    + database.GetTotalWavesCount().ToString());
        Debug.Log("    - Total duration : " + database.GetTotalDuration().ToString());

        // Buffer first stage
        if (database.stages.Count == 0)
        {
            Debug.LogError("No stage found, aborting...");
        }

        // Instanciating the wave manager
        if(waveManager == null)
        {
            waveManager = new WaveManager();
        }

        managerState = ManagerState.GameBegin;
        Debug.Log("Stage manager successfully initialized !");

        musicPlayer = GameObject.Find("MusicPlayer");
    }

    /**
     * Update the stage manager
     */
    void Update()
    {
        if(paused)
        {
            return;
        }

        // Manager switch state machine 
        switch(managerState)
        {
            case ManagerState.GameBegin:   OnGameBegin();   break;
            case ManagerState.GameRunning: OnGameRunning(); break;
            case ManagerState.GamePaused:  OnGamePaused();  break;
            case ManagerState.GameEnd:     OnGameEnd();     break;
            default: break;
        }
    }

    /**
     * Called when the game begins
     * Buffer the first stage
     */
    private void OnGameBegin()
    {
        currentStageIndex = 0;
        currentStage      = database.stages[currentStageIndex];

        // Going to the next state
        stageState   = StageState.StageBegin;
        managerState = ManagerState.GameRunning;
    }

    /**
     * Called when the game is running
     */
    private void OnGameRunning()
    {
        // Stage switch state machine 
        switch (stageState)
        {
            case StageState.StageBegin:   OnStageBegin();   break;
            case StageState.StageRunning: OnStageRunning(); break;
            case StageState.StageEnd:     OnStageEnd();     break;
            case StageState.StageTimeout: OnStageTimeout(); break;
            default: break;
        }
    }

    /**
     * Called when the game is paused
     */
    public void OnGamePaused()
    {
        if (waveManager != null)
        {
            Debug.Log("Stage Manager : Game paused");
            waveManager.OnGamePaused();
        }
    }

    /**
     * Called when the game resumes
     */
    public void OnGameResumed()
    {
        if(waveManager != null)
        {
            Debug.Log("Stage Manager : Game resumed");
            waveManager.OnGameResumed();
        }
    }

    /**
     * Called when the game ends
     */
    private void OnGameEnd()
    {
        // None
    }

    /**
     * Called when the stage begins
     */
    private void OnStageBegin()
    {
        waveManager.Init(currentStage, transform);

        // Going to the next state
        stageState = StageState.StageRunning;

        // Displays message to user
        MessageManager.Message(currentStage.StageName, 3);

        Debug.Log("Stage Manger : New Stage loaded");

        // "A table" management
        switch (currentStageIndex)
        {
            case 0: AkSoundEngine.PostEvent("A_Table_1", musicPlayer); break;
            case 1: AkSoundEngine.PostEvent("A_Table_2", musicPlayer); break;
            case 3: AkSoundEngine.PostEvent("A_Table_3", musicPlayer); break;
            default: break;
        }
    }

    /**
     * Called when the stage is running
     */
    private void OnStageRunning()
    {
        waveManager.Update(Time.deltaTime);
        if(waveManager.GetManagerState() == WaveManager.ManagerState.ManagerDone)
        {
            // Going to the next state
            stageState = StageState.StageEnd;
        }
    }

    /**
     * Called when the stage ends
     */
    private void OnStageEnd()
    {
        // Going to the next state
        stageState = StageState.StageTimeout;
        StartCoroutine(StageTimeOut(currentStage.StageTimeout));

        MessageManager.Message("Stage clear", 3);
        AkSoundEngine.PostEvent("Stage_Cleared", musicPlayer);

        // Refill player hp
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().hitPoint = hpGetBack;
        Debug.Log("Stage Manger : Stage ended");
    }

    /**
     * Called during the stage timeout
     */
    private void OnStageTimeout()
    {
        // None
    }

    /**
     * Wais the timeout and sets up the next state
     */
    private IEnumerator StageTimeOut(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        // Getting next state
        currentStageIndex++;
        if (currentStageIndex >= database.stages.Count)
        {
            // No more stage
            stageState   = StageState.StageNone;
            managerState = ManagerState.GameEnd;

            GameManager gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.currentScore = ScoreManager.GetScore();
            gameManager.OnGameWin();
        }
        else
        {
            stageState   = StageState.StageBegin;
            currentStage = database.stages[currentStageIndex];
        }
    }
}
