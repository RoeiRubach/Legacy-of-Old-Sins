using UnityEngine;
using System.Collections.Generic;

public class CharactersPoolController : MonoBehaviour
{
    public readonly static HashSet<CharactersPoolController> Pool = new HashSet<CharactersPoolController>();

    private void OnEnable()
    {
        CharactersPoolController.Pool.Add(this);
    }

    private void OnDisable()
    {
        CharactersPoolController.Pool.Remove(this);
    }

    public static CharactersPoolController FindClosestCharacter(Vector3 pos)
    {
        CharactersPoolController result = null;
        float distance = float.PositiveInfinity;
        var enumerator = CharactersPoolController.Pool.GetEnumerator();

        while (enumerator.MoveNext())
        {
            float currentDistanceToDouglas = (enumerator.Current.transform.position - pos).sqrMagnitude;

            if (currentDistanceToDouglas < distance)
            {
                result = enumerator.Current;
                distance = currentDistanceToDouglas;
            }
        }
        return result;
    }
}