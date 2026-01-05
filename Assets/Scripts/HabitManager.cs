using System.Collections.Generic;
using UnityEngine;

public class HabitManager : MonoBehaviour
{
    public static HabitManager Instance { get; private set; }

    public List<Habit> habits = new List<Habit>();

    [Header("Debug/Testing")]
    public bool addTestHabitsOnStart = true;

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

    private void Start()
    {
        if (addTestHabitsOnStart)
        {
            CreateHabit("Drink Water", "Drink 8 glasses of water daily");
            CreateHabit("Exercise", "Do 30 minutes of cardio");
            CreateHabit("Read", "Read 10 pages of a book");
        }
    }

    public void CreateHabit(string name, string description)
    {
        Habit newHabit = new Habit(name, description);
        habits.Add(newHabit);
        Debug.Log($"Created habit: {name}");
    }

    public void CompleteHabit(string habitId)
    {
        Habit habit = habits.Find(h => h.id == habitId);
        if (habit != null)
        {
            habit.Complete();
            Debug.Log($"Completed habit: {habit.habitName}. Streak: {habit.currentStreak}");
            
            // Reward Coins (e.g., 10 coins per completion)
            CurrencyManager.Instance?.AddCoins(10);

            // Notify Award System (to be implemented)
            AwardSystem.Instance?.CheckAwards(habit);
        }
    }

    public void CheckDailyResets()
    {
        foreach (var habit in habits)
        {
            habit.CheckReset();
        }
    }
}
