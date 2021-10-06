using System;
using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class GUI : Entity
    {
        private Text scoreText;
        private int maxHealth;
        private int currentHealth;
        private int currentScore;
        public GUI() : base("pacman")
        {
            maxHealth = 4;
            currentHealth = 4;
            currentScore = 0;
            scoreText = new Text();
        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(72, 36, 18, 18);
            scoreText.Font = scene.Assets.LoadFont("pixel-font");

            currentHealth = maxHealth;
            scene.LoseHealth += OnLoseHealth;
            scene.GainScore += OnGainScore;
        }
        private void OnLoseHealth(Scene scene, int amount)
        {
            currentHealth -= amount;
            System.Console.WriteLine(currentHealth);
            if (currentHealth <= 0)
            {
                DontDestroyOnLoad= false;
                scene.Loader.Reload();
            }
        }
        private void OnGainScore(Scene scene, int amount)
        {
            currentScore += amount;
            if (!scene.FindByType<Coin>(out _))
            {
                DontDestroyOnLoad = true;
                scene.Loader.Reload();
            }
        }
        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.LoseHealth -= OnLoseHealth;
            scene.GainScore -= OnGainScore;
        }
        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(36, 396);
            for (int i = 0; i < maxHealth; i++)
            {
                sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18) :
                 new IntRect(72, 0, 18, 18);
                base.Render(target);
                sprite.Position += new Vector2f(18, 0);
            }
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
            target.Draw(scoreText);
        }

    }
}