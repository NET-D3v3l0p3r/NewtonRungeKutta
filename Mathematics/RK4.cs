using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMechanicSimulatorV2MG.Mathematics
{
    public class RK4 : IApproximation
    {

        public RK4(Vector3 _0, float delta)
        {
            Output = _0;
            Delta = delta;
        }

        public override Vector3 Run(Vector3 y)
        {
            Vector3 k1 = Delta * f(Time, y);
            Vector3 k2 = Delta * f(Time + .5f * Delta, y + .5f * k1);
            Vector3 k3 = Delta * f(Time + .5f * Delta, y + .5f * k2);
            Vector3 k4 = Delta * f(Time + Delta, y + k3);

            Output += 1.0f / 6.0f * (k1 + 2 * k2 + 2 * k3 + k4);

            Time += Delta;

            return Output;
        }
    }
}
