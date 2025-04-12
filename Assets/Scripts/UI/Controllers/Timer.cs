using System;
using System.Collections;
using UnityEngine;

namespace UI.Controllers
{
    public class Timer : MonoBehaviour
    {
        private float _duration;
        private float _remainingTime;
        private Coroutine _timerCoroutine;
        
        public event Action<float> OnTimerTick;
        public event Action OnTimerEnd;

        public void Init(float duration)
        {
            _duration = duration;
        }
        
        public void StartTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            _remainingTime = _duration;
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        public float GetTimeRemaining()
        {
            return _remainingTime;
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        private IEnumerator TimerCoroutine()
        {
            while (_remainingTime > 0)
            {
                yield return new WaitForSeconds(1f);
                _remainingTime--;
                OnTimerTick?.Invoke(_remainingTime);
            }

            OnTimerEnd?.Invoke();
        }
    }
}