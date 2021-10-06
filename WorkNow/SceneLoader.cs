using SFML.System;
using System.Collections.Generic;
using SFML.Graphics;
using System;
using System.Text;
using System.IO;

namespace Pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;

        private string currentScene = "", nextScene = "";

        public SceneLoader()
        {
            loaders = new Dictionary<char, Func<Entity>>
            {
                {'#', () => new Wall()},
                {'g', () => new Ghost()},
                {'p', () => new Pacman()},
                {'.', () => new Coin()},
                {'c', () => new Candy()}
            };
        }
        // Initialized dictionary

        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                return true;
            }

            created = null;
            return false;
        }

        public void HandleSceneLoader(Scene scene)
        {
            if (nextScene == "") return;
            scene.ClearScene();
            // TODO: Load scene file
            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loadingscene'{file}'");
            int y = 0;
            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                int x = 0;
                foreach (char character in line)
                {
                    //loaders[character](); I should be able to spawn everything with something like this but I don't have time to figure it out
                    if (Create(character, out Entity created))
                    {

                        switch (character)
                        {
                            case '#': scene.Spawn(new Wall() {Position = new Vector2f(x * 18, y * 18)});break;
                            case 'p': scene.Spawn(new Pacman(){Position = new Vector2f(x * 18,y * 18)});break; 
                            case 'g': scene.Spawn(new Ghost(){Position = new Vector2f(x * 18,y * 18)}); break;
                            case '.': scene.Spawn(new Coin(){Position = new Vector2f(x * 18,y * 18)}); break;
                            case 'c': scene.Spawn(new Candy(){Position = new Vector2f(x * 18,y * 18)}); break;
                        }
                    }
                    x++;
                }
                y++;
            }

            if (scene.FindByType<GUI>(out _));
            else scene.Spawn(new GUI());
            
            currentScene = nextScene;
            nextScene = "";
        } 
        
        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;
    }
}


