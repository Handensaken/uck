using System;

namespace Pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    public class EventManager
    {

        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent CandyEaten;
        private int scoreGained;
        public void PublishGainScore(int amount) => scoreGained += amount;

        private int healthLost;
        public void PublishLoseHealth(int amount) => healthLost += amount;

        private int CandyInt;
        public void PublishCandyEaten(int amount) => CandyInt += amount;
        public void Update(Scene scene)
        {
            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
            if (healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }
            if (CandyInt != 0)
            {
                CandyEaten?.Invoke(scene, CandyInt);
                CandyInt = 0;
            }
        }

    }
}
