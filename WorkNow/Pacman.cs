using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using static SFML.Window.Keyboard.Key;

namespace Pacman
{
    public class Pacman : Actor
    {
        private IntRect[] rights ={
            new IntRect(0, 0, 18, 18),
            new IntRect(18, 0, 18, 18)
    };
        private IntRect[] ups ={
            new IntRect(0, 18, 18, 18),
            new IntRect(18, 18, 18, 18)
        };
        private IntRect[] lefts ={
            new IntRect(0, 36, 18, 18),
            new IntRect(18, 36, 18, 18)
        };
        private IntRect[] downs ={
            new IntRect(0, 54, 18, 18),
            new IntRect(18, 54, 18, 18)
        };
        private IntRect[] currentSprites = {    // we set it to rights for now
            new IntRect(0, 0, 18, 18),
            new IntRect(18, 0, 18, 18)
        };
        public override void Create(Scene scene)
        {
            speed = 100.0f;
            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 18, 18);
            scene.Events.LoseHealth += OnLoseHealth;
        }
        private void OnLoseHealth(Scene scene, int amount)
        {
            Position = originalPosition;
        }
        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.Events.LoseHealth -= OnLoseHealth;
        }
        protected override int PickDirection(Scene scene)
        {
            int dir = direction;

            if (Keyboard.IsKeyPressed(Right))
            {
                dir = 0; moving = true;
                currentSprites = rights;
            }
            else if (Keyboard.IsKeyPressed(Up))
            {
                dir = 1; moving = true;
                currentSprites = ups;
            }
            else if (Keyboard.IsKeyPressed(Left))
            {
                dir = 2; moving = true;
                currentSprites = lefts;
            }
            else if (Keyboard.IsKeyPressed(Down))
            {
                dir = 3; moving = true;
                currentSprites = downs;
            }
            if (IsFree(scene, dir)) return dir;
            if (!IsFree(scene, direction)) moving = false;
            return direction;
        }
        float animationTimer = 1.0f;
        Clock pacClock = new Clock();
        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            if (moving)
            {

                animationTimer -= pacClock.Restart().AsSeconds();
            }
            if (animationTimer <= 0.5f)
            {
                sprite.TextureRect = currentSprites[0];

            }
            else
            {
                sprite.TextureRect = currentSprites[1];
            }
            if (animationTimer <= 0) animationTimer = 1.0f;
        }
        public override void Render(RenderTarget target)
        {

            base.Render(target);
        }
    }
}
