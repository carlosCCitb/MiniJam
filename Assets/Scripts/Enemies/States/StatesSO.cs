using UnityEngine;
using System.Collections.Generic;
public abstract class StatesSO : ScriptableObject
{
    public List<StatesSO> StatesToGo;
    public abstract void OnStateEnter(EnemyController enemyController);
    public abstract void OnStateUpdate(EnemyController enemyController);
    public abstract void OnStateExit(EnemyController enemyController);
}
