using UnityEngine;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    [System.Serializable]
    public class Skin
    {
        public string name;
        public Material material;
        public int price;
        public int skinID;
    }

    public List<Skin> allSkins;
    private int currentSkinID = -1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Cargar skin seleccionado
            currentSkinID = PlayerPrefs.GetInt("SelectedSkinID", -1);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSkin(int skinID)
    {
        currentSkinID = skinID;
        PlayerPrefs.SetInt("SelectedSkinID", skinID);
        PlayerPrefs.Save();
    }

    public Material GetCurrentSkinMaterial()
    {
        if (currentSkinID == -1) return null;

        Skin skin = allSkins.Find(s => s.skinID == currentSkinID);
        return skin?.material;
    }

    public bool IsSkinPurchased(int skinID)
    {
        return PlayerPrefs.GetInt($"Skin_{skinID}_Purchased", 0) == 1;
    }

    public void PurchaseSkin(int skinID)
    {
        PlayerPrefs.SetInt($"Skin_{skinID}_Purchased", 1);
        PlayerPrefs.Save();
    }
}