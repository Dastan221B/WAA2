using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoursorService : MonoBehaviour
{
    [SerializeField] private LayerMask _cell;


    [SerializeField] private Camera _camera;
    [SerializeField] private Texture2D _default;
    [SerializeField] private Texture2D _castle;
    [SerializeField] private Texture2D _move;
    [SerializeField] private Texture2D _attack;
    [SerializeField] private Texture2D _hero;

    [SerializeField] private CursorMode _cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 _hotSpot = Vector2.zero;

    private GameModel _gameModel;

    private void Awake()
    {
        Cursor.SetCursor(_default, _hotSpot , _cursorMode);
        _gameModel = FindObjectOfType<GameController>().GameModel;
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void Update()
    {

        if (!CheckToUI() && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _cell))
        {
            if (hit.transform.TryGetComponent(out Cell cell))
            {
                if(cell.GameMapObjectType == GameMapObjectType.NULL)
                {
                    Cursor.SetCursor(_move, _hotSpot, _cursorMode);
                }else if (cell.GameMapObjectType == GameMapObjectType.HERO)
                {
                    if (_gameModel.TryGetHeroModelObjectForCoursor(cell.CreatureModelObject.MapObjectID, out HeroModelObject heroModelObject1))
                    {
                        Cursor.SetCursor(_hero, _hotSpot, _cursorMode);
                    }
                    else
                    {
                        Cursor.SetCursor(_attack, _hotSpot, _cursorMode);
                    }
                }
                else if (cell.GameMapObjectType == GameMapObjectType.CASTLE)
                {
                    Cursor.SetCursor(_castle, _hotSpot, _cursorMode);
                }
                else if (cell.GameMapObjectType == GameMapObjectType.CREATURE)
                {
                    Cursor.SetCursor(_attack, _hotSpot, _cursorMode);
                }
                else if (cell.GameMapObjectType == GameMapObjectType.RESOURCE)
                {
                    Cursor.SetCursor(_move, _hotSpot, _cursorMode);
                }
                else if (cell.GameMapObjectType == GameMapObjectType.MINE)
                {
                    Cursor.SetCursor(_move, _hotSpot, _cursorMode);
                }
                else
                {
                    Cursor.SetCursor(_default, _hotSpot, _cursorMode);
                }
            }
            else
            {
                Cursor.SetCursor(_default, _hotSpot, _cursorMode);
            }

            //if (hit.transform.TryGetComponent(out Castle castle))
            //{
            //    Cursor.SetCursor(_castle, _hotSpot, _cursorMode);
            //}
            //else if (hit.transform.TryGetComponent(out CreatureModelObject creatureModelObject))
            //{
            //    Cursor.SetCursor(_attack, _hotSpot, _cursorMode);
            //}
            //else if (hit.transform.TryGetComponent(out HeroModelObject heroModelObject))
            //{
            //    if (_gameModel.TryGetHeroModelObjectForCoursor(heroModelObject.MapObjectID, out HeroModelObject heroModelObject1))
            //    {
            //        Cursor.SetCursor(_hero, _hotSpot, _cursorMode);
            //    } else
            //    {
            //        Cursor.SetCursor(_attack, _hotSpot, _cursorMode);
            //    }
            //    Debug.Log("IdHero" + heroModelObject.DicHeroId + " " + heroModelObject.MapObjectID);
            //}
            //else
            //{
            //    Cursor.SetCursor(_default, _hotSpot, _cursorMode);
            //}
        }
        else
        {
            Cursor.SetCursor(_default, _hotSpot, _cursorMode);
        }
    }

    private bool CheckToUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
            return true;
        return false;
    }

}
