using Assets.Scripts.GameResources.MapCreatures;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private LayerMask _heroLayerMask;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _point;
    [SerializeField] private TerrainTypes _type;

    [field: SerializeField] public GameMapObjectType GameMapObjectType { get; private set; }
    public bool IsTargetPoint { get; private set; }
    public TerrainTypes Type => _type;
    [field: SerializeField] public string InteractiveMapObjectId { get; private set; } = "";
    [field: SerializeField] public string ParentObjectId { get; private set; } = "";
    public int CellPathCost { get; private set; }
    public string StartCellId { get; set; }
    public Castle Castle { get; private set; }
    public MineStructure MineStructure { get; private set; }
    private Color _arrowColor;
    private GameModel _gameModel;
    public GameMapObject CreatureModelObject { get; private set; }
    public HeroModelObject HeroModelObject { get; private set; }
    public GameMapObject GameMapObject { get; private set; }
    public Transform Arrow => _arrow.transform;
    private string _baseInteractiveMapObjectId = "";
    [field: SerializeField] public int X { get; set; }
    [field: SerializeField] public int Y { get; set; }  

    public void SetPosition(int x , int y)
    {
        X = x; 
        Y = y;
    }

    public void SetGameMapObject(GameMapObject gameMapObject)
    {
        GameMapObject = gameMapObject;
    }

    public void SetBaseInteractiveMapObjectId(string interactiveMapObjectId)
    {
        _baseInteractiveMapObjectId = interactiveMapObjectId;
    }

    public void BackToBaseInteractiveMapObjectID()
    {
        SetInteractiveMapObjectId(_baseInteractiveMapObjectId);
    }

    public void SetCasle(Castle castle)
    {
        Castle = castle;
    }

    public void SetMineStructure(MineStructure mineStructure)
    {
        MineStructure = mineStructure;
    }

    public void SetInteractiveMapObjectId(string interactiveMapObjectId)
    {

        if (InteractiveMapObjectId != "")
            return;
        InteractiveMapObjectId = interactiveMapObjectId;
        if (interactiveMapObjectId != "")
        {
            int index = _gameModel.InteractiveObjectIdToGameObjectType.FindIndex(x => x.Contains(InteractiveMapObjectId));
            GameMapObjectType = (GameMapObjectType)(index + 1);
        }
        else
        {
            GameMapObjectType = GameMapObjectType.NULL;
        }
    }

    public void SetGameMapObjectType(GameMapObjectType gameMapObjectType)
    {
        GameMapObjectType = gameMapObjectType;
    }

    public bool CheckHero()
    {
        if (Physics.Raycast(transform.position, Vector3.up, 4, _heroLayerMask))
            return true;
        return false;
    }

    public void SetParentObjectId(string id)
    {
        GameMapObjectType = GameMapObjectType.NULL;
        int index = _gameModel.InteractiveObjectIdToGameObjectType.FindIndex(x => x.Contains(ParentObjectId));
        GameMapObjectType = (GameMapObjectType)(index + 1);
        ParentObjectId = id;
    }

    public void ResetInteractiveMapObject()
    { 
        if(GameMapObjectType != GameMapObjectType.CASTLE)
        {
            GameMapObjectType = GameMapObjectType.NULL;
            CreatureModelObject = null;
            InteractiveMapObjectId = "";
        }
    }

    public void SetModelObject(GameMapObject creatureModelObject)
    {
        CreatureModelObject = creatureModelObject;
    }

    public void ResetHeroModelObject()
    {
        CreatureModelObject = null;
    }

    public void SetGameModel(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public void SetCost(int cost)
    {
        CellPathCost = cost;
    }

    public void DrawArrow(Transform target)
    {
        _arrow.SetActive(true);
        _arrow.transform.LookAt(target);
        _arrow.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _arrowColor;
    }

    public void SetColorArrow(Color color)
    {
        _arrowColor = color;
    }

    public void OnTargetMovePoint()
    {
        _point.SetActive(true);
        IsTargetPoint = true;
    }

    public void ResetCell()
    {
        IsTargetPoint = false;
        _arrowColor = Color.green;
        _arrow.SetActive(false);
        _point.SetActive(false);
    }

}