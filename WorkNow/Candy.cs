using SFML.Graphics;

namespace Pacman
{
    public class Candy : Entity
    {
        //ඞ
        public Candy() : base("pacman")
        {
        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(54, 36, 18, 18);
        }

        protected override void CollideWith(Scene scene, Entity e)
        {
            if (e is Pacman)
            {
                scene.PublishCandyEaten(100);
                this.Dead = true;
            }
        }
    }
}