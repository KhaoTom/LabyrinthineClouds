using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Materials", order = 1)]
public class Materials : ScriptableObject
{
    public Material[] materials;

    public Material GetRandomMaterial()
    {
        return materials.RandomItem();
    }
}
