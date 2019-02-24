using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CelestialMechanicSimulatorV2MG.Mathematics
{
    public abstract class IApproximation
    {
        public delegate Vector3 f_delegate(float x, Vector3 y);

        public f_delegate f { get; set; }

        public float Delta { get; set; }
        public float Time { get; set; }

        public Vector3 Output { get; set; }

        public abstract Vector3 Run(Vector3 y);
    }
}
