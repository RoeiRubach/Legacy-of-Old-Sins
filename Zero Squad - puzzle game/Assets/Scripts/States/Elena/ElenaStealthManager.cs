using UnityEngine;

public class ElenaStealthManager : MonoBehaviour
{
    [SerializeField] private Material _elenaBodyNHeadMaterial, _elenaCapeNHoodMaterial;

    [SerializeField] private Material _stealthMode;

    [SerializeField] private SkinnedMeshRenderer[] _elenaBodyNHeadArray;
    [SerializeField] private SkinnedMeshRenderer[] _elenaCapeNHoodArray;

    [SerializeField] private GameObject _elenaHairRef, _elenaHoodRef;

    private bool _isInStealthMode;

    public void CallStealthMode()
    {
        for (int i = 0; i < _elenaBodyNHeadArray.Length; i++)
        {
            _elenaBodyNHeadArray[i].material = _stealthMode;
        }
        for (int i = 0; i < _elenaCapeNHoodArray.Length; i++)
        {
            _elenaCapeNHoodArray[i].material = _stealthMode;
        }

        _elenaHairRef.SetActive(false);
        _elenaHoodRef.SetActive(true);
        _isInStealthMode = true;
    }

    public void OffStealthMode()
    {
        for (int i = 0; i < _elenaBodyNHeadArray.Length; i++)
        {
            _elenaBodyNHeadArray[i].material = _elenaBodyNHeadMaterial;
        }
        for (int i = 0; i < _elenaCapeNHoodArray.Length; i++)
        {
            _elenaCapeNHoodArray[i].material = _elenaCapeNHoodMaterial;
        }

        _elenaHairRef.SetActive(true);
        _elenaHoodRef.SetActive(false);
        _isInStealthMode = false;
    }

    public bool IsElenaUsingStealth()
    {
        return _isInStealthMode;
    }
}
