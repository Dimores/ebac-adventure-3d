using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyToOpen = KeyCode.E;
    public Animator chestAnimator;
    public string triggerOpen = "Open";
    public string playerTag = "Player";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Header("Drop")]
    public ChestItemBase chestItem;

    private Player player;
    private float startScale;
    private bool _isOpen = false;

    void Start()
    {
        startScale = notification.transform.localScale.x;
        notification.transform.localScale = Vector3.zero;
        notification.SetActive(false);

        _isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToOpen) && notification.activeSelf)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (_isOpen) return;

        chestAnimator.SetTrigger(triggerOpen);
        HideNotification();
        _isOpen = true;

        Invoke(nameof(ShowItem), 0.4f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 0.5f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player != null && !_isOpen)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player != null)
        {
            HideNotification();
        }
    }

    private void ShowNotification()
    {
        notification.transform.DOKill();
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(Vector3.one * startScale, tweenDuration).SetEase(tweenEase);
    }

    private void HideNotification()
    {
        notification.transform.DOKill();
        notification.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            notification.SetActive(false);
        });
    }
}