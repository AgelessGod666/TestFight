using System;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Fight
{
    public event Action<Fighter> FightEnded;
    private readonly Fighter firstPlayer;
    private readonly Fighter secondPlayer;

    public Fight(Fighter firstPlayer, Fighter secondPlayer)
    {
        this.firstPlayer = firstPlayer;
        this.secondPlayer = secondPlayer;
        Initialize();
    }

    private void Initialize()
    {
        Run(firstPlayer, secondPlayer);
        Run(secondPlayer, firstPlayer);
    }
    
    private async void Run(Fighter firstPlayer, Fighter secondPlayer)
    {
        var timerAsyncEnumerable = UniTaskAsyncEnumerable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select((_, i) => Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position))
            .TakeWhile(x => PlayerOnDistance(x, firstPlayer, secondPlayer));
        
        await foreach (var second in timerAsyncEnumerable)
        {
            firstPlayer.transform.position = Vector3.MoveTowards(firstPlayer.transform.position,
                secondPlayer.transform.position, firstPlayer.speed * 10 * Time.deltaTime);
        }
    }
    
    private async void NewFight(Fighter firstPlayer, Fighter secondPlayer)
    {
        var timerAsyncEnumerable = UniTaskAsyncEnumerable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .TakeWhile(x => PlayerFight(firstPlayer, secondPlayer));
        
        await foreach (var second in timerAsyncEnumerable)
        {
            secondPlayer.health -= firstPlayer.damage;
        }
    }

    private bool PlayerOnDistance(float radius, Fighter firstPlayer, Fighter secondPlayer)
    {
        var result = radius > firstPlayer.radius;
        if (!result)
        {
            NewFight(firstPlayer, secondPlayer);
        }
        return result;
    }
    
    private bool PlayerFight(Fighter firstPlayer, Fighter secondPlayer)
    {
        var result = secondPlayer.health > 0;
        if (!result)
        {
            Object.Destroy(secondPlayer.gameObject);
            FightEnded?.Invoke(firstPlayer);
        }
        return result;
    }
}