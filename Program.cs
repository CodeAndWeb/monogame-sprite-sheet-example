using System;
using TexturePackerMonoGameDemoCommon;

namespace TexturePacker_MonoGame_Demo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new DemoGame())
                game.Run();
        }
    }
}
