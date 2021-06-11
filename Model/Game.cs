using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Game
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        public MusicPlayer MusicPlayer { get; set; }
        private string defaultPlayerName = "Player";

        public Game()
        {
            Players.Add(new Player(defaultPlayerName + " " + Players.Count.ToString()));
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
    }
}
