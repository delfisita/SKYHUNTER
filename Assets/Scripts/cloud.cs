using UnityEngine;

public class cloud : MonoBehaviour
{
    public float speed = 1f;
    public float resetPositionX = -10f;
    public float startPositionX = 10f;

    void Update()
    {
        // Mover la nube hacia la izquierda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Si se sale por la izquierda, reaparece a la derecha
        if (transform.position.x < resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
