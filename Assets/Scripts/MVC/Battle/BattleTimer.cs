using Assets.Scripts.MVC.Game;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class BattleTimer : MonoBehaviour
    {
        public bool IsBattleVSPlayer { private set; get; }

        private TMP_Text _timerText;

        private Coroutine _timerCoroutine;

        private int _timerTime;
        public int LastStartedTimerValue { get; private set; }
        private bool _isNegativeTimerMode;

        public int TimerTime
        {
            get => _timerTime;
            set
            {
                if (IsBattleVSPlayer)
                    return;
                
                if (value < 0)
                    _timerTime = 0;
                _timerTime = value;
                DisplayTime(_timerTime);
            }
        }

        public void SetIsBattleVSPlayer(bool isBattleVSPlayer)
        {
            IsBattleVSPlayer = isBattleVSPlayer;
        }

        public void SetTimerText(TMP_Text timerText)
        {
            _timerText = timerText;
        }

        public void DisplayTime(int time)
        {
            if (_timerText == null)
                return;
            if (IsBattleVSPlayer)
                return;
                
            string minutes = (time / 60).ToString();
            if (minutes.Length == 1)
            {
                minutes = $"0{minutes}";
            }

            string seconds = (time % 60).ToString();
            if (seconds.Length == 1)
            {
                seconds = $"0{seconds}";
            }

            _timerText.text = $"{minutes} : {seconds}";
        }

        public void SetText(string text)
        {
            if (!IsBattleVSPlayer)
                return;
            if (_timerText != null)
                _timerText.text = text;
        }

        public void StartTimer(int time, bool isNegativeTimerMode = false)
        {
            _isNegativeTimerMode = isNegativeTimerMode;
            LastStartedTimerValue = time;
            TimerTime = time;
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void PauseTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
        }

        public void ContinueTimer()
        {
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void StopTimer()
        {
            TimerTime = 0;
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
            SetText("Time is over");
        }

        public void ResetTimer(int time)
        {
            StopTimer();
            TimerTime = time;
            _timerCoroutine = StartCoroutine(Timer());
        }

        public virtual IEnumerator Timer()
        {
            while (true)
            {
                if (!IsBattleVSPlayer)
                {
                    if (TimerTime <= 0 && !_isNegativeTimerMode)
                    {
                        SetText("Time is over");
                        break;
                    }
                    TimerTime--;
                }
                yield return new WaitForSeconds(1);
            }
        }

    }
}