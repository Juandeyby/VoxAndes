using UnityEngine;

public class MummyGold : MonoBehaviour
{   
    [SerializeField] private GoldPiece goldPiecePrefab;
    [SerializeField] private int goldAmount = 1;
    
    public void DropGold()
    {
        for (var i = 0; i < goldAmount; i++)
        {
            var goldPiece = Instantiate(goldPiecePrefab, transform.position, Quaternion.identity);
            goldPiece.transform.SetParent(null); // Ensure the gold piece is not a child of the mummy
        }
    }
}
