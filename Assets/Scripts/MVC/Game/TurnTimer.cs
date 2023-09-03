using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.MVC.Game
{
    public class TurnTimer : MonoBehaviour
    {
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
                if (value < 0)
                    _timerTime = 0;
                _timerTime = value;
                DisplayTime(_timerTime);
            }
        }

        public void SetTimerText(TMP_Text timerText)
        {
            _timerText = timerText;
        }

        public void DisplayTime(int time)
        {
            if (_timerText == null)
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

        public void StartTimer(int time, bool isNegativeTimerMode = false)
        {
            _isNegativeTimerMode = isNegativeTimerMode;
            LastStartedTimerValue = time;
            TimerTime = time;
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
        }

        public void ResetTimer(int time)
        {
            StopTimer();
            TimerTime = time;
            _timerCoroutine = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                if (TimerTime <= 0 && !_isNegativeTimerMode)
                {

                    break;
                }
                TimerTime--;
                yield return new WaitForSeconds(1);
            }
        }

    }
}