using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HeroIconPrefab : MonoBehaviour , IPointerClickHandler
{
    public event Action<Hero> OnSelectedHero;
    [SerializeField] private Image _heroIconImage;
    [SerializeField] private TMP_Text _heroNameText;
    private Hero _hero;

    public void Init(Hero hero)
    {
        _heroIconImage.sprite = hero.Icon;
        _heroNameText.text = hero.Name;
        _hero = hero;
    }

    public void ChooseHero()
    {
        OnSelectedHero(_hero);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChooseHero();
    }
}