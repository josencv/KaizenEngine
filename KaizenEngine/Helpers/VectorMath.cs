using System;
using Microsoft.Xna.Framework;

namespace KaizenEngine.Helpers
{
    enum Direction { Right, Up, Left, Down }
    public static class VectorMath
    {

        private static Vector2 reference = new Vector2(1, 0);
        private static Vector3 reference3D = new Vector3(1, 0, 0);
        private static Vector3 normal = new Vector3(0, 0, 1);

        public static float Angle(Vector2 vector)
        {
            // If one of the vectors is (0,0) no angle should be returned
            if (reference.Length() == 0 || vector.Length() == 0)
                return float.NaN;

            float angle = MathHelper.ToDegrees((float)Math.Atan2(vector.Y, vector.X));

            if (angle < 0)
                angle = 360 + angle;

            return angle;
        }

        public static double AngleRounded(Vector2 vector)
        {
            double angle = VectorMath.Angle(vector);

            return Math.Round(angle);
        }

        /// <summary>
        /// Gets the direction int representation of a direction based on a vector. Mirrors the blending tree direction
        /// </summary>
        public static double GetDirecionZoneFromVector(Vector2 vector)
        {
            double angle = VectorMath.Angle(vector);

            if (Double.IsNaN(angle))
            {
                return Double.NaN;
            }

            double direction;

            if (45 <= angle && angle <= 135)
            {
                direction = 1; // Up direction
            }
            else if (135 < angle && angle < 225)
            {
                direction = 2; // Left direction
            }
            else if (225 <= angle && angle <= 270)
            {
                direction = 3; // Down direction
            }
            else {
                direction = 0;  // Right direction
            }

            return direction;
        }

        public static Vector2 GetVectorZoneFromVector(Vector2 vector)
        {
            int direction = (int)VectorMath.GetDirecionZoneFromVector(vector);
            Vector2 result = Vector2.Zero;
            switch (direction)
            {
                case 0:
                    result = new Vector2(1, 0);
                    break;
                case 1:
                    result = new Vector2(0, 1);
                    break;
                case 2:
                    result = new Vector2(-1, 0);
                    break;
                case 3:
                    result = new Vector2(0, -1);
                    break;
            }
            return result;
        }


        public static Vector2 Perpendicular(Vector2 vector)
        {
            Vector3 vector3D = new Vector3(vector.X, vector.Y, 0);
            Vector3 perpendicular = Vector3.Cross(normal, vector3D);
            return new Vector2(perpendicular.X, perpendicular.Y);
        }

        public static Vector2 PerpendicularPositive(Vector2 vector)
        {
            Vector3 vector3D = new Vector3(vector.X, vector.Y, 0);
            Vector3 perpendicular = Vector3.Cross(normal, vector3D);
            perpendicular.Y = Math.Abs(perpendicular.Y);
            return new Vector2(perpendicular.X, perpendicular.Y);
        }

        /// <summary>
        /// Cosine values obtained from degrees
        /// </summary>
        /// <param name="degrees">Angle in degrees</param>
        /// <returns></returns>
        public static float Cos(float degrees)
        {
            return (float)Math.Cos(degrees * Math.PI / 180);
        }

    }

}
