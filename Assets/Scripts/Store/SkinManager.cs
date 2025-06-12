using UnityEngine;

public static class SkinManager
{
    public static void UnlockSkin(string skinID)
    {
        PlayerPrefs.SetInt("Skin_" + skinID, 1);
    }

    public static bool IsSkinUnlocked(string skinID)
    {
        return PlayerPrefs.GetInt("Skin_" + skinID, 0) == 1;
    }

    public static void SetCurrentSkin(string skinID)
    {
        PlayerPrefs.SetString("CurrentSkin", skinID);
    }

    public static string GetCurrentSkin()
    {
        return PlayerPrefs.GetString("CurrentSkin", "default");
    }
}
