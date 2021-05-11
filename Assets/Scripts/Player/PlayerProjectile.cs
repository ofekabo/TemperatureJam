using Unity.Mathematics;
using UnityEngine;


public class PlayerProjectile : MonoBehaviour
{
    const int Ice = 0;
    const int Lava = 1;


    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    public GameObject hitEffect;
    public float force = 500f;

    private Vector2 _projectileInitPos;
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
        _projectileInitPos = transform.position;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        TempControl temp = other.GetComponent<TempControl>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (temp && enemy)
        {
            if (bulletType == Type.Ice)
            {
                temp.ChangeTempAI(-6);
            }

            if (bulletType == Type.Lava)
            {
                temp.ChangeTempAI(5);
            }

            if (rb)
            {
                Vector2 dir = (rb.position - _projectileInitPos).normalized;
                rb.AddForce(dir * force);
            }

            enemy.UpdateTemp();
        }

        PlayerController p = other.GetComponent<PlayerController>();
        PlayerProjectile proj = other.GetComponent<PlayerProjectile>();
        if (!p && !proj)
        {
            var clone = Instantiate(hitEffect, transform.position, quaternion.identity);
            Destroy(clone, 0.5f);
            Destroy(gameObject, 0.05f);
        }
    }

    private void OnDestroy()
    {
        hitEffect = null;
        _rb = null;
    }
}