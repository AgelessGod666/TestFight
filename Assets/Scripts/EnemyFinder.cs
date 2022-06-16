using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder
{
    private readonly List<Fighter> firstTeam;
    private readonly List<Fighter> secondTeam;
    private List<Fight> fights = new List<Fight>();

    public EnemyFinder(PlayersHolder playersHolder)
    {
        firstTeam = playersHolder.firstTeam;
        secondTeam = playersHolder.secondTeam;
    }

    public Fight[] FindFight()
    {
        var minimumDistance = 1000f;
        var enemy = 0;
        for (int i = 0; i < firstTeam.Count; i++)
        {
            for (int j = 0; j < secondTeam.Count; j++)
            {
                var distance = Vector3.Distance(firstTeam[i].transform.position, secondTeam[j].transform.position);
                if (minimumDistance > distance)
                {
                    minimumDistance = distance;
                    enemy = j;
                }
            }

            var fight = new Fight(firstTeam[i], secondTeam[enemy]);
            fights.Add(fight);
            secondTeam.RemoveAt(enemy);
        }

        return fights.ToArray();
    }
    
    
}