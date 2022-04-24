using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Player : INotifyPropertyChanged
    {
        public int Scores { get; private set; } = 0;
        public bool IsActive { get; private set; } = false;
        public bool CanAddPlayer
        {
            get
            {
                if (IsActive)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool CanRemovePlayer
        {
            get
            {
                if (!IsActive)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool CanReduceScores
        {
            get
            {
                return IsActive && Scores > 0;
            }
        }
        public string InGame
        {
            get
            {
                if (IsActive)
                {
                    return "Active";
                }
                return "Add player";
            }
        }
        public string CantRemovePlayer
        {
            get
            {
                if (IsActive)
                {
                    return "Remove player";
                }
                return "Cant remove";
            }
        }

        public Player()
        {

        }

        public void ChangeActive()
        {
            if (!IsActive)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
                Scores = 0;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsActive"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanAddPlayer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanRemovePlayer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CantRemovePlayer"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InGame"));
        }
        public void IncreaseScores()
        {
            Scores++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scores"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanReduceScores"));
        }
        public void ReduceScores()
        {
            Scores--;
            if (Scores < 0)
            {
                Scores = 0;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scores"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanReduceScores"));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //не используется
        //public string Name { get; private set; }
        //
        //public void SetNewName(string _newName)
        //{
        //    Name = _newName;
        //}
        //
        //public Player(string _name)
        //{
        //    Name = _name;
        //}
    }
}
