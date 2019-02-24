using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Neptun : CelestialBody
    {
        public Neptun()
        {
            NumericEccentricity = 0.0113f;
            TurnAroundTime = 5196817440.0f;
            PlanetMass = 1.024E26;
            Weights = new Vector2(0.95f, 0.2f);
            Diameter = 50000000;
            Initialize();


            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;

            Color = Color.CornflowerBlue;


        }
    }
}