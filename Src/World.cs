using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;


namespace GameWorld
{
    /// <summary>
    /// Main game world that contains a single actor.
    /// </summary>
    public class World
    {
        readonly Actor m_actor, m_actor2, m_actor3; // Actors within the game world.
        List<Actor> spawnedActors = new List<Actor>(); // List of spawned actors
        private Random random = new Random(); // Random Seed
        public double Width { get; set; } // Rendered width
        public double Height { get; set; } // Rendered height

        /// <summary>
        /// Construct the world and a single actor.
        /// </summary>
        public World()
        {
            SpawnActor(ref m_actor, Actor.ControlState.Player);
            SpawnActor(ref m_actor2, Actor.ControlState.AI);
            SpawnActor(ref m_actor3, Actor.ControlState.AI);

        }
        
        /// <summary>
        /// Update the world - advancing the simulation by deltaT.
        /// </summary>
        /// <param name="deltaT"></param>
        public void Update(float deltaT)
        {
            for (int i = 0; i < spawnedActors.Count; i++)
                spawnedActors[i].Update(deltaT);
        }

        /// <summary>
        /// Render the world.
        /// </summary>
        /// <param name="renderables"></param>
        public void Render(UIElementCollection renderables)
        {
            for (int i = 0; i < spawnedActors.Count; i++)
            {
                spawnedActors[i].Render(renderables);
            }
        }
        
        
        /// <summary>
        /// Spawn a player assigning a random Point & the control state
        /// </summary>
        /// <param name="actor"></param>
        private void SpawnActor(ref Actor actor, Actor.ControlState state)
        {
            float w = random.Next(50, 500);
            float h = random.Next(50, 300);

            float rot = random.Next(0, 359);

            if (state == Actor.ControlState.Player)
                actor = new Actor(new Point(w, h), rot, state);
            else
                actor = new Actor(new Point(w, h), rot, state);

            spawnedActors.Add(actor);

        }

    }
}