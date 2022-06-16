using UnityEngine;

public class GameBoot : MonoBehaviour
{
    [SerializeField] private PlayersHolder playersHolder;
    [SerializeField] private TimerHolder timerHolder;
    
    private void Awake()
    {
        var enemyFinder = new EnemyFinder(playersHolder);
        var fightsSolver = new FightsSolver(enemyFinder.FindFight());
        var timerView = new TimerView(timerHolder, fightsSolver);
    }
}