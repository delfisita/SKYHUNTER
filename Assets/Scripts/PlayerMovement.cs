using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    private int moveDirection = 0;

    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void MoveLeft()
    {
        if (controller.isShooter) return; // no se mueve si está disparando
        moveDirection = -1;
    }

    public void MoveRight()
    {
        if (controller.isShooter) return;
        moveDirection = 1;
    }

    public void StopMoving()
    {
        moveDirection = 0;
    }
}
