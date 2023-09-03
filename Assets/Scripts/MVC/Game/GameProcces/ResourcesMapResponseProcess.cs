using Assets.Scripts.GameResources;
using System.Collections;
using UnityEngine;

public class ResourcesMapResponseProcess : MonoBehaviour
{
    private GameLoadedData _gameLoadedData;
    private ResourcesDataService _resourcesDataService;

    public ResourcesMapResponseProcess(ResourcesDataService resourcesDataService ,GameLoadedData gameLoadedData)
    {
        _resourcesDataService = resourcesDataService;
        _gameLoadedData = gameLoadedData;
    }

    public void InitData(MessageInput message, bool isLoadedGame)
    {
        ResourcesMapResponse resourcesInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResourcesMapResponse>(message.body);
        foreach(var item in resourcesInfo.resources)
        {
            Debug.Log("Resource " + item.Value + " " + item.Key);
        }    
        _gameLoadedData.AddToData(resourcesInfo.resources, typeof(ResourcesData));
        _resourcesDataService.SetResources(resourcesInfo.resources);

        //if (isLoadedGame)
        //    _gameLoadedData.AddToData(resourcesInfo.resources, typeof(ResourcesData));
        //else
        //    _resourcesDataService.SetResources(resourcesInfo.resources);
    }
}