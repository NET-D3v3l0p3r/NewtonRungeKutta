using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Mathematics
{
    public class Euler : IApproximation
    {

        public Euler(Vector3 _0, float delta)
        {
            Output = _0;
            Delta = delta;
        }

        public override Vector3 Run(Vector3 y)
        {
            Output += Delta * f(Time, y);
            Time += Delta;

            return Output;
        }
    }
}
