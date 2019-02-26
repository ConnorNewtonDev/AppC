using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace GameWorld
{
    /// <summary>
    /// Represents a moving character within the game world. It is rendered as a simple coloured polygon.
    /// </summary>
    public class Actor
    {
        public Point Position { get;  set; } // Current position of the actor.
        public float Rotation { get;  set; } // Current rotation of the actor.
        public SolidColorBrush Colour { get;  set; } // Colour to render the actor.
        public  Controller m_controller; // Controller used to update the position and rotation of the actor.
        public enum ControlState {Player, AI}; // State to determine control scheme.
        public ControlState controlState; // Current state of the actor.

        private float rotAccel = 0; // Incremented rotation accelerator.
        private float fwdAccel = 0; // Incremented forward accelerator.


        /// <summary>
        /// Construct the actor, initialising its position, rotation and colour.
        /// </summary>
        public Actor()
        {
            Position = new Point(400, 200);
            Rotation = 0.0f;
            Colour = Brushes.LightSeaGreen;
            m_controller = new Controller();
        }


        /// <summary>
        /// Construct the actor, spawn the players with values passed in from World.
        /// </summary>
        /// <param name="position", name="rotation", name="state"></param>
        public Actor(Point position, float rotation, ControlState state)
        {
            Position = position;
            Rotation = rotation;
            controlState = state;

            if(state == ControlState.Player)
                Colour = Brushes.LightSeaGreen;
            else
                Colour = Brushes.Red;

            m_controller = new Controller();
        }
        

        /// <summary>
        /// Handle the correct interaction by ENUM state
        /// </summary>
        /// <param name="deltaT"></param>
        public virtual void Update(float deltaT)
        {

            switch(controlState)
            {
                case ControlState.Player:
                    HandlePlayerMovement(deltaT);
                    break;
                case ControlState.AI:
                    HandleAIMovement(deltaT);
                    break;
            }
        }


        /// <summary>
        /// Update the position and rotation of the actor - advancing by deltaT.
        /// </summary>
        /// /// <param name="deltaT"></param>
        void HandlePlayerMovement(float deltaT)
        {
            // Apply current rotation to actors heading, with each iteration of the same state add acceleration.
            switch (m_controller.IsRotating())
            {
                case Controller.Rotating.Clockwise:
                    Rotation += deltaT * (2.5f + rotAccel);
                    rotAccel += 0.25f;
                    break;
                case Controller.Rotating.AntiClockwise:
                    Rotation -= deltaT * (2.5f + rotAccel);
                    rotAccel += 0.25f;
                    break;
                case Controller.Rotating.Stationary:
                    rotAccel = 0;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            // Update the position of the actor taking into account its current heading, with each iteration of the same state add acceleration.
            var movement = new Point();
            switch (m_controller.IsMoving())
            {
                case Controller.Moving.Forward:
                    movement.Y = deltaT * (-100 - fwdAccel);
                    fwdAccel += 0.25f;
                    break;
                case Controller.Moving.Backward:
                    movement.Y = deltaT * (100 + fwdAccel);
                    fwdAccel += 0.25f;
                    break;
                case Controller.Moving.Stationary:
                    fwdAccel = 0;
                    break;
                default:
                    Debug.Assert(false);
                    break;

            }
            var rotatedPoint = new Point(
                movement.X * Math.Cos(Rotation) - movement.Y * Math.Sin(Rotation),
                movement.Y * Math.Cos(Rotation) + movement.X * Math.Sin(Rotation)
            );
            var point = Position;
            point.X += rotatedPoint.X;
            point.Y += rotatedPoint.Y;
            Position = point;
        }


        /// <summary>
        /// Update the position and rotation of the actor using the position to handle the behaviour - advancing by deltaT.
        /// </summary>
        /// /// <param name="deltaT"></param>
        void HandleAIMovement(float deltaT)
        {
            // Apply current rotation to actors heading.
            switch (m_controller.AIRotate(Position))
            {
                case Controller.Rotating.Clockwise:
                    Rotation += deltaT * (1.0f + rotAccel);
                    break;
                case Controller.Rotating.AntiClockwise:
                    Rotation -= deltaT * (1.0f + rotAccel);
                    break;
                case Controller.Rotating.Stationary:
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            // Update the position of the actor taking into account its current heading.
            var movement = new Point();
            switch (m_controller.AIMove(Position))
            {
                case Controller.Moving.Forward:
                    movement.Y = deltaT * -100;
                    break;
                case Controller.Moving.Backward:
                    movement.Y = deltaT * 100;
                    break;
                case Controller.Moving.Stationary:
                    break;
                default:
                    Debug.Assert(false);
                    break;

            }

            var rotatedPoint = new Point(
                movement.X * Math.Cos(Rotation) - movement.Y * Math.Sin(Rotation),
                movement.Y * Math.Cos(Rotation) + movement.X * Math.Sin(Rotation)
            );
            var point = Position;
            point.X += rotatedPoint.X;
            point.Y += rotatedPoint.Y;
            Position = point;

        }


        /// <summary>
        /// Add representation of the actor into the renderables collection.
        /// </summary>
        /// <param name="renderables"></param>
        public void Render(UIElementCollection renderables)
        {
            renderables.Add(RenderUtils.CreateTriangle(Position, Rotation, Colour));
        }
    }
}