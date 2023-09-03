using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.TradeMVC
{
    public class TradeController : MonoBehaviour
    {
        [SerializeField] private LayerMask _slotLayerMask;
        private TradeModel _tradeModel;
        private TradeCreatureSlot _lastPickSlot;
        public bool InTrade { get; private set; }


        public void Init(TradeModel tradeModel)
        {
            _tradeModel = tradeModel;
        }

        public void EnterInTradeAndSetPariticipants(HeroObjectFullInfo requesterHeroInfo , HeroObjectFullInfo receiverHeroInfo , Sprite requestIcon, Sprite receiverIcon)
        {
            InTrade = true;
            _tradeModel.SetTradeParticipants(requesterHeroInfo, receiverHeroInfo, requestIcon, receiverIcon, FindAnyObjectByType<GameSceneLoaderTrigger>().RequesterHeroStats, FindAnyObjectByType<GameSceneLoaderTrigger>().RecieverHeroHeroStats);
        }

        public void ExitFromTradePanel()
        {
            InTrade = false;
            if (_lastPickSlot != null)
            {
                _lastPickSlot.GetComponent<Image>().color = Color.white;
            }
            _tradeModel.SumbitTrade();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(TryPickSlot(out TradeCreatureSlot tradeCreatureSlot))
                {
                    if(_lastPickSlot != null)
                    {
                        _lastPickSlot.GetComponent<Image>().color = Color.white;
                    }
                    _tradeModel.PickTradeCreatureSlot(tradeCreatureSlot);
                    _lastPickSlot = tradeCreatureSlot;
                    tradeCreatureSlot.GetComponent<Image>().color = Color.red;
                }
            }
        }

        private bool TryPickSlot(out TradeCreatureSlot tradeCreatureSlot)
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach (var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out tradeCreatureSlot))
                    {
                        if (_lastPickSlot == null)
                        {
                            int count = 0;
                            foreach (var army in _tradeModel.TradeReceiverArmy)
                            {
                                if(army != null)
                                {
                                    count++;
                                }
                                if (army == tradeCreatureSlot.ArmySlotInfo && count <= 1)
                                {
                                    return false;
                                }
                            }
                            count = 0;
                            foreach (var army in _tradeModel.TradeRequesterArmy)
                            {
                                if (army != null)
                                {
                                    count++;
                                }
                                if (army == tradeCreatureSlot.ArmySlotInfo && count <= 1)
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            tradeCreatureSlot = null;
            return false;
        }

    }
}