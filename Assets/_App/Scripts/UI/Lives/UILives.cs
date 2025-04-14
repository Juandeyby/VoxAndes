using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    [SerializeField] private Image[] livesImages;
    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private int maxLives = 6;
    
    public void SetLives(int lives)
    {
        if (lives < 0 || lives > maxLives)
        {
            Debug.LogError("Invalid number of lives: " + lives);
            return;
        }

        // Full life, half life, empty life
        for (var i = 0; i < livesImages.Length; i++)
        {
            if (i < lives)
            {
                livesImages[i].sprite = livesSprites[0]; // Full life
            }
            else if (i == lives)
            {
                livesImages[i].sprite = livesSprites[1]; // Half life
            }
            else
            {
                livesImages[i].sprite = livesSprites[2]; // Empty life
            }
        }
    }
}
