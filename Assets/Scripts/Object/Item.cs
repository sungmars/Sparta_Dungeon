using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        return $"{data.displayName}\n{data.description}";
    }

    public void OnInteract()
    {
        Debug.Log($"[Item] {data.displayName} 획득!");

        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();

        if (gameObject != CharacterManager.Instance.Player.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
