using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance { get; private set; }

    [System.Serializable]
    public class FurnitureItem
    {
        public string id;
        public string itemName;
        public int price;
        public string description;
        // In a real project, this would reference a GameObject or Prefab
        public string prefabName; 
    }

    public List<FurnitureItem> shopItems = new List<FurnitureItem>();

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

        InitializeShop();
    }

    private void InitializeShop()
    {
        AddShopItem("chair_01", "Wooden Chair", 50, "A simple wooden chair.", "ChairPrefab");
        AddShopItem("table_01", "Coffee Table", 100, "A nice coffee table.", "TablePrefab");
        AddShopItem("plant_01", "Potted Plant", 30, "Adds some life to the room.", "PlantPrefab");
        AddShopItem("sofa_01", "Comfy Sofa", 500, "Luxury sitting.", "SofaPrefab");
    }

    private void AddShopItem(string id, string name, int price, string desc, string prefab)
    {
        shopItems.Add(new FurnitureItem { id = id, itemName = name, price = price, description = desc, prefabName = prefab });
    }

    public void BuyItem(string itemId)
    {
        FurnitureItem item = shopItems.Find(x => x.id == itemId);
        if (item != null)
        {
            if (CurrencyManager.Instance.SpendCoins(item.price))
            {
                InventorySystem.Instance.AddFurniture(item);
                Debug.Log($"SHOP: Successfully bought {item.itemName}!");
            }
        }
        else
        {
            Debug.LogError($"SHOP: Item {itemId} not found.");
        }
    }
}
