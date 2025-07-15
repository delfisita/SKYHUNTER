using UnityEngine;

public class HeliceRotator : MonoBehaviour
{
    public float velocidadRotacion = 500f;

    void Update()
    {
        transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
    }
}
