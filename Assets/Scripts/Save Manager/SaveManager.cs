using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        private void Save()
        {
            SaveSetup setup = new SaveSetup();

            setup.lastLevel = 2;
            setup.playerName = "Diego";

            string setupToJson = JsonUtility.ToJson(setup, true);
            SaveFile(setupToJson);
        }

        private void SaveFile(string json)
        {
            string path = Application.persistentDataPath + "/save.txt";

            File.WriteAllText(path, json);
        }
    }

    [System.Serializable]
    public class SaveSetup
    {
        public int lastLevel;
        public string playerName;
    }
}
