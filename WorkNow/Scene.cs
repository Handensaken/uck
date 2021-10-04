using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System.Linq;
namespace Pacman
{
    public class Scene
    {
        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly AssetManager Assets;
        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new AssetManager();
        }
        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public void UpdateAll(float deltaTime)
        {
            Loader.HandleSceneLoad(this);
            HandleEntityChanges();
            foreach (Entity entity in entities.Where(e => e.flag == Entity.Flags.Active))
            {
                entity.Update(this, deltaTime);
            }
        }
        public void RenderAll(RenderTarget target)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(target);
            }
        }
        public bool FindByType<T>(out T found) where T : Entity
        {
            found = default(T);
            foreach (var entity in entities)
            {
                if (entity is T typed)
                {
                    found = typed;
                    return true;
                }
            }
            return false;
        }
        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.flag == Entity.Flags.Dead) continue;
                if (entity.Bounds.Intersects(bounds)) yield return entity;
            }
        }
        public void ClearScene()
        {
            foreach (Entity entity in entities)
            {
                entity.flag = Entity.Flags.Dead;
            }
        }
        private void HandleEntityChanges()  //I'm trying to avoid removing entities during each frame, Instead I flag them and handle all changes before starting the Update functions of each respective entity
        {
            foreach (Entity entity in entities.Where(e => e.flag == Entity.Flags.Preparing))
            {
                entity.flag = Entity.Flags.Active;
            }
            foreach (Entity entity in entities.Where(e => e.flag == Entity.Flags.Dead))
            {
                entities.Remove(entity);
                entity.Destroy(this);
            }

        }
    }
}
