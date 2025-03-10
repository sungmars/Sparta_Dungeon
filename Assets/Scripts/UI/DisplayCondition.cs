using UnityEngine;
using UnityEngine.UI;
public class DisplayCondition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;
    void Start()
    {
        curValue = startValue;
    }
    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }
    float GetPercentage()
    {
        return curValue / maxValue;
    }
    public void Add(float value)//체력 증가
    {
        curValue = Mathf.Min(curValue + value, maxValue);//체력 최대값 설정
    }

    public void Subtract(float value)//체력 감소
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
