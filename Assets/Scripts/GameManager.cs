using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SDD.Events;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum GameState { gameMenu, gamePlay, gameNextLevel, gamePause, gameOver, gameVictory }

public class GameManager : Manager<GameManager>
{
    public GameObject victoryUI;
    public Camera camerae;
    public EventSystem er;

    #region Game State
    private GameState m_GameState;
    public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }

    private bool m_GameHasToRestart = false;

    private string currentLevel;

    public int GameFirstLaunch
    {
        get { return PlayerPrefs.GetInt("FIRST_LAUNCH", 1); }
        set { PlayerPrefs.SetInt("FIRST_LAUNCH", value); }
    }
    #endregion

    //LIVES
    #region Lives
    public int maxHealth = 3;
    public int currentHealth
    {
        get { return PlayerPrefs.GetInt("HEALTH", 0); }
        set { PlayerPrefs.SetInt("HEALTH", value); }
    }

    public HealthBar healthBar;
    #endregion


    #region Score

        public float Score
        {
            get { return PlayerPrefs.GetFloat("SCORE", 0); }
            set { 
                PlayerPrefs.SetFloat("SCORE", value);
                BestScore = Mathf.Max(0, value);
            }
        }

        public float BestScore
        {
            get { return PlayerPrefs.GetFloat("BEST_SCORE", 0); }
            set { PlayerPrefs.SetFloat("BEST_SCORE", value); }
        }

        public void IncrementScore(float increment)
        {
            SetScore(Score + increment);
        }

        private void SetScore(float score)
        {
            Score = score;

            // Every 40 cherries gained, player gets a bonus
            if (Score != 0 && Score % 40 == 0) HealthBar.instance.GetLPBonus();

            EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eTime = m_Time, eScore = Score, eBestScore = BestScore});
        }

        #endregion

    #region Time

        float m_Time;

        void SetTimeScale(float newTimeScale)
        {
            Time.timeScale = newTimeScale;
        }

        float DecrementTime(float increment)
        {
            SetTime(Mathf.Max(0, m_Time - increment));
            return m_Time;
        }

        void SetTime(float newTime)
        {
            m_Time = newTime;
            EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eTime = m_Time, eScore = Score, eBestScore = BestScore });;
        }

        #endregion


    #region Events' subscription
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();

            //MainMenuManager
            EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
            EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
            EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
            EventManager.Instance.AddListener<RestartButtonClickedEvent>(RestartButtonClicked);
            EventManager.Instance.AddListener<NextLevelEvent>(NextLevel);
            EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
            EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);

            //Score Item
            EventManager.Instance.AddListener<ItemHasBeenHitEvent>(ItemHasBeenHit);


            // Fall in water
            EventManager.Instance.AddListener<PlayerFellIntoWaterEvent>(PlayerFellIntoWater);

            // Player gets hit
            EventManager.Instance.AddListener<PlayerGotHitEvent>(PlayerGotHit);

            // Level completed
            EventManager.Instance.AddListener<LevelCompletedEvent>(LevelCompleted);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();

            //MainMenuManager
            EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
            EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
            EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
            EventManager.Instance.RemoveListener<RestartButtonClickedEvent>(RestartButtonClicked);
            EventManager.Instance.AddListener<NextLevelEvent>(NextLevel);
            EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
            EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);

            //Score Item
            EventManager.Instance.RemoveListener<ItemHasBeenHitEvent>(ItemHasBeenHit);

            // Fall in water
            EventManager.Instance.RemoveListener<PlayerFellIntoWaterEvent>(PlayerFellIntoWater);

            // Player gets hit
            EventManager.Instance.RemoveListener<PlayerGotHitEvent>(PlayerGotHit);

            // Level completed
            EventManager.Instance.RemoveListener<LevelCompletedEvent>(LevelCompleted);
        }
        #endregion

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
    {
        InitNewGame(); // essentiellement pour que les statistiques du jeu soient mise à jour en HUD
        yield break;
    }
    #endregion

    #region Game flow & Gameplay
    protected override IEnumerator Start()
    {
        InitNewGame();

        currentLevel = SceneManager.GetActiveScene().name;

        // Show main menu only when the game is launched
        // Do not show it when a new scene is loaded
        if (GameFirstLaunch == 1)
        {
            GameFirstLaunch = 0;
            Menu();

        } else Play();

        return base.Start();
    }

    void Update()
    {
        if (IsPlaying)
            if (DecrementTime(Time.deltaTime) == 0)
                Over();
    }

    //Game initialization
    void InitNewGame()
    {
        // Do not reset data when level 2 is loaded
        if (currentLevel == "level1")
        {
            SetScore(0);

            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        // 3 minutes
        SetTime(3 * 60);
        healthBar.SetHealth(currentHealth);
    }
    #endregion

    #region Callbacks to Events issued by GameManager
    private void ItemHasBeenHit(ItemHasBeenHitEvent e)
    {
        IncrementScore(1);
    }

    private void PlayerFellIntoWater(PlayerFellIntoWaterEvent e)
    {
        HealthBar.instance.TakeDamage(1);
        SetScore(Mathf.Floor(Score / 2));
    }

    private void PlayerGotHit(PlayerGotHitEvent e)
    {
        HealthBar.instance.TakeDamage(1);
    }

    private void LevelCompleted(LevelCompletedEvent e)
    {
        Victory();
    }

    #endregion

    #region Callbacks to Events issued by MenuManager
    private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    private void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    private void ResumeButtonClicked(ResumeButtonClickedEvent e)
    {
        Resume();
    }

    private void RestartButtonClicked(RestartButtonClickedEvent e)
    {
        Restart();
    }

    private void EscapeButtonClicked(EscapeButtonClickedEvent e)
    {
        if (IsPlaying) Pause();
    }

    private void QuitButtonClicked(QuitButtonClickedEvent e)
    {
        GameFirstLaunch = 1;

        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region GameState methods
    private void Restart()
    {
        SceneManager.LoadScene("level1");
        InitNewGame();
    }

    private void NextLevel(NextLevelEvent e)
    {
        SceneManager.LoadScene("Level2");
    }

    private void Menu()
    {
        SetTimeScale(0);

        m_GameState = GameState.gameMenu;
        if (MusicLoopsManager.Instance) MusicLoopsManager.Instance.PlayMusic(Constants.MENU_MUSIC);
        EventManager.Instance.Raise(new GameMenuEvent());
    }

    private void Play()
    {
        if (m_GameHasToRestart) Restart();
        else
        {
            SetTimeScale(1);
            m_GameState = GameState.gamePlay;

            if (MusicLoopsManager.Instance)
                if (currentLevel == "level1") MusicLoopsManager.Instance.PlayMusic(Constants.LEVEL1_MUSIC);
                else if (currentLevel == "level2" || currentLevel == "level2bis") MusicLoopsManager.Instance.PlayMusic(Constants.LEVEL2_MUSIC);
                else MusicLoopsManager.Instance.PlayMusic(Constants.LEVELBONUS_MUSIC);

            EventManager.Instance.Raise(new GamePlayEvent());
        }
    }


    private void Pause()
    {
        if (!IsPlaying) return;

        SetTimeScale(0);
        m_GameState = GameState.gamePause;
        EventManager.Instance.Raise(new GamePauseEvent());
    }

    private void Resume()
    {
        if (IsPlaying) return;

        SetTimeScale(1);
        m_GameState = GameState.gamePlay;
        EventManager.Instance.Raise(new GameResumeEvent());
    }

    private void Victory()
    {
        SetTimeScale(0);
        m_GameState = GameState.gameVictory;
        EventManager.Instance.Raise(new GameVictoryEvent());

        m_GameHasToRestart = true;
    }

    public void Over()
    {
        SetTimeScale(0);
        m_GameState = GameState.gameOver;
        EventManager.Instance.Raise(new GameOverEvent());
        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(Constants.GAMEOVER_SFX);
        m_GameHasToRestart = true;
    }
    #endregion
}



