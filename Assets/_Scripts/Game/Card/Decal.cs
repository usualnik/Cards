using UnityEngine;

public class Decal : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public MeshRenderer GetDecalMeshRenderer() => _renderer; 
}
