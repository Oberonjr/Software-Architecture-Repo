using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the velocity of a single projectile as per the shootingPoint->target vector
//Also makes sure the projectile can destroy itself after some time if it does not collide
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float timer = 2f;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool decayable = true;
    
    public void SetVelocity(Vector3 pVelocity)
    {
        velocity = pVelocity;
    }

    void Start()
    {
        if(decayable) Invoke("DestroySelf", timer);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * velocity);
    }
    
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
