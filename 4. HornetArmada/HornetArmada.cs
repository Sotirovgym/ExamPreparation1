using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class HornetArmada
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        var legionActivity = new Dictionary<string, int>();
        var soldiers = new Dictionary<string, Dictionary<string, long>>();

        var pattern = @"^(\d+) = ([^=->:\s""]+) -> ([^=->:\s""]+):([\d]+)$";

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine();

            var isMatched = Regex.Match(input, pattern);

            if (isMatched.Success)
            {
                var lastActivity = int.Parse(isMatched.Groups[1].Value);
                var legionName = isMatched.Groups[2].Value;
                var soldierType = isMatched.Groups[3].Value;
                var soldierCount = long.Parse(isMatched.Groups[4].Value);

                if (!legionActivity.ContainsKey(legionName))
                {
                    legionActivity.Add(legionName, lastActivity);
                    soldiers.Add(legionName, new Dictionary<string, long>());
                }

                if (!soldiers[legionName].ContainsKey(soldierType))
                {
                    soldiers[legionName][soldierType] = 0;
                }

                if (legionActivity[legionName] < lastActivity)
                {
                    legionActivity[legionName] = lastActivity;
                }

                soldiers[legionName][soldierType] += soldierCount;
                       
            }

        }

        var conditions = Console.ReadLine().Split('\\');

        if (conditions.Length == 1)
        {
            var soldierType = conditions[0];
            var sortedLegions = legionActivity.OrderByDescending(x => x.Value);
            
            foreach (var legion in sortedLegions)
            {
                if (soldiers[legion.Key].ContainsKey(soldierType))
                {
                    Console.WriteLine($"{legion.Value} : {legion.Key}");
                }                
            }
        }
        else
        {
            var lastActivity = int.Parse(conditions[0]);
            var searchedSoldiers = conditions[1];

            var sortedSoldiers = soldiers
                .Where(l => l.Value.ContainsKey(searchedSoldiers))
                .OrderByDescending(l => l.Value.Values.Sum());

            foreach (var soldier in sortedSoldiers)
            {

                if (legionActivity[soldier.Key] < lastActivity)
                {
                    Console.WriteLine($"{soldier.Key} : {soldier.Value[searchedSoldiers]}");
                }                
            }
        }
    }
}


