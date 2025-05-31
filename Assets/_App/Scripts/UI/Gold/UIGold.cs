using TMPro;
using UnityEngine;

public class UIGold : MonoBehaviour
{ 
    [SerializeField] private TMP_Text text;

    public void SetGold(int gold)
    {
        if (text == null)
        {
            Debug.LogError("Text component is not assigned in UIGold.");
            return;
        }

        text.text = gold.ToString();
    }
}
