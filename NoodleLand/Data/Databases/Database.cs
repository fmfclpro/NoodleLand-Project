using System.Collections.Generic;
using NoodleLand.Data.Achievements;
using NoodleLand.Data.Drops;
using NoodleLand.Data.Entities;
using NoodleLand.Data.Items;
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

namespace NoodleLand.Data.Databases
{
    public class Database<T> : ScriptableObject
    {
        public List<T> values = new List<T>();

        public void Register(T t)
        {
            if (values.Contains(t)) return;
            Debug.Log($"{t} was sucessfully added to database of {typeof(T)}");
            values.Add(t);
        }

        public bool Has(T t)
        {
            return values.Contains(t);
        }


        public void Unregister(T t)
        {
            values.Remove(t);
        }
    }

    class DatabasePostDeleteProessor : AssetModificationProcessor
    {
        private static void Process<T>(string deletedAssetPath, string databasePatb) where T : UnityEngine.Object
        {
            if (AssetDatabase.GetMainAssetTypeAtPath(deletedAssetPath) == typeof(T))
            {
                T a = AssetDatabase.LoadAssetAtPath<T>(deletedAssetPath);
                if (a != null)
                {
                    var objs = AssetDatabase.LoadAllAssetsAtPath(databasePatb);
                    foreach (var o in objs)
                    {
                        if (o is Database<T> database)
                        {
                            Debug.Log("Sucessfully Unregistered from Database");
                            database.Unregister(a);
                        }
                    }
                }
            }
        }

        public static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
        {
            Process<DropData>(path, DropDatabase.FolderPath);
            Process<EntityData>(path, EntityDatabase.FolderPath);
            Process<BaseItemData>(path, ItemDatabase.FolderPath);
            Process<Achievement>(path, AchievementDatabase.FolderPath);

            // if (AssetDatabase.GetMainAssetTypeAtPath(path) == typeof(DropData))
            // {
            //    DropData a = AssetDatabase.LoadAssetAtPath<DropData>(path);
            //    if (a != null)
            //    {
            //       var objs = AssetDatabase.LoadAllAssetsAtPath(DropDatabase.FolderPath);
            //       foreach (var o in objs)
            //       {
            //          if (o is DropDatabase dropDatabase)
            //          {
            //             dropDatabase.Unregister(a);
            //          }
            //                
            //       }
            //    }
            //
            // }
            return AssetDeleteResult.DidNotDelete;
        }
    }
}