using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.IceFeatures
{
    public sealed class IceBaseOptionsFeature : IIceBaseOptionsFeature
    {
        public IceBaseOptions Value { get; }

        public IceBaseOptionsFeature(IceBaseOptions value)
        {
            Value = value;
        }
    }
}
