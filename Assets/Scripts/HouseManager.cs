using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public static HouseManager Instance { get; private set; }

    // Simulating "placed" furniture in a room
    // In a real game, this would spawn GameObjects at specific positions
    public List<ShopSystem.FurnitureItem> placedFurniture = new List<ShopSystem.FurnitureItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaceFurniture(string itemId)
    {
        if (InventorySystem.Instance.HasFurniture(itemId))
        {
            ShopSystem.FurnitureItem item = InventorySystem.Instance.ownedFurniture.Find(x => x.id == itemId);
            placedFurniture.Add(item);
            Debug.Log($"HOUSE: Placed {item.itemName} in the room. Room now looks better!");
            
            // Visual logic would go here (Instantiate prefab)
        }
        else
        {
            Debug.LogError("HOUSE: You don't own this item!");
        }
    }

    public void ShowHouseStatus()
    {
        Debug.Log("--- MY HOUSE ---");
        if (placedFurniture.Count == 0)
        {
            Debug.Log("The room is empty.");
        }
        else
        {
            foreach(var item in placedFurniture)
            {
                Debug.Log($"- {item.itemName} ({item.description})");
            }
        }
        Debug.Log("----------------");
    }
}
