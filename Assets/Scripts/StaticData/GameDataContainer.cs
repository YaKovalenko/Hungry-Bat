using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "HB/StaticData/" + nameof(GameDataContainer), fileName = nameof(GameDataContainer))]
    public class GameDataContainer : StaticDataContainer
    {
        [SerializeField] 
        private int _amountOfTreatsRequired;

        [SerializeField] 
        private int _roundTime;
        
        public int AmountOfTreatsRequired => _amountOfTreatsRequired;
        public int RoundTime => _roundTime;
    }
}