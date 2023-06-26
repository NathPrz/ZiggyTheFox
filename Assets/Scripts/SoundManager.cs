using SDD.Events;
using System.Collections;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{

    [SerializeField] AudioClip m_ScoreUpAudio;
    [SerializeField] AudioClip m_WaterSplashAudio;
    [SerializeField] AudioClip m_PlayerHitSound;
    [SerializeField] AudioClip m_LevelCompletedAudio;
    [SerializeField] AudioClip m_GameOverAudio;

    AudioSource m_audioSource;

    #region Manager implementation
    override protected IEnumerator Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        yield return StartCoroutine(InitCoroutine());
    }

    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }
    #endregion

    public override void SubscribeEvents()
    {
        base.SubscribeEvents();

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

        //Score Item
        EventManager.Instance.RemoveListener<ItemHasBeenHitEvent>(ItemHasBeenHit);

        // Fall in water
        EventManager.Instance.RemoveListener<PlayerFellIntoWaterEvent>(PlayerFellIntoWater);

        // Player gets hit
        EventManager.Instance.RemoveListener<PlayerGotHitEvent>(PlayerGotHit);

        // Level completed
        EventManager.Instance.RemoveListener<LevelCompletedEvent>(LevelCompleted);
    }

    protected override void GamePlay(GamePlayEvent e)
    {
    }

    protected override void GameMenu(GameMenuEvent e)
    {
    }


    #region Score Events
    private void ItemHasBeenHit(ItemHasBeenHitEvent e)
    {
        m_audioSource.clip = m_ScoreUpAudio;
        m_audioSource.Play();
    }
    #endregion

    private void PlayerFellIntoWater(PlayerFellIntoWaterEvent e)
    {
        m_audioSource.clip = m_WaterSplashAudio;
        m_audioSource.Play();
    }

    private void PlayerGotHit(PlayerGotHitEvent e)
    {
        m_audioSource.clip = m_PlayerHitSound;
        m_audioSource.Play();

    }

    private void LevelCompleted(LevelCompletedEvent e)
    {
        m_audioSource.clip = m_LevelCompletedAudio;
        m_audioSource.Play();
    }

    protected override void GameOver(GameOverEvent e)
    {
        m_audioSource.clip = m_GameOverAudio;
        m_audioSource.Play();
    }

}
