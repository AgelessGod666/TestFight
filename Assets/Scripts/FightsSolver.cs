using System;
using System.Collections.Generic;

public interface IFightTimer
{
    event Action<int> OnFightEnded;
}

public class FightsSolver : IFightTimer
{
    public event Action<int> OnFightEnded;
    private readonly Fight[] fights;
    private readonly List<Fighter> winners = new List<Fighter>();
    private DateTime startTime;

    public FightsSolver(Fight[] fights)
    {
        this.fights = fights;
        Initialize();
    }

    private void Initialize()
    {
        startTime = DateTime.Now;
        foreach (var fight in fights)
        {
            fight.FightEnded += AddWinner;
        }
    }

    private void AddWinner(Fighter fighter)
    {
        winners.Add(fighter);
        if (fights.Length == winners.Count)
        {
            var timeSpan = DateTime.Now - startTime;
            OnFightEnded?.Invoke(timeSpan.Seconds);
        }
    }
}