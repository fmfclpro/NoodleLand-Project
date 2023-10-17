using NoodleLand.Data.Databases;
using NoodleLand.Data.Entities;
using UnityEditor;
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

namespace NoodleLand.Data.Achievements
{
    [CreateAssetMenu(menuName = "NoodleLand/Data/Achievements/New Achievement", fileName = "Achievement", order = 0)]
    public class Achievement : ScriptableObject,IData
    {
        
        [SerializeField] private string achvTag;
        [SerializeField] private string achievementName;
        [SerializeField] private Sprite image;
        [SerializeField] private Color iconMultiplayer;

        public string AchievementName => achievementName;
        public Sprite Image => image;
        public Color ColorMultiplier => iconMultiplayer;
        
        
        private void Awake()
        {
            RegisterSelf();
            if(iconMultiplayer.a == 0) iconMultiplayer = Color.white;
            
        }

        private void RegisterSelf()
        {
            var objs = AssetDatabase.LoadAllAssetsAtPath(AchievementDatabase.FolderPath);

            if (objs != null && objs.Length != 0)
            {
                foreach (var o in objs)
                {
                    if (o is AchievementDatabase achievementDatabase)
                    {
                        if(! achievementDatabase.Has(this))
                            achievementDatabase.Register(this);
                        return;
                    }    
                   
                }
            }
        }
        
        public string GetTag()
        {
            return achvTag;
        }
    }
}