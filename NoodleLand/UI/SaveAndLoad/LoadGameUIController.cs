using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using NoodleLand.Serialization.BDS;
using NoodleLand.UI.SaveAndLoad;
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

public class LoadGameUIController : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private LoadOptionUI model;


    private List<LoadOptionUI> _loadOptions = new List<LoadOptionUI>();

    public void Refresh()
    {
        Clear();
        string js = PlayerPrefs.GetString("SavedGames");
        LDSDictionary ldsDictionary = JsonConvert.DeserializeObject<LDSDictionary>(js);
        if(ldsDictionary == null) return;
        
        List<string> names = ldsDictionary.GetStringList("names");
        List<string> times = ldsDictionary.GetStringList("times");
        
        for (var i = 0; i < names.Count; i++)
        {
            AddLoadOption(names[i],times[i]);
            
        }
    }

    private void AddLoadOption(string text, string time)
    {
        LoadOptionUI optionUI = Instantiate(model);
        optionUI.Set(text,time);
        _loadOptions.Add(optionUI);
    }

    private void Clear()
    {
        for (var i = 0; i < _loadOptions.Count; i++)
        {
            Destroy(_loadOptions[i].gameObject);
            
        }
    }
    public void Add(string loadName, string time)
    {
        
    }
}
