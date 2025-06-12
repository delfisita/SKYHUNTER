using UnityEngine;
using UnityEngine.UI;

public class Shopitem : MonoBehaviour
{
    public string skinID;
    public int price;

    public Text skinNameText;
    public Text priceText;
    public Button buyButton;

    void Start()
    {
        skinNameText.text = skinID;
        priceText.text = price + " pts";

        buyButton.onClick.AddListener(OnBuyClicked);
    }

    void OnBuyClicked()
    {
        int currentPoints = PointsManager.GetPoints();
        if (currentPoints >= price)
        {
            PointsManager.SpendPoints(price);
            Debug.Log("Compraste: " + skinID);
           
        }
        else
        {
            Debug.Log("No tenés suficientes puntos.");
        }
    }
}
