using CelestialMechanicSimulatorV2MG.Mathematics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Core
{
    public class Sun : CelestialBody
    {
        public Sun()
        {
            NumericEccentricity = 0;
            TurnAroundTime = 0;
            PlanetMass = CelestialBody.SUN_G / CelestialBody.GRAVITATIONCONSTANT;
            Weights = new Vector2(1, 0);
            Diameter = 500000000;
            Initialize();

 
            VelocityApproximation = new RK5(Velocity, CelestialBody.Delta);
            VelocityApproximation.f = a;

            PositionApproximation = new Euler(Position, CelestialBody.Delta);
            PositionApproximation.f = v;


        }
    }
}
