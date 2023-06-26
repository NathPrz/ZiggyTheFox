using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SDD.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : Manager<LevelManager>
{
	#region Manager implementation
	protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
    #endregion

    [SerializeField] TMP_Text m_ScoreValue;
    [SerializeField] TMP_Text m_BestScoreValue;

    public override void SubscribeEvents()
	{
		base.SubscribeEvents();

        //Score Item
        EventManager.Instance.AddListener<ItemHasBeenHitEvent>(ItemHasBeenHit);

        // Level Completed
        EventManager.Instance.AddListener<LevelCompletedEvent>(LevelCompleted);
    }

	public override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();

        //Score Item
        EventManager.Instance.RemoveListener<ItemHasBeenHitEvent>(ItemHasBeenHit);

        // Level Completed
        EventManager.Instance.RemoveListener<LevelCompletedEvent>(LevelCompleted);
    }

	protected override void GamePlay(GamePlayEvent e)
	{
	}

	protected override void GameMenu(GameMenuEvent e)
	{
	}
    private void LevelCompleted(LevelCompletedEvent e)
    {
        m_ScoreValue.text = GameManager.Instance.Score.ToString();
        m_BestScoreValue.text = GameManager.Instance.BestScore.ToString();
    }


    #region Score Events
    private void ItemHasBeenHit(ItemHasBeenHitEvent e)
	{
        IDestroyable destroyable = e.eItem.GetComponent<IDestroyable>();
        if (null != destroyable)
            destroyable.Kill();
    }
    #endregion
}
