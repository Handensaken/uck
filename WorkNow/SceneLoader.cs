using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SFML.System;
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
                {'#', ()=> new Wall()},
                {'g', ()=> new Ghost()}
            };
        }
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
        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.ClearScene();
            //load scene file
            string file = $"assets/maze.txt";
            int row = 1;
            int column = 1;
            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                foreach (char c in line)
                {
                    if (Create(c, out Entity e))
                    {
                        scene.Spawn(e);
                        e.Position = new Vector2f(column * 18, row * 18);   //column should be the x axis and row the y. I think?
                    }
                    column++;
                }
                row++;
                column = 1;
                System.Console.WriteLine(row);
            }
            currentScene = nextScene;
            nextScene = "";
        }
        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;
    }
}
