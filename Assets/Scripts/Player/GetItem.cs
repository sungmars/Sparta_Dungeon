using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void OnInteract()
    {
        UseItem();
    }

    public void UseItem()
    {
        if (itemData == null)
        {
            Debug.LogWarning("ItemData is not assigned.");
            return;
        }

        foreach (var consumable in itemData.consumables)
        {
            if (consumable.type == ConsumableType.Speed)
            {
                StartCoroutine(IncreaseSpeed(consumable.value, 5f)); // 5초 동안 속력 증가
            }
        }
    }

    private IEnumerator IncreaseSpeed(float speedIncrease, float duration)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed += speedIncrease;

        yield return new WaitForSeconds(duration);

        playerController.moveSpeed = originalSpeed;
    }

    public string GetInteractPrompt()
    {
        return "Press Mouse Left Button";
    }

    public void SetItemData(ItemData data)
    {
        itemData = data;
    }
}
