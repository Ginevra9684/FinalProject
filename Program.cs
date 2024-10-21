﻿
using Microsoft.VisualBasic;
using Spectre.Console;

internal class Program
{
    static string[] secretCode = new string[4];
    static string[] guessCode = new string[4];

    //creazione della palette colori con emoji
    static string giallo = "" + Emoji.Known.YellowCircle;
    static string viola = "" + Emoji.Known.PurpleCircle;
    static string blu = "" + Emoji.Known.BlueCircle;
    static string verde = "" + Emoji.Known.GreenCircle;
    static string rosso = "" + Emoji.Known.RedCircle;
    static string arancione = "" + Emoji.Known.OrangeCircle;
    static string marrone = "" + Emoji.Known.BrownCircle;
    static List<string> palette = new List<string> { giallo, viola, blu, verde, rosso, arancione, marrone };
    static List <string> chosenPalette = new List<string> {};

    static int attempts = 0;
    static int colours = 0;
    static int round = 0;
    // conversione codici giocatori in stringhe
    static string PcCode = "";
    static string ourCode = "";

    //array per i dati in tabella
    static string[] dots = new string[attempts];
    static string[] hints = new string[attempts];

    private static void Main(string[] args)
    {
        string path = @"punteggi.csv";
        if (!File.Exists(path)) //rende persistenti i dati - se manca, il programma "cancella" il testo presente nel file
        {
            File.Create(path).Close();
        }
        
        //titolo di gioco 
        Console.Clear();
        var rule = new Rule($"Mastermind Game {giallo}{rosso}{verde}{blu}");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        //salvataggio del nome giocatore e scelta "difficoltà"
        AnsiConsole.MarkupLine("[bold]Benvenuto a Mastermind![/]");;
        var name = AnsiConsole.Prompt(new TextPrompt<string>("Contro chi sto giocando?"));
        AnsiConsole.WriteLine("");

        attempts = AnsiConsole.Prompt(new TextPrompt<int>("In quanti turni pensi di battermi?")); //scelta turni
        
        colours = AnsiConsole.Prompt(new TextPrompt<int>("Con quanti colori vuoi giocare? (1-7)"));
        chosenPalette = palette.GetRange(0, colours);

        //tentativi
        while (attempts > 0)
        {
            GenerateSecretCode(); 

            for (int j = 0; j < guessCode.Length; j++)
            {
                AnsiConsole.WriteLine("\n\nScegli il tuo codice: ");
                guessCode[j] = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .PageSize(chosenPalette.Count)
                    .AddChoices(chosenPalette));
                Console.Clear();
                ourCode = string.Join(" ", guessCode);
                Console.WriteLine($"\n{ourCode}\n");
                
                AnsiConsole.WriteLine($"\n{PcCode}"); //Se si vuol barare
            }

            dots[round] = ourCode;
            
            round++;
            attempts--;

            //hint
            int white = 0, black = 0;

            bool[] visited = new bool[4];
            bool[] guessVisited = new bool[4];

            for (int i = 0; i < 4; i++)
            {
                if (guessCode[i] == secretCode[i])
                {
                    black++;
                    visited[i] = true;
                    guessVisited[i] = true;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (!guessVisited[i])
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!visited[j] && guessCode[i] == secretCode[j])
                        {
                            white++;
                            visited[j] = true;
                            break;
                        }
                    }
                }
            }
            string hint = $"{Emoji.Known.BlackCircle}: {black} - {Emoji.Known.WhiteCircle}: {white}";
            hints[round - 1] = hint;
            
            //tabella
            var table = new Table();
            table.AddColumn("Round");
            table.AddColumn("Pedine");
            table.AddColumn("Suggerimenti");
            for (int i = 1; i <= round; i++)
            {
                table.AddRow(i.ToString(), dots[i-1], hints[i-1]);
            }

            Console.Clear();
            AnsiConsole.WriteLine("");
            AnsiConsole.Write(rule);

            AnsiConsole.Write(table);

            //risultati del round
            int score = attempts*10;
            if (ourCode == PcCode)
            {
                AnsiConsole.WriteLine($"\nHai vinto in {round} turni!");
                //File.AppendAllText(path, $"\n{score} - {name} - {currentDate}/{currentMonth}-{currentHour}:{currentMinute}");
                AnsiConsole.WriteLine($"Vuoi giocare di nuovo?");
                var reStart = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choice)[/]")
                    .AddChoices(new[] {
                        "Sì","No"
                        }));
                switch (reStart)
                {
                    case "Sì":
                    attempts = 10;
                    
                    Console.Clear();
                    break;
                    case "No":
                    Console.WriteLine("Ciao ciao!");
                    attempts = 0;
                    Console.Clear();
                    break;
                }
            }
            else if (attempts == 0)
            {
                AnsiConsole.WriteLine("\nMi dispiace, ma hai perso!");
                //File.AppendAllText(path, $"\n{score} - {name} - {currentDate}/{currentMonth}-{currentHour}:{currentMinute}");
                AnsiConsole.WriteLine($"Il codice era {PcCode}");
                
                AnsiConsole.WriteLine($"Vuoi giocare di nuovo?");
                var reStart = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choice)[/]")
                    .AddChoices(new[] {
                        "Sì","No"
                        }));
                switch (reStart)
                {
                    case "Sì":
                        attempts = 10;

                        Console.Clear();
                    break;
                    case "No":   
                        attempts = 0;
                        Console.Clear();
                    break;
                }
            }
            else if (attempts == 1)
            {
                AnsiConsole.WriteLine("\nTic-tac, hai ancora un tentativo.");
            }
            else
            {
                AnsiConsole.WriteLine($"\nRitenta, hai ancora {attempts} tentativi.");
            }
        }
    }

    static void GenerateSecretCode()
    {
        Random code = new Random();
        for (int i = 0; i < secretCode.Length; i++)
        {
            secretCode[i] = chosenPalette[code.Next(0, colours)];
        }
        PcCode = string.Join(" ", secretCode);
    }

}