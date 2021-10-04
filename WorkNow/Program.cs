using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(828, 900), "Packman"))
            {
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Loader.Load("maze");
                window.SetView(new View(new FloatRect(27, 0, 414, 450)));
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    //Updates
                    scene.UpdateAll(deltaTime);
                    window.Clear(new Color(223, 246, 245));
                    //Renders
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}
