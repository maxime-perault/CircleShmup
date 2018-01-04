using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages & sequences all game stages / waves
 * @class StageManager
 */
public class StageManager : MonoBehaviour
{
    public StageDatabase database;

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

    // TOOD
    private Stage currentStage;
    private int   currentStageIndex;

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

        managerState = ManagerState.GameBegin;
        Debug.Log("Stage manager successfully initialized !");
    }

    /**
     * Update the stage manager
     */
    void Update()
    {
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
    private void OnGamePaused()
    {
        // TODO
    }

    /**
     * Called when the game ends
     */
    private void OnGameEnd()
    {
        // TODO
    }

    /**
     * Called when the stage begins
     */
    private void OnStageBegin()
    {
        // TODO waves ?
    }

    /**
     * Called when the stage is running
     */
    private void OnStageRunning()
    {
        // TODO
    }

    /**
     * Called when the stage ends
     */
    private void OnStageEnd()
    {
        // TODO
    }

    /**
     * Called during the stage timeout
     */
    private void OnStageTimeout()
    {
        // TODO
    }
}
