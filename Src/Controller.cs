using System.Windows.Input;
using System.Windows;

namespace GameWorld
{
    /// <summary>
    /// Input control for an actor. 
    /// </summary>
    public class Controller
    {

        // ----- ENUM ----- //

        public enum Rotating
        {
            Stationary,
            Clockwise,
            AntiClockwise
        }

        public enum Moving
        {
            Stationary,
            Forward,
            Backward
        }

        // ----- PLAYER ----- //

        /// <summary>
        /// Check if the actor is currently rotating, and the direction they are rotating.
        /// </summary>
        /// <returns name="Rotating"></returns>
        public Rotating IsRotating()
        {
            if (Keyboard.IsKeyDown(Key.A))
                return Rotating.AntiClockwise;
            else if (Keyboard.IsKeyDown(Key.D))
                return Rotating.Clockwise;
            else
                return Rotating.Stationary;
        }
       

        /// <summary>
        /// Check if the actor is currently moving, and the direction they are moving.
        /// </summary>
        /// <returns name="Moving"></returns>
        public Moving IsMoving()
        {
            if (Keyboard.IsKeyDown(Key.W))
                return Moving.Forward;
            else if (Keyboard.IsKeyDown(Key.S))
                return Moving.Backward;
            else
                return Moving.Stationary;
        }


        // ----- AI ----- //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns name="Rotating"></returns>
        public Rotating AIRotate(Point position)
        {
            if (position.X < 40 || position.X > 325)
                return Rotating.AntiClockwise;

            return Rotating.Stationary;
        }


        /// <summary>
        /// Always currently advancing
        /// </summary>
        /// <param name="position"></param>
        /// <returns name="Moving"></returns>
        public Moving AIMove(Point position)
        {
            return Moving.Forward;
        }



    }
}