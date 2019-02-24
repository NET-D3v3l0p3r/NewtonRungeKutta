using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Lines;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class CelestialBody
    {
        public static readonly double ASTRONOMICUNIT = 149597870700;
        public static readonly double KEPLERCONSTANTSUN = 2.97f * 0.0000000000000000001f; // 2.97*10^(-19) s^2/m^3
        public static readonly double GRAVITATIONCONSTANT = 6.67408e-11;
        public static readonly double SUN_G = 1.989E30 * GRAVITATIONCONSTANT;
        public static readonly double Exponent = 3;


        public float Size { get; set; }
        public double PlanetMass { get; set; }

        public float TurnAroundTime { get; set; }

        public float LinearEccentricity { get; private set; }
        public float NumericEccentricity { get; set; }

        public float Periphel { get; set; }
        public float Aphel { get; set; }
        public float MajorAxisA { get; set; }
        public float MinorAxisB { get; set; }
        public float Velocity0 { get; set; }

        public float Diameter { get; set; }

        public Vector2 Weights { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public Color Color;
        public Texture2D Texture;
 
        public static float Delta = 60 * 60 * 24 * 7 * 0.1f;
        public IApproximation VelocityApproximation { get; set; }
        public IApproximation PositionApproximation { get; set; }

        public static Model Model { get; set; }
        public static List<CelestialBody> InfluencingBodies = new List<CelestialBody>();


        // PATHS
        private static List<int> _indices = new List<int>();
        private static List<VertexPositionColor> _vertices = new List<VertexPositionColor>();

        private static VertexBuffer _vertexBuffer;
        private static IndexBuffer _indexBuffer;

        private static BasicEffect _bEffect;
        private static int _index;

        private static int _amount;

        public void Initialize()
        {
            MajorAxisA = (float)ToAstronomicUnit(Math.Pow((Math.Pow(TurnAroundTime, 2) / KEPLERCONSTANTSUN), 1.0 / 3));
            LinearEccentricity = NumericEccentricity * MajorAxisA;
            MinorAxisB = (float)Math.Sqrt(Math.Pow(MajorAxisA, 2) - Math.Pow(LinearEccentricity, 2));
            Periphel = MajorAxisA - LinearEccentricity;
            Aphel = MajorAxisA + LinearEccentricity;
            Velocity0 = (float)Math.Sqrt(SUN_G * (2 / ToOriginDistance(MajorAxisA * 1) - (1 / ToOriginDistance(MajorAxisA * 1))));

            Position = new Vector3((float)ToOriginDistance(MajorAxisA * 1), 0, 0);
            Velocity = new Vector3(Velocity0 * (1.0f - Weights.X), Velocity0 * Weights.Y, Velocity0 * Weights.X);
 
        }

        public Vector3 a(float x, Vector3 position)
        {
            Vector3 acceleration = new Vector3();
            for (int i = 0; i < InfluencingBodies.Count; i++)
            {
                if (InfluencingBodies[i].PlanetMass == PlanetMass)
                    continue;
                float distance = (position - InfluencingBodies[i].Position).Length();
                acceleration += new Vector3((float)(-GRAVITATIONCONSTANT * InfluencingBodies[i].PlanetMass * (position.X / (distance * distance * distance))),
                                            (float)(-GRAVITATIONCONSTANT * InfluencingBodies[i].PlanetMass * (position.Y / (distance * distance * distance))),
                                            (float)(-GRAVITATIONCONSTANT * InfluencingBodies[i].PlanetMass * (position.Z / (distance * distance * distance))));
            }

            return acceleration;
        }

        public Vector3 v(float x, Vector3 position)
        {
            return VelocityApproximation.Run(position);
        }

        public void Update(GameTime gTime)
        {
            if (PlanetMass == 1.989E30)
                return;
            Position = PositionApproximation.Run(Position);
        }

         
        public void PreSimulate()
        {
            for (double x = 0; x <= TurnAroundTime + Delta; x += Delta)
            {

                _vertices.Add(new VertexPositionColor(Position / 1000000000.0f, Color));
                _indices.Add(_index);
                _indices.Add(++_index);

                Position = PositionApproximation.Run(Position);

            }

            _indices.RemoveAt(_indices.Count - 1);
            _indices.RemoveAt(_indices.Count - 1);

            Initialize();

            VelocityApproximation = new RK4(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;
        }

        public static double ToAstronomicUnit(double value)
        {
            return value / ASTRONOMICUNIT;
        }

        public static double ToOriginDistance(double value)
        {
            return value * ASTRONOMICUNIT;
        }

        public static void InitializePaths()
        {
            _bEffect = new BasicEffect(Globals.Device);
            _indexBuffer = new IndexBuffer(Globals.Device, IndexElementSize.ThirtyTwoBits, _indices.Count, BufferUsage.None);
            _vertexBuffer = new VertexBuffer(Globals.Device, typeof(VertexPositionColor), _vertices.Count, BufferUsage.None);

            _indexBuffer.SetData<int>(_indices.ToArray());
            _vertexBuffer.SetData<VertexPositionColor>(_vertices.ToArray());

            _amount = _indices.Count;

            _indices.Clear();
            _vertices.Clear();
        }
        
        public static void RenderPaths()
        {
            Globals.Device.RasterizerState = RasterizerState.CullNone;
            Globals.Device.Indices = _indexBuffer;
            Globals.Device.SetVertexBuffer(_vertexBuffer);

            _bEffect.View = Camera.ViewMatrix;
            _bEffect.Projection = Camera.ProjectionsMatrix;
            _bEffect.World = Matrix.CreateTranslation(-Camera.CameraPosition);
            _bEffect.VertexColorEnabled = true;
            _bEffect.CurrentTechnique.Passes[0].Apply();

            Globals.Device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, _amount / 2);
 

        }

        public void Render()
        {
            Globals.Device.BlendState = BlendState.Opaque;
            Globals.Device.DepthStencilState = DepthStencilState.Default;
            Globals.Device.RasterizerState = RasterizerState.CullNone;
            Globals.Device.SamplerStates[0] = SamplerState.LinearWrap;

            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = Camera.ViewMatrix;
                    effect.Projection = Camera.ProjectionsMatrix;
                    effect.World = Matrix.CreateScale(Diameter / 100000000 ) * Matrix.CreateTranslation(-Camera.CameraPosition + (Position / 1000000000.0f));
                    effect.TextureEnabled = false;
                    if (Texture != null)
                    {
                        effect.Texture = Texture;
                        effect.TextureEnabled = true;
                    }
                    effect.CurrentTechnique.Passes[0].Apply();
                }
                mesh.Draw();
            }
        }
        


    }
}
