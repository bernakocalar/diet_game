using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    public List<ShopSystem.FurnitureItem> ownedFurniture = new List<ShopSystem.FurnitureItem>();

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

    public void AddFurniture(ShopSystem.FurnitureItem item)
    {
        ownedFurniture.Add(item);
        Debug.Log($"INVENTORY: Added {item.itemName} to inventory.");
    }

    public bool HasFurniture(string itemId)
    {
        return ownedFurniture.Exists(x => x.id == itemId);
    }
}
