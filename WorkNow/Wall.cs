using System;
using SFML.Graphics;
using SFML.System;
namespace Pacman
{
    public class Wall : Entity
    {
        public Wall() : base("pacman") { }

        public override bool Solid => true;
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(54, 54, 18, 18);
            sprite.Origin = new Vector2f(9, 9);
        }
        public override void Update(Scene scene, float deltaTime) { }
    }
}
