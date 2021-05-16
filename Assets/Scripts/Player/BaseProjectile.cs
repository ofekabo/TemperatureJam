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

    public void SpawnEffectNDestroy(Collider2D other)
    {
        BaseProjectile bp = other.GetComponent<BaseProjectile>();
        if (bp)
        {
            Destroy(gameObject,0.01f);
            return;
        }
        var clone = Instantiate(hitEffect, transform.position, quaternion.identity);
        Destroy(clone, 0.5f);
        Destroy(gameObject, 0.05f);
    }
    
    private void OnDestroy()
    {
        hitEffect = null;
        _rb = null;
    }

    public void CheckCollision(Collider2D other,GameObject diffExplosive,GameObject sameExplosive,float radius,float force)
    {
         var p = other.GetComponent<BaseProjectile>();
         if(!p) 
         {
             SpawnEffectNDestroy(other);
             return;
         }
         if (bulletType == Type.Lava)
         {
             if (p.bulletType == Type.Ice)
             {
                 // diff
                 SpawnExplosive(diffExplosive,radius,force);
             }

             if (p.bulletType == Type.Lava)
             {
                 // same
                 SpawnExplosive(sameExplosive,radius,force);
             }
         }

         if (bulletType == Type.Ice)
         {
             if (p.bulletType == Type.Lava)
             {
                 // diff
                 SpawnExplosive(diffExplosive,radius,force);
             }
             if (p.bulletType == Type.Ice)
             {
                 // same
                 SpawnExplosive(sameExplosive,radius,force);
             }
         }
         
         Destroy(gameObject, 0.01f);
    }

    void SpawnExplosive(GameObject prefab,float explosiveRadius, float force)
    {
        var clone = Instantiate(prefab, transform.position, quaternion.identity);
        Destroy(clone, 1.2f);
        Destroy(gameObject, 0.05f);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosiveRadius);

        foreach (var nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            BaseProjectile bp = nearbyObject.GetComponent<BaseProjectile>();

            if (rb && !bp)
            {
                Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(dir * force,ForceMode2D.Force);
            }
        }
    }
}