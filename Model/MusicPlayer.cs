using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class MusicPlayer
    {
        public ObservableCollection<Music> PlayList { get; set; } = new ObservableCollection<Music>();
        public ObservableCollection<Music> CurrentPlayList { get; set; } = new ObservableCollection<Music>();
        public Music CurrentMusic { get; set; }
        public int CurrentMusicIndex { get; set; } = 0;
        public bool styleSelected { get; set; } = false;
        public bool chooseRandomMusic { get; set; } = true;
        public bool allMusicWasPlayed { get; set; } = false;

        static Random random = new Random();

        public MusicPlayer(string _musicFolder)
        {
            //заполнение общего плэйлиста из директории
            CurrentMusic = NextMusic();
        }

        public void Play(int _time)
        {
            if(CurrentMusic == null && allMusicWasPlayed)
            {
                //останавливаем игру, чтобы игрок выбрал что делать дальше
                //возможно он захочет начать игру заново, выбрать другой жанр
                //или закрыть игру
            }
            //проигрывание музыки определённый промежуток времени
            CurrentMusic.wasPlayed = true;
            CurrentMusic = NextMusic();
        }
        public void Pause()
        {
            //ставим музыку на паузу
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
        public void ChangeMusicStyle(string _style)
        {
            //выбираем жанр песен, которые будем проигрывать
            CurrentPlayList.Clear();
            for (int i = 0; i < PlayList.Count; i++)
            {
                if(PlayList[i].MusicStyle == _style)
                {
                    CurrentPlayList.Add(PlayList[i]);
                }
            }
            styleSelected = true;
            CurrentMusic = NextMusic();
        }
        public void ResetMusicStyle()
        {
            //сбрасываем заданый музыкальный жанр, после этого будет проигрываться вся музыка
            CurrentPlayList = null;
            styleSelected = false;
        }
        public Music GetRandomMusic()
        {
            //проверяем вся-ли музыка была проиграна
            if (AllMusicWasPlayed())
            {
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
                return CurrentPlayList[randomNumberOfMusic];
            }
            else
            {
                int randomNumberOfMusic = random.Next(0, PlayList.Count);
                while (PlayList[randomNumberOfMusic].wasPlayed == true)
                {
                    randomNumberOfMusic = random.Next(0, PlayList.Count);
                }
                return PlayList[randomNumberOfMusic];
            }
        }
        public Music GetNextMusic()
        {
            //проверяем вся-ли музыка была проиграна
            if (AllMusicWasPlayed())
            {
                return null;
            }
            //выбирает следующую песню по порядку проигрывания и проверяет не превышает-ли индекс текущей песни размер коллекции
            if (styleSelected == true)
            {
                while (CurrentPlayList[CurrentMusicIndex].wasPlayed == true && CurrentMusicIndex <= CurrentPlayList.Count - 1)
                {
                    CurrentMusicIndex++;
                }
                return CurrentPlayList[CurrentMusicIndex];
            }
            else
            {
                while (PlayList[CurrentMusicIndex].wasPlayed == true && CurrentMusicIndex <= CurrentPlayList.Count - 1)
                {
                    CurrentMusicIndex++;
                }
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
                        return allMusicWasPlayed;
                    }
                }
            }
            allMusicWasPlayed = true;
            return allMusicWasPlayed;
        }
        public void ResetAll()
        {
            //сбрасываем всю музыку
            ResetMusicStyle();
            CurrentMusic = null;
            CurrentMusicIndex = 0;
            allMusicWasPlayed = false;
            foreach (var music in PlayList)
            {
                music.wasPlayed = false;
            }
            CurrentMusic = NextMusic();
        }
    }
}
