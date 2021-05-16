using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooterState : MonsterState
{
    private Shooter _shooter;

    public MonsterStateID GetID()
    {
        return MonsterStateID.AttackPlayer;
    }

    public void Enter(BaseEnemy enemy)
    {
        _shooter = enemy.GetComponent<Shooter>();
    }

    public void Update(BaseEnemy enemy)
    {
        _shooter.UpdateRuntime();


        if (!RaycastCheck(enemy))
        {
            return;
        }

        switch (enemy.enemyType)
        {
            case BaseEnemy.Type.Ice:
                _shooter.IceShot();
                break;

            case BaseEnemy.Type.Lava:
                _shooter.LavaShot();
                break;
        }
    }

    public void Exit(BaseEnemy enemy)
    {

    }

    bool RaycastCheck(BaseEnemy enemy)
    {

        Vector2 dir = (enemy.player.position - enemy.transform.position).normalized;
        Ray2D ray = new Ray2D(enemy.transform.position, dir);

        Debug.DrawRay(ray.origin, ray.direction * 14, Color.cyan);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 14, enemy.excludeLayers);

        PlayerController p = hit.collider.GetComponent<PlayerController>();

        if (p)
        {
            return true;
        }

        return false;
    }
}
    
