using System;
using CSPlang;
using PlugAndPlay;

namespace Testing_GIntegrate
{
    class TestGIntegrate
    {
        static void Main(string[] args)
        {
            One2OneChannel N2I = Channel.one2one();
            One2OneChannel I2P = Channel.one2one();

            IamCSProcess[] testList = {new Numbers(outChannel: N2I.Out ()),
            new Integrate(inChannel: N2I.In (),
            outChannel: I2P.Out ()),
            new GPrint(inChannel: I2P.In (),
            heading: "Integrate")
                };

            new CSPParallel(testList).run();
        }
    }
}
