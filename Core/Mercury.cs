using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Mercury : CelestialBody
    {
        public Mercury()
        {
            NumericEccentricity = 0.20563069f;
            TurnAroundTime = 7600521.6f;
            PlanetMass = 3.285E23;
            Weights = new Vector2(0.99f, 0.06f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.Gray;


        }
    }
}
