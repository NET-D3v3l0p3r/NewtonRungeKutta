using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace CelestialMechanicSimulatorV2MG.Mathematics
{
    public class RK5 : IApproximation
    {

        public RK5(Vector3 _0, float delta)
        {
            Output = _0;
            Delta = delta;
        }

        public override Vector3 Run(Vector3 y)
        {
            Vector3 k1 = Delta * f(Time, y);
            Vector3 k2 = Delta * f(Time + ((3.0f * Delta) / 5.0f), y + ((1 * 3.0f) / 5.0f) * k1);
            Vector3 k3 = Delta * f(Time + ((2.0f * Delta) / 5.0f), y + ((4.0f * 1) / 15.0f) * k1 + ((2.0f * 1) / 15.0f) * k2);
            Vector3 k4 = Delta * f(Time + (Delta / 5.0f), y + ((3.0f * 1) / 20.0f) * k1 + (1 / 20.0f) * k3);
            Vector3 k5 = Delta * f(Time + ((4.0f * Delta) / 5.0f), y + ((-1 / 5.0f)) * k1 + ((-2.0f * 1) / 5.0f) * k2 + ((7.0f * 1) / 5.0f) * k3);
            Vector3 k6 = Delta * f(Time + Delta, y + ((59.0f * 1) / 84.0f) * k1 + ((40.0f * 1) / 21.0f) * k2 + ((-165.0f * 1) / 28.0f) * k3 + ((20.0f * 1) / 7.0f) * k4 + ((10.0f * 1) / 7.0f) * k5);

            Output += (k1 / 12.0f) + ((25.0f * k3) / 72.0f) + ((25.0f * k4) / 144.0f) + ((25.0f * k5) / 72.0f) + ((7.0f * k6) / 144.0f);

            Time += Delta;

            return Output;
        }
    }
}
