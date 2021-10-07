using SFML.System;
using System.Collections.Generic;
using SFML.Graphics;
using System;
using System.Linq;

namespace Pacman
{
    //public delegate void ValueChangedEvent(Scene scene, int value);
    public class Scene
    {

        private readonly List<Entity> entities = new List<Entity>();
        public readonly SceneLoader Loader = new SceneLoader();
        public readonly AssetManager Assets = new AssetManager();
        public readonly EventManager Events = new EventManager();
        /*    public event ValueChangedEvent GainScore;
            public event ValueChangedEvent LoseHealth;
            public event ValueChangedEvent CandyEaten;*/
        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public bool FindByType<T>(out T found) where T : Entity
        {
            // TODO: Loop through list for instances of T
            foreach (Entity entity in entities)
            {
                if (entity is T typed)
                {
                    found = typed;
                    return true;
                }
            }
            found = default(T);
            return false;
        }
        public void ClearScene()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (!entity.DontDestroyOnLoad)
                {
                    entities.RemoveAt(i);
                    entity.Destroy(this);
                }
            }
        }

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = entities.Count - 1;
            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }

        /*private int scoreGained;
        public void PublishGainScore(int amount) => scoreGained += amount;

        private int healthLost;
        public void PublishLoseHealth(int amount) => healthLost += amount;

        private int CandyInt;
        public void PublishCandyEaten(int amount) => CandyInt += amount;*/
        public void UpdateAll(float deltaTime)
        {
            Loader.HandleSceneLoader(this);

            if (deltaTime > 0.1f) deltaTime = 0.1f;
            foreach (Entity entity in entities)
            {
                entity.Update(this, deltaTime);
            }
            Events.Update(this);
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (entities[i].Dead) entities.RemoveAt(i);
            }

        }

        public void RenderAll(RenderWindow window)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(window);
            }
        }
    }
}