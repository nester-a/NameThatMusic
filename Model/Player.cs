using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Player
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
        }
        public void IncreaseScores()
        {
            Scores++;
        }
        public void ReduceScores()
        {
            Scores--;
            if (Scores < 0)
            {
                Scores = 0;
            }
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
