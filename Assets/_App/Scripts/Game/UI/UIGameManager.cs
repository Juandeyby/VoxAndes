using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIGameManager : MonoBehaviour
{
    
    [SerializeField] private UIPauseMenu pauseMenu;
    public UIPauseMenu PauseMenu => pauseMenu;
    
    [SerializeField] private UIHubMenu hubMenu;
    public UIHubMenu HubMenu => hubMenu;
    

}