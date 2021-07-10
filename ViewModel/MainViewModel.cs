using NameThatMusic.Model;
using NameThatMusic.View;
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
        private PlayersWindow _playersWindow;
        private SettingsWindow _settingsWindow;


        public Player Player1 { get; set; } = new Player();
        public Player Player2 { get; set; } = new Player();
        public Player Player3 { get; set; } = new Player();
        public Player Player4 { get; set; } = new Player();
        public Game Game { get; set; } = new Game();
        public int GameRoundTime
        {
            get
            {
                return Game.RoundTime;
            }
        }
        public bool CanStartPlayMusic
        {
            get
            {
                if ((Player1.IsActive || Player2.IsActive || Player3.IsActive || Player4.IsActive) && Game.IsStarted)
                {
                    return true;
                }
                return false;
            }
        }
        public int SW_RoundTime { get; set; }



        #region Add/Remove Player Buttons
        public ICommand ClickPlayersOneButton
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Player1.ChangeActive();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Player1"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
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


        public ICommand ClickStartGame
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    ////открытие окна для игроков
                    //if(_playersWindow == null)
                    //{
                    //    _playersWindow = new PlayersWindow();
                    //    _playersWindow.Show();
                    //}
                    Game.IsStarted = true;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickEndGame
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    //закрываем окно с игроками
                    //if(_playersWindow != null)
                    //{
                    //    _playersWindow.Close();
                    //    _playersWindow = null;
                    //}

                    Game.IsStarted = false;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanStartPlayMusic"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickPlay
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    //ну вот мы нажимаем на песню и она играет
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickMenuButtonOptions
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    _settingsWindow = new SettingsWindow();
                    _settingsWindow.Show();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }

        public ICommand SW_ClickSaveRoundTime
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Game.ChangeRoundTime(SW_RoundTime);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SW_RoundTime"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GameRoundTime"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }


    }
}
