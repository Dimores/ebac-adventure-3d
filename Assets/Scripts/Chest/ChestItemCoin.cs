using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    [Header("Coin Settings")]
    public int coinAmount = 20;
    public GameObject coinObject;

    [Header("Spawn Range")]
    public Vector2 randomRange = new Vector2(-0.5f, 0.5f);
    public Vector2 heightRange = new Vector2(0.5f, 1.5f);

    [Header("Animation Settings")]
    public float tweenEndTime = 0.5f;

    private readonly List<GameObject> coins = new List<GameObject>();

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject coin = Instantiate(coinObject);

            Vector3 randomOffset = new Vector3(
                Random.Range(randomRange.x, randomRange.y),
                Random.Range(heightRange.x, heightRange.y),
                Random.Range(randomRange.x, randomRange.y)
            );

            coin.transform.position = transform.position + randomOffset;
            coin.transform.DOScale(Vector3.zero, tweenEndTime).From().SetEase(Ease.OutBack);

            coins.Add(coin);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach (var coin in coins)
        {
            coin.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            coin.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}