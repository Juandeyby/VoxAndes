using UnityEngine;
using UnityEngine.Serialization;

public class UIHubMenu : MonoBehaviour
{
    [SerializeField] private UILives lives;
    public UILives Lives => lives;
    
    [SerializeField] private UIInstruction instruction;
    public UIInstruction Instruction => instruction;

    [SerializeField] private UIPuzzleInteract uiPuzzleInteract;
    public UIPuzzleInteract UIPuzzleInteract => uiPuzzleInteract;
    
    [SerializeField] private UIGold uiGold;
    public UIGold UIGold => uiGold;
}