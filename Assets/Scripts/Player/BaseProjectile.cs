using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;


public class BaseProjectile : MonoBehaviour
{
    const int Ice = 0;
    const int Lava = 1;


    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    public GameObject hitEffect;
    public float force = 500f;

    protected Vector2 projectileInitPos;
    private Rigidbody2D _rb;

    public enum Type
    {
        Ice,
        Lava
    }

    public Type bulletType;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
        projectileInitPos = transform.position;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime > 0)
        {
            return;
        }

        Destroy(gameObject);
    }

    public void SpawnEffectNDestroy()
    {
        var clone = Instantiate(hitEffect, transform.position, quaternion.identity);
        Destroy(clone, 0.5f);
        Destroy(gameObject, 0.05f);
    }
    
    private void OnDestroy()
    {
        hitEffect = null;
        _rb = null;
    }
}