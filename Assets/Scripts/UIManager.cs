using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // This is a placeholder for the actual UI implementation.
    // In a real Unity project, we would reference UI GameObjects (Text, Buttons, Panels).
    // Since we are coding blindly without the Editor to drag references, we will simulate UI updates via logs for now,
    // or assume a setup that uses Find or dynamic instantiation.

    private void Start()
    {
        if (AwardSystem.Instance != null)
        {
            AwardSystem.Instance.onAwardUnlocked.AddListener(ShowAwardNotification);
        }
    }

    public void ShowAwardNotification(AwardSystem.Award award)
    {
        // "Win the award style" visualization would trigger here
        // e.g., play fanfare sound, show popup animation
        Debug.Log($"UI DISPLAY: You won the '{award.title}' award! {award.description}");
    }

    public void RefreshHabitList()
    {
        // Would update the visual list of habits
        Debug.Log("UI: Refreshing Habit List...");
        foreach(var h in HabitManager.Instance.habits)
        {
            Debug.Log($"Displaying Habit: {h.habitName} [Streak: {h.currentStreak}] [Done Today: {h.isCompletedToday}]");
        }
        Debug.Log($"COINS: {CurrencyManager.Instance.CurrentCoins}");
    }

    private void Update()
    {
        // Ensure Keyboard is present to avoid null reference exceptions if no keyboard is connected
        if (Keyboard.current == null) return;

        // Temporary testing input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RefreshHabitList();
        }

        // Shop Test
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.Log("UI: Attempting to buy 'chair_01' (50 coins)...");
            ShopSystem.Instance.BuyItem("chair_01");
        }

        // House Test
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("UI: Attempting to place 'chair_01'...");
            HouseManager.Instance.PlaceFurniture("chair_01");
            HouseManager.Instance.ShowHouseStatus();
        }
    }
}
