using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Pluto : CelestialBody
    {
        public Pluto()
        {
            NumericEccentricity = 0.2488f;
            TurnAroundTime = 7831375200.0f;
            PlanetMass = 1.27E22;
            Weights = new Vector2(0.9f, 0.4f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.SlateGray;


        }
    }
}
