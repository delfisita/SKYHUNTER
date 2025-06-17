using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    private int moveDirection = 0;
    private float initialPositionX;

    private PlayerController controller;

    private void Start()
    {
       
        initialPositionX = transform.position.x;
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (controller.isShooter) return;

        if (moveDirection != 0)
        {
           
            Vector3 movement = new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else
        {
          
            float newX = Mathf.Lerp(transform.position.x, initialPositionX, 2f * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
    public void MoveLeft()
    {
        if (controller.isShooter) return;
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
