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
        public Player()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

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
        }
        public void IncreaseScores()
        {
            Scores++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scores"));
        }
        public void ReduceScores()
        {
            Scores--;
            if (Scores < 0)
            {
                Scores = 0;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scores"));
        }

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
