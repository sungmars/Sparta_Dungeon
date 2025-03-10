using UnityEngine;

public class UICondition : MonoBehaviour
{
    public DisplayCondition health;
    public DisplayCondition stamina;
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
