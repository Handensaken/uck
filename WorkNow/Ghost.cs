using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
namespace Pacman
{
    public class Ghost : Actor
    {
        public override void Create(Scene scene)
        {
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            sprite.Origin = new Vector2f(9, 9);
        }
        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if ((i + 2) % 4 == direction) continue;
                System.Console.WriteLine(IsFree(scene, i));
                if (IsFree(scene, i)) validMoves.Add(i);
            }
            return validMoves[new Random().Next(0, validMoves.Count)];
        }
    }
}
