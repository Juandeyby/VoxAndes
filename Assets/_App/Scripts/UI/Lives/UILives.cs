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

        var fullHearts = lives / 2;
        var hasHalfHeart = (lives % 2) == 1;

        for (var i = 0; i < livesImages.Length; i++)
        {
            if (i < fullHearts)
            {
                livesImages[i].sprite = livesSprites[0]; // Full
            }
            else if (i == fullHearts && hasHalfHeart)
            {
                livesImages[i].sprite = livesSprites[1]; // Half
            }
            else
            {
                livesImages[i].sprite = livesSprites[2]; // Empty
            }
        }
    }
}
