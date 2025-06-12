using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopitemPrefab;
    public Transform contentParent;
    public Text pointsText;

    [System.Serializable]
    public class SkinData
    {
        public string skinID;
        public int price;
    }

    public SkinData[] skins;

    void Start()
    {
        UpdatePoints();
        LoadItems();
    }

    void UpdatePoints()
    {
        pointsText.text = "Puntos: " + PointsManager.GetPoints();
    }

    void LoadItems()
    {
        foreach (SkinData skin in skins)
        {
            GameObject item = Instantiate(shopitemPrefab, contentParent);
            Shopitem script = item.GetComponent<Shopitem>();

            script.skinID = skin.skinID;
            script.price = skin.price;
        }
    }
}
