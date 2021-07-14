﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Game : INotifyPropertyChanged
    {
        public bool CanBeCanceled
        {
            get
            {
                if (IsStarted)
                {
                    return true;
                }
                return false;
            }
        }
        public bool CanBeStarted
        {
            get
            {
                if (!IsStarted)
                {
                    return true;
                }
                return false;
            }
        }
        public bool CanStartPlayMusic
        {
            get
            {
                if(MusicPlayer != null)
                {
                    if (MusicPlayer.IsActive)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }




        //наш музыкальный плеер, который будет ставить музыку
        public MusicPlayer MusicPlayer { get; set; }

        //время на проигрывание музыки, в секундах
        public int RoundTime { get; private set; } = 10;

        public bool IsStarted { get; set; } = false;
        public Game()
        {
            MusicPlayer = new MusicPlayer();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void StartGame()
        {
            IsStarted = true;

            //if (!MusicPlayer.allMusicWasPlayed)
            //{
            //    MusicPlayer.Play(RoundTime);
            //}
            //else
            //{
            //    //вся музыка проигралась
            //    //ждём пока игроки решат, что делать
            //}
        }
        public void StartRound()
        {
            MusicPlayer.Play(RoundTime);
        }
        public void StartNewGame()
        {
            MusicPlayer.ResetAll();
        }
        public void ChangeRoundTime(int newRoundTime)
        {
            RoundTime = newRoundTime;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoundTime"));
        }


        //private string defaultPlayerName = "Player";
        //
        //коллекция игроков
        //public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        //
        //public void AddNewPlayer()
        //{
        //    if (Players.Count < 4)
        //    {
        //        //Players.Add(new Player(defaultPlayerName + " " + Players.Count.ToString()));
        //        Players.Add(new Player());
        //    }
        //    else
        //    {
        //        //нельзя добавить более 4-х игроков
        //    }
        //}
        //
        //public void RemovePlayer()
        //{
        //    if (Players.Count > 1)
        //    {
        //        Players.Remove(Players[Players.Count - 1]);
        //    }
        //    else
        //    {
        //        //в игре не может быть меньше одного игрока
        //    }
        //}
        //
        //public void PlayerDoTurn(Player _player)
        //{
        //    //пока не понимаю, как будут ходить игроки
        //}
    }
}
