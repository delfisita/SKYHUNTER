using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuPointsDisplay : MonoBehaviour
{
    public TMP_Text pointsText; // Asigna esto en el inspector a tu objeto Text

    void Start()
    {
        UpdatePointsDisplay();
    }

    public void UpdatePointsDisplay()
    {
        // Obtener los puntos guardados (0 si no existen)
        int totalPoints = PlayerPrefs.GetInt("TotalPoints", 0);
        pointsText.text = $"Puntos: {totalPoints}";
    }

    // Opcional: método para resetear los puntos (para testing)
    public void ResetPoints()
    {
        PlayerPrefs.DeleteKey("TotalPoints");
        UpdatePointsDisplay();
    }
}
