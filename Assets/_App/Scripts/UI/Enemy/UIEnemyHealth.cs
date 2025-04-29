using System;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Canvas canvas;
    
    private Camera _mainCamera;
    
    private void Start()
    {
        // Get the main camera
        _mainCamera = Camera.main;
    }
    
    public void SetHealth(float health)
    {
        // Clamp the health value between 0 and 1
        health = Mathf.Clamp01(health);
        healthBar.fillAmount = health;
    }

    private void Update()
    {
        // Rotate the canvas to face the camera
        if (_mainCamera is not null)
        {
            canvas.transform.LookAt(canvas.transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }
    }
    
    public void SetActive(bool isActive)
    {
        canvas.gameObject.SetActive(isActive);
    }
}
