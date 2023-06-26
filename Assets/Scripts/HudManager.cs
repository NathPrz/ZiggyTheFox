using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SDD.Events;
using TMPro;

public class HudManager : Manager<HudManager>
{

    [Header("HudManager")]
    #region Labels & Values
    [SerializeField] TMP_Text m_ScoreValue;
    [SerializeField] TMP_Text m_TimeValue;
    [SerializeField] TMP_Text m_BestScoreValue;
    #endregion

    void SetStatisticsTexts(float score, float time, float bestScore)
    {
        m_ScoreValue.text = score.ToString();


        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        m_TimeValue.text = string.Format("{0:0} : {1:00}", minutes, seconds);

        m_BestScoreValue.text = bestScore.ToString();
    }

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
	#endregion

	#region Callbacks to GameManager events
	protected override void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
        SetStatisticsTexts(e.eScore, e.eTime, e.eBestScore);
    }
	#endregion

}