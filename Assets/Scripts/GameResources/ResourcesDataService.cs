using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameResources
{
    public class ResourcesDataService : MonoBehaviour
    {
        public event System.Action OnUpdatedResources;

        [SerializeField] private Dictionary<ResourceTypes, int> _resources = new Dictionary<ResourceTypes, int>()
        {
            [ResourceTypes.WOOD] = 10,
            [ResourceTypes.MERCURY] = 4,
            [ResourceTypes.ORE] = 10,
            [ResourceTypes.SULFUR] = 4,
            [ResourceTypes.CRYSTAL] = 4,
            [ResourceTypes.GEM] = 4,
            [ResourceTypes.GOLD] = 10000,
        };

        public int GetResoucesValueByType(ResourceTypes resourcesTypes)
        {
            if (_resources.TryGetValue(resourcesTypes, out int value))
                return value;
            return 0;
        }

        public void ChangeResourceCountByID(int resourceID, int resoucesCount)
        {
            _resources[(ResourceTypes)resourceID] = resoucesCount;
            OnUpdatedResources?.Invoke();
        }

        public void SetResources(Dictionary<int, Resource> resources)
        {
            foreach(var item in _resources.Keys.ToList())
            {
                if(resources.TryGetValue((int)item, out Resource resource))
                {
                    Debug.Log((ResourceTypes)_resources[item] + " + " + resource.Amount);
                    _resources[item] = resource.Amount;
                }
            }
            OnUpdatedResources?.Invoke();
        }

        public Dictionary<int, int> GetResources()
        {
            Dictionary<int, int> resourcesDict = new Dictionary<int, int>();
            for (int i = 0; i < _resources.Values.Count; i++)
            {
                if(_resources.TryGetValue((ResourceTypes)i + 1, out int count))
                    resourcesDict.Add(i + 1, count);
            }
            return resourcesDict;
        }
    }
}