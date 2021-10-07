using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
using System;

namespace Pacman
{
    public class Ghost : Actor
    {
        private float frozenTimer;
        private float timePassed;

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            direction = -1;
            speed = 100.0f;
            moving = true;
            scene.Events.CandyEaten += OnCandyEaten;
        }
        private void OnCandyEaten(Scene scene, int value)
        {
            frozenTimer = 5f;
        }
        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if ((i + 2) % 4 == direction) continue;
                if (IsFree(scene, i)) validMoves.Add(i);

            }
            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];

        }
        protected override void CollideWith(Scene scene, Entity e)
        {
            if (e is Pacman)
            {
                if (frozenTimer <= 0)
                {
                    scene.Events.PublishLoseHealth(1);

                }
                Position = originalPosition;
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
            timePassed += deltaTime;
        }

        public override void Render(RenderTarget target)
        {
            if (frozenTimer > 0.0f)
            {
                if (timePassed >= 0.2)
                {
                    if (sprite.TextureRect.Left == 36)
                    {
                        sprite.TextureRect = new IntRect(54, 18, 18, 18);
                    }
                    else
                    {
                        sprite.TextureRect = new IntRect(36, 18, 18, 18);
                    }

                    timePassed = 0;
                }
            }
            else
            {
                if (timePassed >= 0.2)
                {
                    if (sprite.TextureRect.Left == 36)
                    {
                        sprite.TextureRect = new IntRect(54, 0, 18, 18);
                    }
                    else
                    {
                        sprite.TextureRect = new IntRect(36, 0, 18, 18);
                    }

                    timePassed = 0;
                }
            }
            base.Render(target);
        }




    }
}