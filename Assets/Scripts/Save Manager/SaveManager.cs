using Cloth;
using Ebac.Core.Singleton;
using System;
using System.IO;
using UnityEngine;

namespace Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        public int lastLevel;

        private SaveSetup _saveSetup;
        private string _path;

        public Action<SaveSetup> FileLoaded;

        #region PROPERTIES

        public SaveSetup GetSaveSetup()
        {
            return _saveSetup;
        }

        public float GetSavedHealth()
        {
            return _saveSetup.playerHealth;
        }

        public ClothType GetSavedCloth()
        {
            return (ClothType)_saveSetup.clothType;
        }

        public int GetSavedCheckpoint()
        {
            return _saveSetup.checkpointKey;
        }

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            _path = Path.Combine(
                Application.persistentDataPath,
                "save.txt"
            );

            Debug.Log(_path);
        }

        private void Start()
        {
            Load();
        }

        #endregion

        #region SAVE

        private void Save()
        {
            string setupToJson =
                JsonUtility.ToJson(_saveSetup, true);

            SaveFile(setupToJson);
        }

        public void SaveItems()
        {
            _saveSetup.coins =
                Items.ItemManager.Instance
                .GetItemByType(Items.ItemType.COIN)
                .soInt.value;

            _saveSetup.healthPacks =
                Items.ItemManager.Instance
                .GetItemByType(Items.ItemType.LIFE_PACK)
                .soInt.value;

            Save();
        }

        public void SaveName(string name)
        {
            _saveSetup.playerName = name;

            Save();
        }

        public void SaveLastLevel(int level)
        {
            _saveSetup.lastLevel = level;

            SaveItems();
        }

        public void SaveCheckpoint(
            int checkpointKey,
            float playerHealth,
            ClothType clothType)
        {
            _saveSetup.checkpointKey = checkpointKey;
            _saveSetup.playerHealth = playerHealth;
            _saveSetup.clothType = (int)clothType;

            Save();
        }

        #endregion

        #region LOAD

        public void LoadCheckpoint()
        {
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.LoadCheckpoint(
                    _saveSetup.checkpointKey
                );
            }
        }

        #endregion

        #region FILE

        private void SaveFile(string json)
        {
            File.WriteAllText(_path, json);
        }

        private void Load()
        {
            if (File.Exists(_path))
            {
                string fileLoaded =
                    File.ReadAllText(_path);

                SaveSetup loadedSetup =
                    JsonUtility.FromJson<SaveSetup>(fileLoaded);

                if (loadedSetup != null)
                {
                    _saveSetup = loadedSetup;
                }
            }
            else
            {
                CreateNewSave();
            }

            lastLevel = _saveSetup.lastLevel;

            FileLoaded?.Invoke(_saveSetup);
        }

        private void CreateNewSave()
        {
            _saveSetup = new SaveSetup
            {
                lastLevel = 0,

                coins = 0,
                healthPacks = 0,

                playerName = "Diego",

                checkpointKey = 0,

                clothType = (int)ClothType.SPEED,

                playerHealth = 50
            };

            Save();
        }

        #endregion
    }

    [System.Serializable]
    public class SaveSetup
    {
        public int lastLevel;

        public float coins;
        public float healthPacks;

        public string playerName;

        public int checkpointKey;
        public int clothType;
        public float playerHealth;
    }
}