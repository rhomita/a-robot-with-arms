using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Puzzle;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [Header("Behaviour")] 
    [SerializeField] private Trigger trigger;
    
    [Header("Visual")] 
    [SerializeField] private Material _activatedMaterial;
    [SerializeField] private Material _deactivatedMaterial;
    [SerializeField] private int _materialToChangeIndex;
    
    private List<MeshRenderer> _meshesToChange;

    void Awake()
    {
        _meshesToChange = GetComponentsInChildren<MeshRenderer>().ToList();
    }
    
    void Start()
    {
        trigger.OnActivateTrigger += Activate;
        trigger.OnDeactivateTrigger += Deactivate;
    }
    
    void Activate()
    {
        ChangeMaterial(_activatedMaterial);
    }

    void Deactivate()
    {
        ChangeMaterial(_deactivatedMaterial);
    }

    void ChangeMaterial(Material material)
    {
        foreach (MeshRenderer meshRenderer in _meshesToChange)
        {
            Material[] materials = meshRenderer.materials;
            materials[_materialToChangeIndex] = material;
            meshRenderer.materials = materials;
        }
    }
}
