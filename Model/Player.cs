using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Player
    {
        public string Name { get; private set; }
        public int Scores { get; private set; } = 0;
        public Player(string _name)
        {
            Name = _name;
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
        public void SetNewName(string _newName)
        {
            Name = _newName;
        }
    }
}
