using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int CurrentCoins { get; private set; }

    public UnityEvent<int> onCoinsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
        Debug.Log($"<color=yellow>CURRENCY: Added {amount} coins. Total: {CurrentCoins}</color>");
        onCoinsChanged?.Invoke(CurrentCoins);
    }

    public bool SpendCoins(int amount)
    {
        if (CurrentCoins >= amount)
        {
            CurrentCoins -= amount;
            Debug.Log($"<color=yellow>CURRENCY: Spent {amount} coins. Remaining: {CurrentCoins}</color>");
            onCoinsChanged?.Invoke(CurrentCoins);
            return true;
        }
        else
        {
            Debug.Log($"<color=red>CURRENCY: Not enough coins! Needed: {amount}, Have: {CurrentCoins}</color>");
            return false;
        }
    }
}
