using System;
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
        public event PropertyChangedEventHandler PropertyChanged;
        CancellationTokenSource stopSource;
        private int deffaultTime = 10;

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
        public MusicPlayer MusicPlayer { get; set; }
        public int RoundTime { get; private set; } = 10;
        public bool IsStarted { get; set; } = false;
        public bool RoundIsStarted { get; set; }
        public bool RoundIsStoped { get; set; }
        public string CurrentMusicName
        {
            get
            {
                if (MusicPlayer.allMusicWasPlayed)
                {
                    return "All Music Was Played";
                }
                else  if (MusicPlayer.CurrentMusic == null)
                {
                    return "No song";
                }
                return MusicPlayer.CurrentMusic.Name;
            }
        }


        public Game()
        {
            MusicPlayer = new MusicPlayer();
        }


        public void StartRound()
        {
            RoundIsStoped = false;
            RoundIsStarted = true;
            MusicPlayer.PlayMusic(RoundTime);
            TimeIsRunningOut();
        }
        public void StopRound()
        {
            RoundIsStarted = false;
            RoundIsStoped = true;
            MusicPlayer.StopMusic();
            stopSource?.Cancel();
            RoundTime = deffaultTime;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoundTime"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        }
        public void ChangeRoundTime(int newRoundTime)
        {
            RoundTime = newRoundTime;
            deffaultTime = RoundTime;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoundTime"));
        }
        public void TimeIsRunningOut()
        {
            stopSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (RoundTime > 0)
                {
                    Task.Delay(1000).Wait();
                    RoundTime--;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoundTime"));
                    if (stopSource.Token.IsCancellationRequested) return;
                    if (RoundTime <= 0)
                    {
                        stopSource = new CancellationTokenSource();
                        StopRound();
                    }
                }
            });
            RoundTime = deffaultTime;
            RoundIsStarted = false;
            RoundIsStoped = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoundTime"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        }
        public void StartGame()
        {
            IsStarted = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsStarted"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanBeStarted"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanBeCanceled"));
        }
        public void EndGame()
        {
            IsStarted = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsStarted"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanBeStarted"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanBeCanceled"));
        }

        //не используется
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
        //
        //public void StartGame()
        //{
        //    IsStarted = true;

        //    //if (!MusicPlayer.allMusicWasPlayed)
        //    //{
        //    //    MusicPlayer.Play(RoundTime);
        //    //}
        //    //else
        //    //{
        //    //    //вся музыка проигралась
        //    //    //ждём пока игроки решат, что делать
        //    //}
        //}
        //
        //public void StartNewGame()
        //{
        //    MusicPlayer.ResetAll();
        //}
    }
}
