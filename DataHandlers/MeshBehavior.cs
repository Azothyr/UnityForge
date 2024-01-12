using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Material))]
public class MeshBehavior : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material _material;
    private Color _color;
    
    private void OnEnable()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _color = _material.color;
    }

    public void SetOpacity(float value)
    {
        if (_meshRenderer != null)
        {
            value = value switch
            {
                < 0 => 0,
                > 1 => 1,
                _ => value
            };
            _color.a = value;
            _meshRenderer.material.color = _color;
        }
    }
}
