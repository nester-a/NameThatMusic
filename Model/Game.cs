﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Game
    {
        //коллекция игроков
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

        //наш музыкальный плеер, который будет ставить музыку
        public MusicPlayer MusicPlayer { get; set; }

        //время на проигрывание музыки, в секундах
        public int RoundTime { get; set; }

        private string defaultPlayerName = "Player";

        public Game()
        {
            Players.Add(new Player(defaultPlayerName + " " + (Players.Count + 1)));
            MusicPlayer = new MusicPlayer("C:\\Music");//нужно добавить конкретную директорию загрузки игры
        }
        public void AddNewPlayer()
        {
            if (Players.Count < 4)
            {
                Players.Add(new Player(defaultPlayerName + " " + Players.Count.ToString()));
            }
            else
            {
                //нельзя добавить более 4-х игроков
            }
        }
        public void RemovePlayer()
        {
            if (Players.Count > 1)
            {
                Players.Remove(Players[Players.Count - 1]);
            }
            else
            {
                //в игре не может быть меньше одного игрока
            }
        }
        public void StartGame()
        {
            if (!MusicPlayer.allMusicWasPlayed)
            {
                MusicPlayer.Play(RoundTime);
            }
            else
            {
                //вся музыка проигралась
                //ждём пока игроки решат, что делать
            }
        }
        public void StartNewGame()
        {
            MusicPlayer.ResetAll();
        }
        public void PlayerDoTurn(Player _player)
        {
            //пока не понимаю, как будут ходить игроки
        }
    }
}