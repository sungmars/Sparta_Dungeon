using UnityEngine;

public class GetItem : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public void OnInteract()
    {
        if (CharacterManager.Instance.Player == null) return;

        Debug.Log($"[GetItem] {itemData.displayName} 아이템 전달 및 사용!");

        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.UseItem();
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
