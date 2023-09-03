using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesSturctures", menuName = "ScriptableObjects/ResourcesSturctures")]

public class ResourcesSturctures : ScriptableObject
{
    [SerializeField] private ResourceSturcture[] _resourceSturctures;

    public ResourceSturcture GetResourceSturctureByID(int id)
    {
        if (id > 0 && id < _resourceSturctures.Length + 1)
            return _resourceSturctures[id - 1];
        return _resourceSturctures[0];
    }
}