using UnityEngine;

public class RandomizeMaterial : MonoBehaviour
{
    public Materials Materials;
    
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = Materials.GetRandomMaterial();
    }
}
