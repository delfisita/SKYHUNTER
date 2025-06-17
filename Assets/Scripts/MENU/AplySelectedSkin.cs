using UnityEngine;

public class AplySelectedSkin : MonoBehaviour
{
    void Start()
    {
        ApplySkin();
    }

    public void ApplySkin()
    {
        Material skinMaterial = SkinManager.Instance.GetCurrentSkinMaterial();
        if (skinMaterial == null) return;

        GameObject[] skinModels = GameObject.FindGameObjectsWithTag("skinmodel");
        foreach (GameObject model in skinModels)
        {
            Renderer renderer = model.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = skinMaterial;
            }
        }
    }
}