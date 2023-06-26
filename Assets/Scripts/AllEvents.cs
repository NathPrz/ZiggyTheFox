using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}

public class GameStatisticsChangedEvent : SDD.Events.Event
{
	public float eTime { get; set; }
	public float eScore { get; set; }
	public float eBestScore { get; set; }
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class RestartButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class QuitButtonClickedEvent : SDD.Events.Event
{ }
#endregion



#region Score Event

public class ItemHasBeenHitEvent : SDD.Events.Event
{
    public GameObject eItem;
}
public class ScoreItemEvent : SDD.Events.Event
{
	public float eScore { get; set; }
}
#endregion


public class PlayerFellIntoWaterEvent : SDD.Events.Event
{
}
public class PlayerGotHitEvent : SDD.Events.Event
{
}

public class NextLevelEvent : SDD.Events.Event
{
}

public class LevelCompletedEvent : SDD.Events.Event
{
}
