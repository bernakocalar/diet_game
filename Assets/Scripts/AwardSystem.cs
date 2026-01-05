using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwardSystem : MonoBehaviour
{
    public static AwardSystem Instance { get; private set; }

    [System.Serializable]
    public class Award
    {
        public string id;
        public string title;
        public string description;
        public int requiredStreak;
        public bool isUnlocked;
    }

    public List<Award> awards = new List<Award>();
    
    // Event to listen to
    public UnityEvent<Award> onAwardUnlocked;

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

        InitializeAwards();
    }

    private void InitializeAwards()
    {
        // Define some basic "win the award" style achievements
        AddAward("award_first_step", "First Step", "Complete a habit once", 1);
        AddAward("award_streak_3", "Consistency is Key", "Reach a 3-day streak", 3);
        AddAward("award_streak_7", "Habit Master", "Reach a 7-day streak", 7);
    }

    private void AddAward(string id, string title, string desc, int reqStreak)
    {
        awards.Add(new Award { id = id, title = title, description = desc, requiredStreak = reqStreak, isUnlocked = false });
    }

    public void CheckAwards(Habit habit)
    {
        foreach (var award in awards)
        {
            if (!award.isUnlocked && habit.currentStreak >= award.requiredStreak)
            {
                UnlockAward(award);
            }
        }
    }

    private void UnlockAward(Award award)
    {
        award.isUnlocked = true;
        Debug.Log($"<color=yellow>AWARD UNLOCKED: {award.title}!</color>");
        onAwardUnlocked?.Invoke(award);
    }
}
