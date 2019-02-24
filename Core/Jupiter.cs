using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Jupiter : CelestialBody
    {
        public Jupiter()
        {
            NumericEccentricity = 0.0484f;
            TurnAroundTime = 374112000.0f;
            PlanetMass = 1.898E27;
            Weights = new Vector2(0.99f, 0.09f);
            Diameter = 100000000;
            Initialize();


            VelocityApproximation = new RK4(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.Brown;


        }
    }
}