using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeTime = 3f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Hit player!");
            Destroy(gameObject);
          
        }
    }
}
