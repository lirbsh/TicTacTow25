﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    internal class MainPageVM:ObservableObject
    {
        private Games games = new();
        public bool IsBusy => games.IsBusy;
        public  IList<GameSize>? GameSizes { get => games.GameSizes; set => games.GameSizes = value; }
        public GameSize SelectedGameSize { get; set; } = new GameSize();
        public ICommand AddGameCommand => new Command(AddGame);

        private void AddGame()
        {
            games.AddGame(SelectedGameSize);
            OnPropertyChanged(nameof(IsBusy));
        }
        public IList<Game>? GamesList => games.GamesList;
        public MainPageVM()
        {
            games.OnGameAdded += OnGameAdded;
        }

        private void OnGameAdded(object? sender, bool e)
        {
            OnPropertyChanged(nameof(IsBusy));
        }
    }
}
