using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HireCreatureSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private Image _icon;
    private Building _building;
    private CastleController _controller;

    public void Init(Sprite icon, Building building , CastleController castleController)
    {
        if (icon != null)
        {
            _controller = castleController;
            _building = building;
            _icon.color = new Color(1, 1, 1, 1);
            _icon.sprite = icon;
        }    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _controller.OpenBuilding(_building);
    }

    public void ResetIcons()
    {
        _icon.color = new Color(0, 0, 0, 0);
        _icon.sprite = null;
    }
}
