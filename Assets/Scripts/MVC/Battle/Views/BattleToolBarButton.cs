using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleToolBarButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _hoverSprite;
    private Sprite _baseSprite;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _baseSprite = _image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _baseSprite;
    }
}
