using System.Collections.Generic;
using Newtonsoft.Json;
using NoodleLand.Data.Achievements;
using NoodleLand.Events;
using NoodleLand.Events.Player;
using NoodleLand.Registeries;
using NoodleLand.Serialization;
using NoodleLand.Serialization.BDS;
using UnityEngine;

/*
MIT License

Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace NoodleLand.Achievements
{
    public class AchievementWatcher : MonoBehaviour,IOnGameSaveAndLoad
    {
        [SerializeField] private AchievementCompletedPopUp achievementPopUp;
        [SerializeField] private List<AchievementInstance> achievements = new();

        private void Awake()
        {
            GameSavingSystem.Register(this);
        }
        
        private void Start()
        {
            RegisteredAchievements achievementDatabase = RegisteredAchievements.Instance;
            
            for (int i = 0; i < achievementDatabase.Count; i++)
            {
                Achievement acv0 = achievementDatabase.Get(i);
                Debug.Log(acv0.AchievementName);
                achievements.Add(new AchievementInstance(acv0));
            }
        }

        private void OnEnable()
        {
          FakeEventBus.Subscribe<ItemCollectedEvent>(this);
        }
        
        public void CompleteAchievement(Achievement achievement)
        {
            foreach (var achievementInstance in achievements)
            {
                if (achievementInstance.Achievement == achievement)
                {
                    if (!achievementInstance.HasCompleted)
                    {
                        achievementInstance.CompleteAchievement();

                        achievementPopUp.Set(achievement);
                    }
                }
                
            }
        }

        public AchievementInstance Get(Achievement achievement)
        {
            AchievementInstance a = null;
            for (var i = 0; i < achievements.Count; i++)
            {
                AchievementInstance instance = achievements[i];
                if (instance.Achievement == achievement)
                {
                    a = instance;
                }

            }

            return a;
        }
        
        public void OnItemCollected(ItemCollectedEvent @event)
        {
            RegisteredItems a = RegisteredItems.Instance;
        
            if (@event.Collected.BaseItemData == a.Flint)
            {
                CompleteAchievement(RegisteredAchievements.Instance.TheStart);
            }
        }

        public void OnLoadGame()
        {
            string entJson = PlayerPrefs.GetString("achievements");

            List<LDSDictionary> deserializeObject =  JsonConvert.DeserializeObject<List<LDSDictionary>>(entJson);
            
            for (var i = 0; i < deserializeObject.Count; i++)
            {
                LDSDictionary s0 = deserializeObject[i];
                achievements[i].OnLoad(s0);
            }
        }

        public void OnSaveGame()
        {
            List<LDSDictionary> binaryDataSaves = new List<LDSDictionary>();
            foreach (var acchv in achievements)
            {
                LDSDictionary b0 = new LDSDictionary();
                acchv.OnSave(b0);
                binaryDataSaves.Add(b0);

            }
            string saved = JsonConvert.SerializeObject(binaryDataSaves);
            PlayerPrefs.DeleteKey("achievements");
            PlayerPrefs.SetString("achievements",saved);
        }
    }
}