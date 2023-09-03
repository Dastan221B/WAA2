using Assets.Scripts.Client.DTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Client
{
    public class CoommonMessageHandler
    {
        private CommonData _commonData;

        public CoommonMessageHandler(CommonData commonData)
        {
            _commonData = commonData;
        }

        enum InputCommonHeaders
        {
            GET_CASTLE_LIST_RESULT = 0,
            GET_HERO_LIST_RESULT = 1,
            GET_BUILDING_LIST_RESULT = 2,
            GET_CREATURE_LIST_RESULT = 3
        }

        public void ProccessResponseFromServer(MessageInput message)
        {
            switch ((InputCommonHeaders)message.header)
            {
                case InputCommonHeaders.GET_CASTLE_LIST_RESULT:
                    List<DicCastleDTO> castleDTOs;
                    castleDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DicCastleDTO>>(message.body);
                    _commonData.SetCastlesDTO(castleDTOs);
                    break;
                case InputCommonHeaders.GET_HERO_LIST_RESULT:
                    List<DicHeroDTO> heroDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DicHeroDTO>>(message.body);
                    _commonData.SetHeroesDTO(heroDTOs);
                    break;
                case InputCommonHeaders.GET_BUILDING_LIST_RESULT:
                    List<DicBuildingDTO> dicBuildingDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DicBuildingDTO>>(message.body);
                    _commonData.SetBuildingsInfo(dicBuildingDTOs);
                    break;
                case InputCommonHeaders.GET_CREATURE_LIST_RESULT:
                    List<DicCreatureDTO> creatures;
                    creatures = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DicCreatureDTO>>(message.body);
                    _commonData.SetDicCreatureDTOList(creatures);
                    break;
                default:
                    break;
            }
        }
    }
}