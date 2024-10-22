using FinalProject.Moldel;
using FinalProject.View;
using Spectre.Console;

namespace FinalProject.Controller
{
    public class Controller
    {
        private MyView _myView;
        private Game _game;
        private string _name;
        public string[] Dots{get;set;}
        public string[] Hints{get;set;}


        public Controller(MyView myView, Game game){
            _game = game;
            _myView = myView;
        }

        public void GameMenu(){
            _myView.ShowTitle(Game.palette);
            _name = _myView.ChoseName();
            _game.Attempts = _myView.ChoseAttempts();
            Dots = new string[_game.Attempts];
            Hints = new string[_game.Attempts];
            _game.Colours = _myView.ChoseColorsAmount();
            Game.chosenPalette = Game.palette.GetRange(0, _game.Colours);
            GenerateSecretCode();
            int pageSize =  (Game.chosenPalette.Count < 3) ? 3 : Game.chosenPalette.Count;
            Console.Clear();
            while (_game.Attempts > 0)
            {
                for (int j = 0; j < _game.GuessCode.Length; j++)
                {
                    _game = _myView.ShowGame(j, pageSize, _game);
                }
                Dots[_game.Round] = _game.OurCode;
            
                _game.Attempts--;

                GameLogic();

                string hint = $"{Emoji.Known.BlackCircle}: {_game.Black} - {Emoji.Known.WhiteCircle}: {_game.White}";
                Hints[_game.Round] = hint;

                _myView.ShowTable(_game.Round, Dots, Hints);

                if (_game.OurCode == _game.PcCode)
                {
                    _game.Attempts = _myView.VictoryMenu(_game.Round);
                }else if(_game.Attempts == 0)
                {
                    _game.Attempts = _myView.GameOverMenu(_game.PcCode);
                }else if(_game.Attempts == 1)
                {
                    AnsiConsole.WriteLine("\nTic-tac, hai ancora un tentativo.");
                }
                else
                {
                    AnsiConsole.WriteLine($"\nRitenta, hai ancora {_game.Attempts} tentativi.");
                }
                _game.Round++;
            }
        }
        private void GenerateSecretCode()
        {
            Random code = new Random();
            for (int i = 0; i < _game.SecretCode.Length; i++)
            {
                _game.SecretCode[i] = Game.chosenPalette[code.Next(0, _game.Colours)];
            }
            _game.PcCode = string.Join(" ", _game.SecretCode);
        }

        private void GameLogic(){
            bool[] visited = new bool[4];
            bool[] guessVisited = new bool[4];

            _game.Black = 0;
            _game.White = 0; 

            for (int i = 0; i < 4; i++)
            {
                if (_game.GuessCode[i] == _game.SecretCode[i])
                {
                    _game.Black++;
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
                        if (!visited[j] && _game.GuessCode[i] == _game.SecretCode[j])
                        {
                            _game.White++;
                            visited[j] = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}