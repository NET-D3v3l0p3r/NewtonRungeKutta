using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CelestialMechanicSimulatorV2MG;

namespace Lines
{
    public class Camera
    {
        public static bool CameraMoving;

        private static float oldMouseX, oldMouseY;


        public static Matrix ProjectionsMatrix { get; private set; }
        public static Matrix ViewMatrix { get; private set; }

        public static Vector3 CameraPosition { get; set; }
        public static Vector3 Direction { get; private set; }
        public static BoundingFrustum ViewFrustum { get; private set; }

        public static float Yaw { get; private set; }
        public static float Pitch { get; private set; }

        public static float MouseSensity { get; set; }
        public static float Velocity { get; set; }

        public static Vector3 REFERENCEVECTOR = new Vector3(0, 0, -1);

        public Camera(float mouse_sensity, float velocity)
        {
            MouseSensity = mouse_sensity;
            Velocity = velocity;
            ProjectionsMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Globals.Device.Viewport.AspectRatio, 0.01f, 15000.0f);
        }

        public static void Update()
        {

            float dX = Mouse.GetState().X - oldMouseX;
            float dY = Mouse.GetState().Y - oldMouseY;

            Pitch += -MouseSensity * dY;
            Yaw += -MouseSensity * dX;

            Pitch = MathHelper.Clamp(Pitch, -1.5f, 1.5f);

            UpdateMatrices();
            ResetCursor();

            ViewFrustum = new BoundingFrustum(ViewMatrix * ProjectionsMatrix);
        }


        public static void Move(Vector3 to)
        {
            Vector3 velocity = Vector3.Transform(to, Matrix.CreateRotationX(Pitch) * Matrix.CreateRotationY(Yaw));
            velocity *= Velocity;
            CameraPosition -= velocity;
        }



        private static void ResetCursor()
        {
            Mouse.SetPosition(Globals.Device.Viewport.Width / 2, Globals.Device.Viewport.Height / 2);
            oldMouseX = Globals.Device.Viewport.Width / 2.0f;
            oldMouseY = Globals.Device.Viewport.Height / 2.0f;
        }

        private static void UpdateMatrices()
        {
            Matrix rotation = Matrix.CreateRotationX(Pitch) * Matrix.CreateRotationY(Yaw);
            Vector3 transformedVec = Vector3.Transform(REFERENCEVECTOR, rotation);
            Direction = Vector3.Zero + transformedVec;
            
            ViewMatrix = Matrix.CreateLookAt(Vector3.Zero, Direction, Vector3.Up);
        }


    }
}