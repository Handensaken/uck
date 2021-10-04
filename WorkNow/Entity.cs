using System;
using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;

        // public bool Dead;

        public enum Flags
        {
            Preparing,
            Active,
            Dead
        }
        public Flags flag;

        public Entity(string textureName)
        {
            sprite = new Sprite();
            this.textureName = textureName;
            flag = Flags.Preparing;
        }
        public Vector2f Position
        {
            get
            {
                return sprite.Position;
            }
            set
            {
                sprite.Position = value;
            }
        }
        public FloatRect Bounds { get; }
        public virtual bool Solid { get; }
        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }
        public void Destroy(Scene scene)
        {
            //empty for now
        }
        protected virtual void CollideWith(Scene s, Entity other)
        {

        }
        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(Bounds))
            {
                CollideWith(scene, found);
            }
        }
    }
}
