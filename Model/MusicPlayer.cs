using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace NameThatMusic.Model
{
    class MusicPlayer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        static Random random = new Random();

        public ObservableCollection<Music> PlayList { get; private set; } = new ObservableCollection<Music>();
        public ObservableCollection<Music> CurrentPlayList { get; private set; } = new ObservableCollection<Music>();
        public Music CurrentMusic { get; private set; } = null;
        public int CurrentMusicIndex { get; private set; } = 0;
        public bool styleSelected { get; private set; } = false;
        public bool chooseRandomMusic { get; private set; } = true;
        public bool allMusicWasPlayed { get; private set; } = false;
        public bool IsActive { get; private set; } = false;
        public MediaPlayer musicPlayer { get; private set; } = new MediaPlayer();


        public MusicPlayer()
        {

        }

        
        public void PlayMusic()
        {
            //проверяем все-ли песни уже играли
            if(CurrentMusic != null && !allMusicWasPlayed)
            {
                IsActive = true;
                musicPlayer.Open(new Uri(CurrentMusic.Path, UriKind.Absolute));
                musicPlayer.Play();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsActive"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
            }
        }
        public void StopMusic()
        {
            IsActive = false;
            musicPlayer.Stop(); //здесь вылазит ошибка воспроизведения

            //переключаем песню на следующую
            CurrentMusic.wasPlayed = true;
            CurrentMusic = NextMusic();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsActive"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        }
        public Music NextMusic()
        {
            //выбирает следующую песню для проигрывания
            if(chooseRandomMusic == true)
            {
                return GetRandomMusic();
            }
            else
            {
                return GetNextMusic();
            }
        }
        public Music GetRandomMusic()
        {
            //проверяем вся-ли музыка была проиграна
            if (AllMusicWasPlayed())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("allMusicWasPlayed"));
                return null;
            }
            //выбирает случайную следующую песню
            if (styleSelected == true)
            {
                int randomNumberOfMusic = random.Next(0, CurrentPlayList.Count);
                while (CurrentPlayList[randomNumberOfMusic].wasPlayed == true)
                {
                    randomNumberOfMusic = random.Next(0, CurrentPlayList.Count);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                return CurrentPlayList[randomNumberOfMusic];
            }
            else
            {
                int randomNumberOfMusic = random.Next(0, PlayList.Count);
                while (PlayList[randomNumberOfMusic].wasPlayed == true)
                {
                    randomNumberOfMusic = random.Next(0, PlayList.Count);
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                return PlayList[randomNumberOfMusic];
            }
        }
        public Music GetNextMusic()
        {
            //проверяем вся-ли музыка была проиграна
            if (AllMusicWasPlayed())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("allMusicWasPlayed"));
                return null;
            }
            //выбирает следующую песню по порядку проигрывания и проверяет не превышает-ли индекс текущей песни размер коллекции
            if (styleSelected == true)
            {
                while (CurrentPlayList[CurrentMusicIndex].wasPlayed == true && CurrentMusicIndex <= CurrentPlayList.Count - 1)
                {
                    CurrentMusicIndex++;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                return CurrentPlayList[CurrentMusicIndex];
            }
            else
            {
                while (PlayList[CurrentMusicIndex].wasPlayed == true && CurrentMusicIndex <= CurrentPlayList.Count - 1)
                {
                    CurrentMusicIndex++;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                return PlayList[CurrentMusicIndex];
            }
        }
        public bool AllMusicWasPlayed()
        {
            //проводит проверку была ли проиграна вся музыка
            if (styleSelected == true)
            {
                for (int i = 0; i < CurrentPlayList.Count; i++)
                {
                    if (CurrentPlayList[i].wasPlayed != true)
                    {
                        allMusicWasPlayed = false;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("allMusicWasPlayed"));
                        return allMusicWasPlayed;
                    }
                }
            }
            else if (styleSelected != true)
            {
                for (int i = 0; i < PlayList.Count; i++)
                {
                    if (PlayList[i].wasPlayed != true)
                    {
                        allMusicWasPlayed = false;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("allMusicWasPlayed"));
                        return allMusicWasPlayed;
                    }
                }
            }
            allMusicWasPlayed = true;
            CurrentMusic = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("allMusicWasPlayed"));
            return allMusicWasPlayed;
        }
        public void LoadMusic(string _musicFolder)
        {
            string[] musicFiles = Directory.GetFiles(_musicFolder, "*.mp3");
            for (int i = 0; i < musicFiles.Length; i++)
            {
                PlayList.Add(new Music(musicFiles[i]));
            }
            CurrentMusic = NextMusic();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        }


        //не используется
        //public void ChangeMusicStyle(string _style)
        //{
        //    //выбираем жанр песен, которые будем проигрывать
        //    CurrentPlayList.Clear();
        //    for (int i = 0; i < PlayList.Count; i++)
        //    {
        //        if(PlayList[i].MusicStyle == _style)
        //        {
        //            CurrentPlayList.Add(PlayList[i]);
        //        }
        //    }
        //    styleSelected = true;
        //    CurrentMusic = NextMusic();
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        //}
        //конструктор
        //public MusicPlayer(string _musicFolder)
        //{
        //    LoadMusic(_musicFolder);
        //}
        //
        //public void ResetAll()
        //{
        //    //сбрасываем всю музыку
        //    ResetMusicStyle();
        //    CurrentMusic = null;
        //    CurrentMusicIndex = 0;
        //    allMusicWasPlayed = false;
        //    foreach (var music in PlayList)
        //    {
        //        music.wasPlayed = false;
        //    }
        //    CurrentMusic = NextMusic();
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusic"));
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMusicName"));
        //}
        //
        //public void ResetMusicStyle()
        //{
        //    //сбрасываем заданый музыкальный жанр, после этого будет проигрываться вся музыка
        //    CurrentPlayList = null;
        //    styleSelected = false;
        //}
    }
}
