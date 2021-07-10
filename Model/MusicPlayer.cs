using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TagLib;

namespace NameThatMusic.Model
{
    class MusicPlayer
    {
        public ObservableCollection<Music> PlayList { get; private set; } = new ObservableCollection<Music>();
        public ObservableCollection<Music> CurrentPlayList { get; private set; } = new ObservableCollection<Music>();
        public Music CurrentMusic { get; private set; }
        public int CurrentMusicIndex { get; private set; } = 0;
        public bool styleSelected { get; private set; } = false;
        public bool chooseRandomMusic { get; private set; } = true;
        public bool allMusicWasPlayed { get; private set; } = false;
        public bool IsActive { get; private set; } = false;
        public SoundPlayer musicPlayer { get; private set; }
        public Timer playingTimer { get; set; }

        //конструктор
        public MusicPlayer(string _musicFolder)
        {
            //ищем музыку в переданной в конструктор директории
            string[] musicFiles = Directory.GetFiles(_musicFolder, "*.mp3");
            for (int i = 0; i < musicFiles.Length; i++)
            {
                PlayList.Add(new Music(musicFiles[i]));

                //теперь нужно каким-то образом задать песням название и определить их жанр
                //скорее всего это всё будет делаться в настройках игры
                //т.е. перед запуском игры, нужно будет просканировать директорию с музыкой
                //затем отобразить список адресов песен
                //и в ручную к каждом экземпляру добавить музыку и жанр
            }

            //заполнение общего плэйлиста из директории
            CurrentMusic = NextMusic();
        }

        //он нам нужен, чтобы ставить песни случайным образом
        static Random random = new Random();

        public void Play(int _seconds)
        {
            //проверяем все-ли песни уже играли
            if(CurrentMusic == null && allMusicWasPlayed)
            {
                //останавливаем игру, чтобы игрок выбрал что делать дальше
                //возможно он захочет начать игру заново, выбрать другой жанр
                //или закрыть игру
            }

            //если ещё есть песни, которые не проигрывались, то продолжаем играть
            int time = _seconds * 1000;//переводим время в милисекунды
            musicPlayer = new SoundPlayer(CurrentMusic.Path);
            playingTimer = new Timer(time);

            //если музыка загрузилась удачно, то начинаем её проигрывать
            if(musicPlayer != null)
            {
                IsActive = true;
                playingTimer.Start();
                musicPlayer.Play();
                //проигрывание музыки определённый промежуток времени
                playingTimer.Elapsed += Timer_Elapsed;
            }
            else
            {
                //произошла ошибка
            }
            IsActive = false;
        }

        //событие - время проигрывания вышло
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StopMusic();
        }

        public void StopMusic()
        {
            //останавливаем музыку
            playingTimer.Stop();
            playingTimer.Dispose();
            musicPlayer.Stop();
            musicPlayer.Dispose();

            //показываем название композиции, которая играла, каким-нибудь образом
            //
            //

            //переключаем песню на следующую
            CurrentMusic.wasPlayed = true;
            CurrentMusic = NextMusic();
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
