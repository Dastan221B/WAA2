using Assets.Scripts.Client.DTO;
using System;
using UnityEngine;

[Serializable]
public class Hero
{
    [SerializeField] private HeroMapObject _heroGameObject;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _heroID;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int HeroID => _heroID;
    public HeroMapObject HeroMapObject => _heroGameObject;

    public void Init(DicHeroDTO heroDTO)
    {
        _name = heroDTO.name;
        _heroID = heroDTO.id;
    }
}