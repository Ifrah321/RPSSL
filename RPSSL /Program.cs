// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Velkommen til Sten, Saks, Papir, Øgler, Spock!");
        Console.WriteLine("Vælg mellem: sten, saks, papir, øgler, spock");
        
        string[] choices = { "sten", "saks", "papir", "øgler", "spock" };
        
        var rules = new Dictionary<string, string[]>
        {
            { "sten", new[] { "saks", "øgler" } },
            { "saks", new[] { "papir", "øgler" } },
            { "papir", new[] { "sten", "spock" } },
            { "øgler", new[] { "spock", "papir" } },
            { "spock", new[] { "sten", "saks" } }
        };

        int winningScore = 2; 
        int playerScore = 0;
        int computerScore = 0;

        Random random = new Random();

        while (playerScore < winningScore && computerScore < winningScore)
        {
       
            Console.Write("\nDit valg: ");
            string playerChoice = Console.ReadLine().ToLower();

            if (Array.IndexOf(choices, playerChoice) == -1)
            {
                Console.WriteLine("Ugyldigt valg! Prøv igen.");
                continue;
            }
            
            string computerChoice = choices[random.Next(choices.Length)];
            Console.WriteLine($"Computeren vælger: {computerChoice}");
            
            if (playerChoice == computerChoice)
            {
                Console.WriteLine("Uafgjort, ingen får point.");
            }
            else if (Array.Exists(rules[playerChoice], element => element == computerChoice))
            {
                Console.WriteLine("Du vinder denne runde!");
                playerScore++;
            }
            else
            {
                Console.WriteLine("Computeren vinder denne runde!");
                computerScore++;
            }

            Console.WriteLine($"Stilling: Du {playerScore} - {computerScore} Computer");
        }
        
        if (playerScore > computerScore)
            Console.WriteLine("\nTillykke, du har vundet bedst ud af 3!");
        else
            Console.WriteLine("\nComputeren vandt bedst ud af 3!");
    }
}

