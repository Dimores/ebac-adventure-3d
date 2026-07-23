using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using UnityEngine.Events;
using Save;

namespace Items
{
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;  

        public AudioClip coinPickUpSound;
        public UnityEvent onReset;
        public UnityEvent onAddCoin;
        public UnityEvent onAddLifePack;
        public UnityEvent onRemoveLifePack;

        private void Start()
        {
            Reset();
            Invoke(nameof(LoadItemsFromSave), 0.1f);
        }

        public void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int) SaveManager.Instance.GetSaveSetup().coins);
            AddByType(ItemType.LIFE_PACK, (int) SaveManager.Instance.GetSaveSetup().healthPacks);
        }

        private void Reset()
        {
            foreach (var itemSetup in itemSetups)
            {
                itemSetup.soInt.value = 0;
            }

            onReset?.Invoke();
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;

            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;

            if (itemType == ItemType.COIN)
            {
                onAddCoin?.Invoke();
            }
            else if (itemType == ItemType.LIFE_PACK)
            {
                onAddLifePack?.Invoke();
            }
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;

            var item = itemSetups.Find(i => i.itemType == itemType);

            item.soInt.value -= amount;

            if(item.soInt.value < 0) item.soInt.value = 0;

            onRemoveLifePack?.Invoke();
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }

        public void AddRocks(int amount = 1)
        {
            //rocks.value += amount;
            onAddLifePack?.Invoke();
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public IntData soInt;
    }
}