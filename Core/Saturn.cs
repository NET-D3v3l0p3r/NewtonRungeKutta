using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Saturn : CelestialBody
    {
        public Saturn()
        {
            NumericEccentricity = 0.05648f;
            TurnAroundTime = 928735200.0f;
            PlanetMass = 5.683E26;
            Weights = new Vector2(0.999f, 0.03f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK4(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.SandyBrown;


        }
    }
}
