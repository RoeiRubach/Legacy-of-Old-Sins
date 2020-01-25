using UnityEngine;

public class ElenaStealthManager : MonoBehaviour
{
    [SerializeField] private Material _elenaOGMaterial;

    [SerializeField] private Material _stealthMode;

    [SerializeField] private SkinnedMeshRenderer[] _elenaClothesMaterial;

    private bool _isInStealthMode;

    public void CallStealthMode()
    {
        for (int i = 0; i < _elenaClothesMaterial.Length; i++)
        {
            _elenaClothesMaterial[i].material = _stealthMode;
            _isInStealthMode = true;
        }
    }

    public void OffStealthMode()
    {
        for (int i = 0; i < _elenaClothesMaterial.Length; i++)
        {
            _elenaClothesMaterial[i].material = _elenaOGMaterial;
            _isInStealthMode = false;
        }
    }

    public bool IsElenaUsingStealth()
    {
        return _isInStealthMode;
    }
}
