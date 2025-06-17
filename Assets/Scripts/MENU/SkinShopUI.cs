using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopUI : MonoBehaviour
{
    [System.Serializable]
    public class SkinButton
    {
        public int skinID;
        public Button actionButton;
        public TMP_Text buttonText;
    }

    public SkinButton[] skinButtons;
    public TMP_Text pointsText;

    void Start()
    {
        UpdateShopUI();
    }

    void UpdateShopUI()
    {
        int currentPoints = PlayerPrefs.GetInt("TotalPoints", 0);
        pointsText.text = $"Puntos: {currentPoints}";

        foreach (SkinButton skinButton in skinButtons)
        {
            SkinManager.Skin skin = SkinManager.Instance.allSkins.Find(s => s.skinID == skinButton.skinID);
            if (skin == null) continue;

            if (SkinManager.Instance.IsSkinPurchased(skin.skinID))
            {
                bool isSelected = (PlayerPrefs.GetInt("SelectedSkinID", -1) == skin.skinID);
                skinButton.buttonText.text = isSelected ? "Seleccionado" : "Seleccionar";
            }
            else
            {
                skinButton.buttonText.text = $"Comprar ({skin.price})";
            }

            skinButton.actionButton.onClick.RemoveAllListeners();
            skinButton.actionButton.onClick.AddListener(() => OnSkinButtonClicked(skin.skinID));
        }
    }

    void OnSkinButtonClicked(int skinID)
    {
        SkinManager.Skin skin = SkinManager.Instance.allSkins.Find(s => s.skinID == skinID);
        if (skin == null) return;

        if (SkinManager.Instance.IsSkinPurchased(skinID))
        {
            // Seleccionar skin ya comprado
            SkinManager.Instance.SelectSkin(skinID);
            UpdateShopUI();
        }
        else
        {
            // Intentar comprar
            int currentPoints = PlayerPrefs.GetInt("TotalPoints", 0);
            if (currentPoints >= skin.price)
            {
                currentPoints -= skin.price;
                PlayerPrefs.SetInt("TotalPoints", currentPoints);
                SkinManager.Instance.PurchaseSkin(skinID);
                SkinManager.Instance.SelectSkin(skinID);
                UpdateShopUI();
            }
            else
            {
                Debug.Log("Puntos insuficientes");
                // Aquí puedes añadir feedback visual/sonoro
            }
        }
    }
}