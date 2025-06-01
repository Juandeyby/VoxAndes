using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private UIMenuPanel uiMenuPanel;
    public UIMenuPanel UIMenuPanel => uiMenuPanel;
    [SerializeField] private UIMenuOptions uiMenuOptions;
    public UIMenuOptions UIMenuOptions => uiMenuOptions;
}
