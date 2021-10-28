using System;
using System.IO;
using CS_lab.Models;

namespace CS_lab.GameStateWriter
{
    public class GameStateFileWriter : IGameStateWriter, IDisposable
    {
        private readonly StreamWriter _logsWriter;
        
        public GameStateFileWriter(string fileName)
        {
            _logsWriter = new StreamWriter(fileName);
        }
        
        public void WriteGameState(World world)
        {
            _logsWriter.Write("Worms: [");
            for (var i = 0; i < world.Worms.Count; ++i)
            {
                _logsWriter.Write(
                    $"{world.Worms[i].Name}-{world.Worms[i].Health} ({world.Worms[i].Position.X},{world.Worms[i].Position.Y})");
                if (i != world.Worms.Count - 1)
                {
                    _logsWriter.Write(",");
                }
            }

            _logsWriter.Write("], Food: [");

            for (var i = 0; i < world.Foods.Count; ++i)
            {
                _logsWriter.Write($"({world.Foods[i].Position.X},{world.Foods[i].Position.Y})");
                if (i != world.Foods.Count - 1)
                {
                    _logsWriter.Write(",");
                }
            }

            _logsWriter.WriteLine("]");
        }

        public void Dispose()
        {
            _logsWriter.Dispose();
        }
    }
}