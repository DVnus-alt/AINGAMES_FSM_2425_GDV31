using UnityEngine;

public class AttackState : FSMState
{
    public override StateID StateId => StateID.Attack;
    private EnemyTankController _enemyTankController;
    PlayerTankController playerTank;

    public AttackState(EnemyTankController enemyTankController)
    {
        _enemyTankController = enemyTankController;
    }

    public override void CheckTransition(Transform agent, Transform player)
    {
        if (Vector3.Distance(agent.position, player.position) > _enemyTankController.ChaseDistance)
        {
            _enemyTankController.PerformTransition(TransitionID.LostPlayer);
        }
    }

    public override void RunState(Transform agent, Transform player)
    {
        Vector3 additional = new Vector3(0, 1, 0);
        Quaternion targetRotation = Quaternion.LookRotation(player.position - _enemyTankController.turret.position + additional);
        _enemyTankController.turret.transform.rotation = Quaternion.Slerp(_enemyTankController.turret.transform.rotation, targetRotation, Time.deltaTime * _enemyTankController.turretRotSpeed);
        if (_enemyTankController.elapsedTime >= _enemyTankController.shootRate)
        {
            //Reset the time
            _enemyTankController.elapsedTime = 0.0f;
            //Spawn the bullet
            _enemyTankController.Shoot();


        }
        _enemyTankController.elapsedTime += Time.deltaTime;
    }
}
