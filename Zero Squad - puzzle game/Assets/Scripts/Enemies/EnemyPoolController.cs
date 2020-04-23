using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolController : MonoBehaviour
{
    public readonly static HashSet<EnemyPoolController> Pool = new HashSet<EnemyPoolController>();

    private void OnEnable()
    {
        EnemyPoolController.Pool.Add(this);
    }

    private void OnDisable()
    {
        EnemyPoolController.Pool.Remove(this);
    }

    public static EnemyPoolController FindClosestEnemy(Vector3 douglasPos)
    {
        EnemyPoolController result = null;
        float distance = float.PositiveInfinity;
        var enumerator = EnemyPoolController.Pool.GetEnumerator();
        
        while (enumerator.MoveNext())
        {
            float currentDistanceToDouglas = (enumerator.Current.transform.position - douglasPos).sqrMagnitude;

            if (currentDistanceToDouglas < distance)
            {
                result = enumerator.Current;
                distance = currentDistanceToDouglas;
            }
        }
        return result;
    }
}