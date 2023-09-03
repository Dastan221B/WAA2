using Assets.Scripts.GameResources.MapCreatures;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "ModelCreatures", menuName = "ScriptableObjects/ModelCreatures")]
[ExecuteInEditMode]
public class ModelCreatures : ScriptableObject
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private List<CreaturePack> _modelCreatures;


    //private void OnEnable()
    //{
    //    Object[] subListObjects = Resources.LoadAll("Prefabs", typeof(GameObject));
    //    Debug.Log(subListObjects.Length);
    //    int id = 0;
    //    foreach (GameObject subListObject in subListObjects)
    //    {
    //        GameObject obj = (GameObject)subListObject;

    //        if(obj.TryGetComponent(out CreatureModelObject creatureModelObject))
    //        {
    //            _modelCreatures.Add(new CreaturePack(creatureModelObject, _defaultSprite, id));
    //            id++;
    //        }
    //    }
    //}

    public void TrySetDicCreatureDTO(int id, DicCreatureDTO dicCreatureDTO)
    {
        if(id < _modelCreatures.Count)
        {
            _modelCreatures[id].CreatureModelObject.SetCreatureDTO(dicCreatureDTO);
        }
    }
    public CreatureModelObject GetMapCreatureByID(int id)
    {
        if (id > _modelCreatures.Count)
            return _modelCreatures[_modelCreatures.Count - 1].CreatureModelObject;

        return _modelCreatures[id].CreatureModelObject;

        //foreach (var item in _modelCreatures)
        //{
        //    if (item.ID == id)
        //    {
        //        return item.CreatureModelObject;
        //    }
        //}
        //return creatureModelObject;
    }

    public Sprite GetIconById(int id)
    {
        if (id > _modelCreatures.Count)
            return _modelCreatures[_modelCreatures.Count - 1].Icon;

        return _modelCreatures[id].Icon;

        //foreach (var item in _modelCreatures)
        //{
        //    if (item.CreatureModelObject.DicCreatureDTO.id == id)
        //    {
        //        return item.Icon;
        //    }
        //}
        return null;
    }

    [System.Serializable]
    private struct CreaturePack
    {
        [SerializeField] private CreatureModelObject _creatureModelObject;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _id;
        public int ID => _id;


        public CreaturePack(CreatureModelObject creatureModelObject, Sprite icon , int id)
        {
            _creatureModelObject = creatureModelObject;
            _icon = icon;
            _id = id;
        }

        public CreatureModelObject CreatureModelObject => _creatureModelObject;
        public Sprite Icon => _icon;
    }
}