using System;
using CS_lab.Models;

namespace CS_lab.GameStateWriter
{
    public interface IGameStateWriter
    {
        public void WriteGameState(World world);
        
    }
}