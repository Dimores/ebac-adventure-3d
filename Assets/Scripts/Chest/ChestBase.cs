using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator chestAnimator;
    public string triggerOpen = "Open";
    public string playerTag = "Player";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    private Player player;
    private float startScale;

    void Start()
    {
        startScale = notification.transform.localScale.x;
        notification.transform.localScale = Vector3.zero;
        notification.SetActive(false);
    }

    void Update()
    {

    }

    public void OpenChest()
    {
        chestAnimator.SetTrigger(triggerOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player != null)
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