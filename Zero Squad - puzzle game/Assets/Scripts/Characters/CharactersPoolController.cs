using UnityEngine;
using System.Collections.Generic;

public class CharactersPoolController : MonoBehaviour
{
    public readonly static HashSet<CharactersPoolController> Pool = new HashSet<CharactersPoolController>();

    private void OnEnable()
    {
        Pool.Add(this);
    }

    private void OnDisable()
    {
        Pool.Remove(this);
    }

    public static CharactersPoolController FindClosestCharacter(Vector3 pos, EnemyTargetDetecting enemyTargetDetecting, ElenaStealthManager elenaStealthManager)
    {
        CharactersPoolController result = null;
        float distance = float.PositiveInfinity;
        var enumerator = Pool.GetEnumerator();

        while (enumerator.MoveNext())
        {
            float currentDistanceToCharacter = (enumerator.Current.transform.position - pos).sqrMagnitude;

            if (currentDistanceToCharacter < distance)
            {
                if (enumerator.Current.CompareTag(CharactersEnum.Elena.ToString()))
                {
                    if ((!enemyTargetDetecting.IsElenaBeenSpotted || elenaStealthManager.IsInStealthMode)) continue;
                }
                result = enumerator.Current;
                distance = currentDistanceToCharacter;
            }
        }
        return result;
    }

    public static CharactersPoolController FindClosestCharacter(Vector3 pos)
    {
        CharactersPoolController result = null;
        float distance = float.PositiveInfinity;
        var enumerator = Pool.GetEnumerator();

        while (enumerator.MoveNext())
        {
            float currentDistanceToCharacter = (enumerator.Current.transform.position - pos).sqrMagnitude;

            if (currentDistanceToCharacter < distance)
            {
                result = enumerator.Current;
                distance = currentDistanceToCharacter;
            }
        }
        return result;
    }
}