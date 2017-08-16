using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class HornetComm
{
    static void Main()
    {
        var input = Console.ReadLine();

        var messagesPattern = @"^[0-9]+ <-> [0-9a-zA-Z]+$";
        var broadcastPattern = @"^[^0-9]+ <-> [a-zA-Z0-9]+$";

        var broadcasts = new List<string>();
        var privateMessages = new List<string>();

        while (input != "Hornet is Green")
        {
            var isMatchedMessage = Regex.Match(input, messagesPattern);
            var isMatchedBroadcast = Regex.Match(input, broadcastPattern);

            if (isMatchedBroadcast.Success || isMatchedMessage.Success)
            {
                var tokens = input.Split(new string[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries);

                var firstQuery = tokens[0];
                var secondQuery = tokens[1];

                AddPrivateMessages(privateMessages, isMatchedMessage, firstQuery, secondQuery);

                AddBroadcasts(broadcasts, isMatchedBroadcast, firstQuery, secondQuery);
            }

            input = Console.ReadLine();
        }

        Console.WriteLine("Broadcasts:");
        if (broadcasts.Count != 0)
        {
            foreach (var broadcast in broadcasts)
            {
                Console.WriteLine(broadcast);
            }
        }
        else
        {
            Console.WriteLine("None");
        }
        
        Console.WriteLine("Messages:");
        if (privateMessages.Count != 0)
        {
            foreach (var message in privateMessages)
            {
                Console.WriteLine(message);
            }
        }
        else
        {
            Console.WriteLine("None");
        }
        
    }

    private static void AddPrivateMessages(List<string> privateMessages, Match isMatchedMessage, string firstQuery, string secondQuery)
    {
        if (isMatchedMessage.Success)
        {
            var recipiendCode = firstQuery.Reverse().ToArray();
            var reversedRecipientCode = string.Empty;

            for (int i = 0; i < recipiendCode.Length; i++)
            {
                reversedRecipientCode += recipiendCode[i];
            }

            privateMessages.Add($"{reversedRecipientCode} -> {secondQuery}");
        }
    }

    private static void AddBroadcasts(List<string> broadcasts, Match isMatchedBroadcast, string firstQuery, string secondQuery)
    {
        if (isMatchedBroadcast.Success)
        {
            var frequency = string.Empty;

            for (int i = 0; i < secondQuery.Length; i++)
            {
                if (char.IsLower(secondQuery[i]))
                {
                    frequency += char.ToUpper(secondQuery[i]);
                }
                else if (char.IsUpper(secondQuery[i]))
                {
                    frequency += char.ToLower(secondQuery[i]);
                }
                else
                {
                    frequency += secondQuery[i];
                }
            }

            broadcasts.Add($"{frequency} -> {firstQuery}");
        }
    }
}

