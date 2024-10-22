using Spectre.Console;

namespace FinalProject.Moldel
{
    public class Game
    {
        public static List<string> palette = new List<string> {Emoji.Known.YellowCircle, Emoji.Known.PurpleCircle, Emoji.Known.BlueCircle, Emoji.Known.GreenCircle, 
                                                            Emoji.Known.RedCircle, Emoji.Known.OrangeCircle, Emoji.Known.BrownCircle};
        public static List <string> chosenPalette = new List<string>();
        public string OurCode{get;set;}
        public string PcCode{get;set;}
        public string[] SecretCode{get;set;}// = new string[4];
        public string[] GuessCode{get;set;}// = new string[4];
        public int Attempts{get;set;}
        public int Colours{get;set;}
        public int Black{get;set;}
        public int White{get;set;}
        public int Round{get;set;}// = 0;
        // conversione codici giocatori in stringhe
        // = "";
        // = "";
        public Game()
        {
            SecretCode = new string[4];
            GuessCode = new string[4];
            Attempts = 0;
            Colours = 0;
            Round = 0;
            PcCode = "";
            OurCode = "";
        }

    }
}