using System;
using SFML.Graphics;
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
        }
    }
}
