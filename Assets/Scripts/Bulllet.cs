using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shooterID;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}