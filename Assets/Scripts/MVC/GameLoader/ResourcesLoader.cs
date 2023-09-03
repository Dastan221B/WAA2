using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader
{
    private ResourcesSturctures _resourcesSturctures;
    private GameModel _gameModel;

    public ResourcesLoader(ResourcesSturctures resourcesSturctures, GameModel gameModel)
    {
        _resourcesSturctures = resourcesSturctures;
        _gameModel = gameModel;
    }

    public List<ResourceSturcture> CreateStructures(Dictionary<string, ResourceObject> resources)
    {
        List<ResourceSturcture> resourceSturctures = new List<ResourceSturcture>();
        foreach(var resource in resources)
        {
            ResourceSturcture resourceSturcture = MonoBehaviour.Instantiate(_resourcesSturctures.GetResourceSturctureByID(resource.Value.DicResourceId),
                new Vector3(resource.Value.coordinates.x, 0.55f, resource.Value.coordinates.y), Quaternion.identity, _gameModel.TerrainObjectsParent);
            resourceSturcture.SetMapObjectID(resource.Key);
            resourceSturcture.SetDicResourceID(resource.Value.DicResourceId + 1);
            resourceSturctures.Add(resourceSturcture);
        }
        return resourceSturctures;
    }
}