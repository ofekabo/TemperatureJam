using System;
using UnityEngine;
using Pathfinding;

public class BaseEnemy : MonoBehaviour
{
   

    [Header("State Machine")]
    public MonsterStateID initState;
    public MonsterStateMachine StateMachine;
    public Transform player;
    public LayerMask excludeLayers = new LayerMask();

    [Header("Temp Control")]
    [SerializeField] int deathTemp;
    [SerializeField] int tempDifference = 3;
    [SerializeField] bool basicHealth = false;
    [HideInInspector] public TempControl tempControl;
    [HideInInspector] public Attacker attacker;
    public int DeathTemp => deathTemp;
    
    [Header("A*")]
    [HideInInspector] public AIPath aiPath;

    [Header("Animation")]
    [HideInInspector] public AnimatorController animator;
    [HideInInspector] public AIMovement movement;

    public enum Type
    {
        Lava,
        Ice
    }

    public Type enemyType;
    
    
    int _tempDamage;

    #region Collision Fix
    
    bool _canDamage;
    float _fixInterval;
    float colliderFixDelay = 0.1f;
    
    #endregion


    public virtual void Awake()
    {
        tempControl = GetComponent<TempControl>();
        aiPath = GetComponent<AIPath>();
        movement = GetComponent<AIMovement>();
        animator = GetComponent<AnimatorController>();
        attacker = GetComponent<Attacker>();

        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
        
        #region StateMachine
        
        RegisterStates();
        StateMachine.ChangeState(initState);
        
        #endregion
        _canDamage = true;
    }

    public virtual void RegisterStates()
    {
        // register states
        StateMachine = new MonsterStateMachine(this);
        
        StateMachine.RegisterState(new CubeAttackPlayerState());
        StateMachine.RegisterState(new AiDeathState());
    }

    public virtual void Update()
    {
        StateMachine.Update();
        animator.CalculateAngleForAnim(new Vector2(player.position.x, player.position.y), transform.position);

        #region Collision Fix

        _fixInterval += Time.deltaTime;
            if (_canDamage == false && _fixInterval > colliderFixDelay)
            {
                _canDamage = true;
                _fixInterval = 0;
            }
            
        #endregion
        
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
        if (attacker)
        {
            switch (enemyType)
            {
                case Type.Ice: 
                    _tempDamage = -attacker.tempDamage;
                    break;
                case Type.Lava:
                    _tempDamage = attacker.tempDamage;
                    break;
            }



            PlayerController p = other.collider.GetComponent<PlayerController>();
            if (p && _canDamage)
            {
                p.tempControl.ChangeTemp(_tempDamage);
                p.healthComp.TakeDamage(attacker.healthDamage);
                _canDamage = false;
            }
        }
    }
}