using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 500.0f)] float m_force = 10.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_lifetime = 1.5f;
    [SerializeField] GameObject m_explosion = null;

    void Start()
    {
        // Gives the projectile force to send it forward
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * m_force, ForceMode.Impulse);
        // Destroy object after lifetime has expired
        Destroy(gameObject, m_lifetime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_explosion, collision.transform.position, collision.transform.rotation);
        

        // Issue damage to game object that possesses Health script
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<Health>().Damage(20.0f);
        }

        // Destroy object after object collides with something
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
