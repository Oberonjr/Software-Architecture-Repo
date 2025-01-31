using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
