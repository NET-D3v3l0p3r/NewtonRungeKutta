using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Earth : CelestialBody
    {
        public Earth()
        {
            NumericEccentricity = 0.0167f;
            TurnAroundTime = 31558118.4f;
            PlanetMass = 5.972E24;
            Weights = new Vector2(0.99f, 0.01f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.Blue;

           

        }
    }
}
