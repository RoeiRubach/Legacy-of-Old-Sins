using UnityEngine;

public class ElenaStealthManager : MonoBehaviour
{
    [SerializeField]
    private Material elenaOGMaterial;

    [SerializeField]
    private Material stealthMode;

    [SerializeField]
    private SkinnedMeshRenderer[] elenaClothesMaterial;

    private bool isInStealthMode;

    public void CallStealthMode()
    {
        for (int i = 0; i < elenaClothesMaterial.Length; i++)
        {
            elenaClothesMaterial[i].material = stealthMode;
            isInStealthMode = true;
        }
    }

    public void OffStealthMode()
    {
        for (int i = 0; i < elenaClothesMaterial.Length; i++)
        {
            elenaClothesMaterial[i].material = elenaOGMaterial;
            isInStealthMode = false;
        }
    }

    public bool IsElenaUsingStealth()
    {
        return isInStealthMode;
    }
}
