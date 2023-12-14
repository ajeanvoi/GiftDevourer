using UnityEngine.UI;
using TMPro;
using UnityEngine;

public abstract class AbstractBoost : MonoBehaviour
{
    public Sprite backgroundPower;
    public string description;
    public bool canUsePower = false;

    protected virtual void OnEnable()
    {
        canUsePower = true;
        this.gameObject.GetComponent<Image>().sprite = backgroundPower;
        this.gameObject.GetComponent<ButtonController>().AddDescription(description);
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => ListenerMethod());
    }

    protected void ListenerMethod()
    {
        if (canUsePower)
        {
            ApplyPower();
            canUsePower = false;
        }
    }
    public abstract void ApplyPower();
}
