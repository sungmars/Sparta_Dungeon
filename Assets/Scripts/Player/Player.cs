using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Condition condition;
    public GetItem getItem;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;
    private Coroutine speedBoostCoroutine;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        condition = GetComponent<Condition>();
        controller = GetComponent<PlayerController>();
        getItem = GetComponent<GetItem>();
    }

    public void UseItem()
    {
        if (itemData == null)
        {
            Debug.Log("[Player] 사용 가능한 아이템이 없습니다.");
            return;
        }

        Debug.Log($"[Player] {itemData.displayName} 아이템 사용!");

        foreach (var consumable in itemData.consumables)
        {
            if (consumable.type == ConsumableType.Speed)
            {
                Debug.Log($"[Player] 속도 증가 실행! 증가량: {consumable.value}");


                if (speedBoostCoroutine != null)
                {
                    StopCoroutine(speedBoostCoroutine);
                    controller.ResetSpeed(); // 이전 속도 복구
                }

                speedBoostCoroutine = StartCoroutine(IncreaseSpeed(consumable.value, 5f));
            }
            else if (consumable.type == ConsumableType.Health)
            {
                Debug.Log($"[Player] 체력 {consumable.value} 회복!");
                condition.Heal(consumable.value);
            }
        }

        itemData = null;
    }

    private IEnumerator IncreaseSpeed(float speedIncrease, float duration)
    {
        Debug.Log($"[Player] IncreaseSpeed() 실행됨! 속도 증가: {speedIncrease}");

        controller.ApplySpeedBuff(speedIncrease); 

        Debug.Log($"[Player] 속도 증가 적용됨! 현재 속도: {controller.GetCurrentSpeed()}");

        yield return new WaitForSeconds(duration);

        controller.ResetSpeed();
        Debug.Log($"[Player] 속도 복귀됨! 현재 속도: {controller.GetCurrentSpeed()}");
    }
}
