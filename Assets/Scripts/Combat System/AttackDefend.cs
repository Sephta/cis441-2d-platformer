using UnityEngine;

interface ICombatible
{
    void OnEntityAttack();
    void OnEntityDefend();
    void OnEntityDefend(GameObject defendFrom);
}
