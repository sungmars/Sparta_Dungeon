using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Condition condition;
    public UseItem useItem;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        condition = GetComponent<Condition>();
        controller = GetComponent<PlayerController>();
        useItem = GetComponent<UseItem>();
    }
}
