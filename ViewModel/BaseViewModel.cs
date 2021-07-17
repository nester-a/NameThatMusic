using NameThatMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.ViewModel
{
    class BaseViewModel
    {
        public static Game Game { get; set; } = new Game();


        public static Player Player1 { get; set; } = new Player();
        public static Player Player2 { get; set; } = new Player();
        public static Player Player3 { get; set; } = new Player();
        public static Player Player4 { get; set; } = new Player();
    }
}
