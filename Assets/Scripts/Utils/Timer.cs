using System;
using System.Collections.Generic;
using System.Linq;

namespace daifuDemo
{
    public interface ITimer
    {
        float AdjustTime();
        
        void Reset();
        
        void SetSpecialValue(string value);
        
        string GetSpecialValue();
    }
    
    public class Timer : ITimer
    {
        private DateTime _lastCallTime;
        private DateTime _currentTime;
        private string _specialValue;

        private void StartTime()
        {
            _currentTime = DateTime.Now;
            _lastCallTime = _currentTime;
        }

        public float AdjustTime()
        {
            if (_lastCallTime == default(DateTime))
            {
                StartTime();
            }

            _currentTime = DateTime.Now;
            TimeSpan timeSpan = _currentTime - _lastCallTime;
            float timeElapsed = (float)timeSpan.TotalSeconds;

            _lastCallTime = _currentTime;

            return timeElapsed;
        }

        public void Reset()
        {
            _lastCallTime = default(DateTime);
            _specialValue = null;
        }

        public void SetSpecialValue(string value)
        {
            _specialValue = value;
        }

        public string GetSpecialValue()
        {
            return _specialValue;
        }
    }
    
    public interface ITimerPool
    {
        Timer GetTimer(string value);
    }

    public class TimerPool : ITimerPool
    {
        private List<Timer> _pool;

        public TimerPool()
        {
            _pool = new List<Timer>();
        }

        public Timer GetTimer(string value)
        {
            var timer = _pool.FirstOrDefault(t => t.GetSpecialValue() == value);

            if (timer != null)
            {
                _pool.Remove(timer);
                return timer;
            }

            Timer newTimer = new Timer();
            newTimer.SetSpecialValue(value);
            return newTimer;
        }
    }
}