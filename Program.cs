﻿
using Microsoft.VisualBasic;
using Spectre.Console;
using FinalProject.Moldel;
using FinalProject.View;
using FinalProject.Controller;

public class Program
{
    static void Main(string[] args){
        Game game = new Game();
        MyView myView = new MyView();
        Controller controller = new Controller(myView, game);
        controller.GameMenu();
    }

}