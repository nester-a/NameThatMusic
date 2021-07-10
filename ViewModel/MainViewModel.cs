﻿using NameThatMusic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabControlTestMVVM.ViewModel;

namespace NameThatMusic.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Player Player1 { get; set; } = new Player();
        public Player Player2 { get; set; } = new Player();
        public Player Player3 { get; set; } = new Player();
        public Player Player4 { get; set; } = new Player();

        #region Add/Remove Player Buttons
        public ICommand ClickPlayersOneButton
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player1.ChangeActive();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player1"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersTwoButton
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player2.ChangeActive();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player2"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersThreeButton
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player3.ChangeActive();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player3"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersFourButton
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player4.ChangeActive();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player4"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }

        #endregion

        #region Increase/Reduce Scores
        #region Player1
        public ICommand ClickPlayersOneButtonIncreaseScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player1.IncreaseScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player1"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersOneButtonReduceScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player1.ReduceScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player1"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        #endregion
        #region Player2
        public ICommand ClickPlayersTwoButtonIncreaseScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player2.IncreaseScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player2"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersTwoButtonReduceScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player2.ReduceScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player2"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        #endregion
        #region Player3
        public ICommand ClickPlayersThreeButtonIncreaseScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player3.IncreaseScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player3"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersThreeButtonReduceScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player3.ReduceScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player3"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        #endregion
        #region Player4
        public ICommand ClickPlayersFourButtonIncreaseScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player4.IncreaseScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player4"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlayersFourButtonReduceScores
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player4.ReduceScores();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player4"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        #endregion
        #endregion

    }
}