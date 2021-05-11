using System;
using UnityEngine;
using Pathfinding;

public class BaseEnemy : MonoBehaviour
{
    public Transform player;

    [Header("State Machine")] public MonsterStateID initState;
    public MonsterStateMachine StateMachine;

    [Header("Temp Control")] [SerializeField]
    int deathTemp;

    [SerializeField] int tempDifference = 3;
    [SerializeField] bool basicHealth = false;
    [HideInInspector] public TempControl tempControl;
    [HideInInspector] public Attacker attacker;

    [Header("A*")] [HideInInspector] public AIPath aiPath;

    [Header("Animation")] [HideInInspector]
    public AnimatorController animator;

    public int DeathTemp => deathTemp;

    public enum Type
    {
        Lava,
        Ice
    }

    public Type enemyType;
    int damage;

    [HideInInspector] public AIMovement movement;
    
    

    private void Awake()
    {
        tempControl = GetComponent<TempControl>();
        aiPath = GetComponent<AIPath>();
        movement = GetComponent<AIMovement>();
        animator = GetComponent<AnimatorController>();
        attacker = GetComponent<Attacker>();

        #region StateMachine

        StateMachine = new MonsterStateMachine(this);

        // register states
        StateMachine.RegisterState(new AiAttackPlayerState());
        StateMachine.RegisterState(new AiDeathState());

        StateMachine.ChangeState(initState);

        #endregion
    }

    private void Update()
    {
        StateMachine.Update();
        animator.CalculateAngleForAnim(new Vector2(player.position.x, player.position.y), transform.position);
    }

    private void FixedUpdate()
    {
        movement.AiLocomotion();
    }


    public void UpdateTemp()
    {
        if (!basicHealth)
        {
            if (Mathf.Abs(tempControl.Temperature - deathTemp) <= tempDifference)
            {
                Destroy(gameObject);
            }

            return;
        }

        if (enemyType == Type.Lava && tempControl.Temperature <= deathTemp)
        {
            Destroy(gameObject);
        }

        if (enemyType == Type.Ice && tempControl.Temperature >= deathTemp)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (enemyType)
        {
            case Type.Ice:
                damage = -attacker.damage;
                break;
            case Type.Lava:
                damage = attacker.damage;
                break;
        }
       
        
        PlayerController p = other.collider.GetComponent<PlayerController>();
        if (p)
        {
            p.tempControl.ChangeTemp(damage);
        }
    }
}