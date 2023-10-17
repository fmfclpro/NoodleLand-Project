using System;
using System.Collections.Generic;
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

namespace NoodleLand.Serialization
{

    public interface IOnGameSaveAndLoad
    {
        public void OnLoadGame();
        public void OnSaveGame();

    }
    public class GameSavingSystem : MonoBehaviour
    {
        private List<IOnGameSaveAndLoad> _onGameSaveAndLoads = new List<IOnGameSaveAndLoad>();
        private static event Action<IOnGameSaveAndLoad> onGameSaveAndLoadEvent;

        private void Awake()
        {

            onGameSaveAndLoadEvent += InternalRegister;
        }

        public void LoadGame()
        {
            foreach (var onGameSaveAndLoad in _onGameSaveAndLoads)
            {
                onGameSaveAndLoad.OnLoadGame();
                
            }
        }

        public void SaveGame()
        {
            foreach (var onGameSaveAndLoad in _onGameSaveAndLoads)
            {
                onGameSaveAndLoad.OnSaveGame();
                
            }
        }
        private void OnDestroy()
        {
            onGameSaveAndLoadEvent -= InternalRegister;
        }

        private void InternalRegister(IOnGameSaveAndLoad onGameSaveAndLoad)
        {
            _onGameSaveAndLoads.Add(onGameSaveAndLoad);
        }
        public static void Register(IOnGameSaveAndLoad obj)
        {

            onGameSaveAndLoadEvent?.Invoke(obj);
            
        }

    }
}