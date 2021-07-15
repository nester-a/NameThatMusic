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
    }
}
