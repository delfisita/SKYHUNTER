using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private int moveDirection = 0;

    private void Update()
    {
        Vector3 movement = new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }


    public void MoveLeft()
    {
        moveDirection = -1;
    }

    public void MoveRight()
    {
        moveDirection = 1;
    }

    public void StopMoving()
    {
        moveDirection = 0;
    }
}
