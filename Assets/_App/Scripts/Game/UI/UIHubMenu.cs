using UnityEngine;

public class UIHubMenu : MonoBehaviour
{
    [SerializeField] private UILives lives;
    public UILives Lives => lives;
    [SerializeField] private UIInstruction instruction;
    public UIInstruction Instruction => instruction;
}