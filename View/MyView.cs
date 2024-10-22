using Spectre.Console;
using FinalProject.Moldel;

namespace FinalProject.View
{
    public class MyView
    {
        public void ShowTitle(List<string> palette){
            Console.Clear();
            var rule = new Rule($"Mastermind Game {palette[0]}{palette[4]}{palette[3]}{palette[2]}");
            rule.LeftJustified();
            AnsiConsole.Write(rule);
        }
        public string ChoseName(){
            AnsiConsole.MarkupLine("[bold]Benvenuto a Mastermind![/]");;
            var name = AnsiConsole.Prompt(new TextPrompt<string>("Contro chi sto giocando?"));
            AnsiConsole.WriteLine();
            return name;
        }
        public int ChoseAttempts(){
            return  AnsiConsole.Prompt(new TextPrompt<int>("In quanti turni pensi di battermi?")); 
        }
        public int ChoseColorsAmount(){
            int colors;
            do{
                Console.Clear();
                colors = AnsiConsole.Prompt(new TextPrompt<int>("Con quanti colori vuoi giocare? (2-7)"));
                if(colors < 2 || colors > 7)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong choice! retry!\nPress any key");
                    Console.ReadKey();
                }
            }while(colors < 2 || colors > 7);
            return colors;
        }
        public Game ShowGame(int j, int pageSize, Game game){          
            AnsiConsole.WriteLine("\n\nScegli il tuo codice: ");
            game.GuessCode[j] = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(pageSize)
                .AddChoices(Game.chosenPalette));
            Console.Clear();
            game.OurCode = string.Join(" ", game.GuessCode);
            Console.WriteLine($"\n{game.OurCode}\n");
            
            AnsiConsole.WriteLine($"\n{game.PcCode}"); //Se si vuol barare
            return game;
        }
        public void ShowTable(int round, string[] dots, string[] hints){
            var table = new Table();
            table.AddColumn("Round");
            table.AddColumn("Pedine");
            table.AddColumn("Suggerimenti");
            for (int i = 0; i <= round; i++)
            {
                table.AddRow((i+1).ToString(), dots[i], hints[i]);
                table.AddEmptyRow();
            }

            ShowTitle(Game.palette);
            AnsiConsole.Write(table);
        }
        public int VictoryMenu(int round){
            AnsiConsole.WriteLine($"\nHai vinto in {round} turni!");
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
                        Console.Clear();
                        return 10;
                    case "No":
                        Console.WriteLine("Ciao ciao!");
                        Console.Clear();
                        return 0;
                    default:
                        return 0;
                }
        }
        public int GameOverMenu(string PcCode){
            AnsiConsole.WriteLine("\nMi dispiace, ma hai perso!");
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
                    Console.Clear();
                    return 10;
                case "No":   
                    Console.Clear();
                    return  0;
                default:
                    return 0;
            }
        }
        

    }
}