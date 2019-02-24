using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Uranus : CelestialBody
    {
        public Uranus()
        {
            NumericEccentricity = 0.0472f;
            TurnAroundTime = 2649370896.0f;
            PlanetMass = 8.683E25;
            Weights = new Vector2(0.99f, 0.06f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.CadetBlue;


        }
    }
}