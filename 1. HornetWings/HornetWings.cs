using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class HornetWings
{
    static void Main()
    {
        var wingFlaps = int.Parse(Console.ReadLine());
        var metersPer1000Flaps = decimal.Parse(Console.ReadLine());
        var endurance = int.Parse(Console.ReadLine());

        var meters = (wingFlaps / 1000) * metersPer1000Flaps;
        var seconds = (wingFlaps / 100) + (wingFlaps / endurance) * 5;

        Console.WriteLine($"{meters:f2} m.");
        Console.WriteLine($"{seconds} s.");
    }
}

