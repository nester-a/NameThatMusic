﻿using NameThatMusic.Model;
using NameThatMusic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabControlTestMVVM.ViewModel;
using Microsoft.Win32;

namespace NameThatMusic.ViewModel
{
    class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private PlayersWindow _playersWindow;
        private SettingsWindow _settingsWindow;
        


        //public Game Game { get; set; } = new Game();
        
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
        public string SW_MusicFolder { get; set; }


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
                    Game.StartGame();
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

                    Game.EndGame();
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
                    Game.StartRound();

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand ClickStop
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    //останавливаем раунд
                    Game.StopRound();

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
                    _settingsWindow.Closing += _settingsWindow_Closing;
                    _settingsWindow.Show();
                    
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }

        private void _settingsWindow_Closing(object sender, CancelEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GameRoundTime"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Game"));
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
        public ICommand SW_ClickGetMusicFolder
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    OpenFileDialog ODF = new OpenFileDialog();
                    if (ODF.ShowDialog() == true)
                    {
                        string tmp = ODF.FileName;
                        int index = tmp.LastIndexOf('\\');
                        SW_MusicFolder = tmp.Substring(0, index);
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SW_MusicFolder"));
                },
                (p) =>
                {
                    return true;
                });
            }
        }
        public ICommand SW_ClickSaveMusicFolder
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Game.MusicPlayer = new MusicPlayer();
                    Game.MusicPlayer.LoadMusic(SW_MusicFolder);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SW_MusicFolder"));
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
