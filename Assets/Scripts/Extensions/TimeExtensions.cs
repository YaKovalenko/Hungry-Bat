using UnityEngine;

namespace Extensions
{
    public static class TimeExtensions
    {
        public static string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);

            int seconds = Mathf.FloorToInt(time % 60);

            float fraction = time % 1;
            int hundredths = Mathf.FloorToInt(fraction * 100);

            return $"{minutes:00}:{seconds:00}.{hundredths:00}";
        }
    }
}