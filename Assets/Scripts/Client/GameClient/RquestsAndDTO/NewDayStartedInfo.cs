using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Client.GameClient.RquestsAndDTO
{
    public enum DayStatus
    {
        REGULAR_DAY = 0,
        WEEL_START = 1,
        MONTH_START = 2
    }

    public class NewDayStartedInfo : SessionIdBasedResponse
    {
        public int daysCounter;
        public int weeksCounter;
        public int monthsCounter;
        public DayStatus dayStatus;
    }
}