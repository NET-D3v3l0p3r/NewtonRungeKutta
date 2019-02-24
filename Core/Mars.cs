using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Mars : CelestialBody
    {
        public Mars()
        {
            NumericEccentricity = 0.0935f;
            TurnAroundTime = 59355072.0f;
            PlanetMass = 6.39E23;
            Weights = new Vector2(0.99f, 0.01f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.Orange;


        }
    }
}