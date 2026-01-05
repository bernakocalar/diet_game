using System;

[Serializable]
public class Habit
{
    public string id;
    public string habitName;
    public string description;
    public int currentStreak;
    public bool isCompletedToday;
    public DateTime lastCompletedDate;

    public Habit(string name, string desc)
    {
        id = Guid.NewGuid().ToString();
        habitName = name;
        description = desc;
        currentStreak = 0;
        isCompletedToday = false;
        lastCompletedDate = DateTime.MinValue;
    }

    public void Complete()
    {
        if (isCompletedToday) return;

        isCompletedToday = true;
        currentStreak++;
        lastCompletedDate = DateTime.Now;
    }

    public void CheckReset()
    {
        if (lastCompletedDate.Date < DateTime.Now.Date.AddDays(-1))
        {
            // Only reset if it wasn't completed yesterday or today
            // If completed yesterday, streak continues (but isCompletedToday becomes false for new day)
            // This logic allows for a daily check.
            isCompletedToday = false;
            if (lastCompletedDate.Date < DateTime.Now.Date.AddDays(-1))
            {
                currentStreak = 0;
            }
        }
        else if (lastCompletedDate.Date < DateTime.Now.Date)
        {
             // It's a new day, reset the daily flag
             isCompletedToday = false;
        }
    }
}
