using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static void AddPoints(int amount)
    {
        int current = PlayerPrefs.GetInt("PlayerPoints", 0);
        PlayerPrefs.SetInt("PlayerPoints", current + amount);
    }

    public static int GetPoints()
    {
        return PlayerPrefs.GetInt("PlayerPoints", 0);
    }

    public static void SpendPoints(int amount)
    {
        int current = PlayerPrefs.GetInt("PlayerPoints", 0);
        PlayerPrefs.SetInt("PlayerPoints", Mathf.Max(0, current - amount));
    }
}
