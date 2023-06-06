using UnityEngine;

public interface IFindTarget 
{
    GameObject CurrentTarget { get; }

    void FindTarget();
}
