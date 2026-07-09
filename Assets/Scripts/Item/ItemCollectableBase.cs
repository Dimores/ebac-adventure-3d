using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [Header("Item Type")]
        public ItemType itemType;

        [Header("Collider")]
        public Collider itemCollider;

        [Header("Sprite")]
        public GameObject visual;

        [Header("Collision tag")]
        public string compareTag = "Player";

        [Header("Sounds")]
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (visual != null) visual.SetActive(false);
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;
            Invoke("HideObject", 3);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}