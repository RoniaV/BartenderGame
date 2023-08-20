using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IDamageDealer
{
    [SerializeField] float minVeloccity;
    [SerializeField] Vector3 centerOfMass = Vector3.zero;
    [SerializeField] float lifetime = 5;

    private Rigidbody rb;

    void Awake()
    {
        SetRigidbody();
    }

    void OnEnable()
    {
        StartCoroutine(Lifetime());
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(rb.velocity.magnitude > minVeloccity 
            && coll.gameObject.GetComponent<IDamageable>() != null)
        {
            DoDamage(coll.gameObject.GetComponent<IDamageable>());
        }
    }

    public void LaunchBullet(Vector3 direction, float force)
    {
        SetRigidbody();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction * force);
    }

    public void LaunchBullet(Vector3 direction, float force, Vector3 initialVelocity)
    {
        SetRigidbody();

        rb.velocity = initialVelocity;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction * force);
    }

    public void DoDamage(IDamageable target)
    {
        target.ReceiveDamage();
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);

        gameObject.SetActive(false);
    }

    private void SetRigidbody()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = centerOfMass;
        }
    }
}
