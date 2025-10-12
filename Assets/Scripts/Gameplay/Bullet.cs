using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    //Explosion Effect
    [SerializeField] 
    private GameObject explosion;

    [SerializeField] 
    private float speed = 50.0f;
    [SerializeField] 
    private float lifeTime = 3.0f;
    [SerializeField] 
    private int damage = 50;

  
    private void Start()
    {
        // Simply destroy the gameobject after the given lifeTime duration
        Destroy(gameObject, lifeTime);
    }

   
    private void Update()
    {
        // Make the object always move forward
        transform.position += 
			transform.forward * speed * Time.deltaTime;       
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contact = collision.contacts[0];
        Instantiate(explosion, contact.point, Quaternion.identity);
        if (collision.gameObject.GetComponent<PlayerTankController>())
        {
            collision.gameObject.GetComponent<PlayerTankController>().health -= damage;
        }
        else if (collision.gameObject.GetComponent<EnemyTankController>())
        {
            collision.gameObject.GetComponent<EnemyTankController>().health -= damage;
        }
        
        Destroy(gameObject);
    }
}