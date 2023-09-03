using System.Collections.Generic;
using UnityEngine;

public class ChooseHeroPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _parent;

    private List<HeroIconPrefab> _heroIconPrefabs = new List<HeroIconPrefab>();
    private HeroIconPrefab _heroIconPrefab;
    private Heroes _heroes;
    private IChangebleHero _changebleHero;

    public void Init(HeroIconPrefab heroIconPrefab, Heroes heroes)
    {
        _heroIconPrefab = heroIconPrefab;
        _heroes = heroes;
    }

    public void ClearPanel()
    {
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }

    public void OnDestroy()
    {
        ClearPanel();
    }

    public void OpenPanelToSelectHeroForParitipantSlot(IChangebleHero changebleHero , IReadOnlyCollection<Hero> heroes)
    {
        foreach (var item in _heroIconPrefabs)
        {
            item.OnSelectedHero -= SelectHero;
        }
        foreach (var item in _heroIconPrefabs)
        {
            Destroy(item.gameObject);
        }
        _heroIconPrefabs.Clear();

        foreach (var item in heroes)
        {
            var icon = Instantiate(_heroIconPrefab, _parent);
            icon.Init(item);
            icon.OnSelectedHero += SelectHero;
            _heroIconPrefabs.Add(icon);
        }
        _changebleHero = changebleHero;
        _panel.SetActive(true);

    }

    public void SelectHero(Hero hero)
    {
        _changebleHero.ChangeHero(hero);
        _panel.SetActive(false);
    }

}
 