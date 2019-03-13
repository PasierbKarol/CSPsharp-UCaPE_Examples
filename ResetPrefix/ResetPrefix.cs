using System;
using CSPlang;
using PlugAndPlay;

namespace ResetPrefix
{
    class ResetPrefix : IamCSProcess
    {
        private int prefixValue = 0;
        private ChannelOutput outChannel;
        private ChannelInput inChannel;
        private ChannelInput resetChannel;

        public ResetPrefix(int prefixValue, ChannelOutput outChannel, AltingChannelInput inChannel,
            ChannelInput resetChannel)
        {
            this.prefixValue = prefixValue;
            this.outChannel = outChannel;
            this.inChannel = inChannel;
            this.resetChannel = resetChannel;
        }

        public void run()
        {
            var alt = new Alternative(new Guard[] {resetChannel as Guard, inChannel as Guard});
            outChannel.write(prefixValue);
            while (true)
            {
                int index = alt.priSelect();
                if (index == 0)
                {
                    // resetChannel input
                    var resetValue = resetChannel.read();
                    inChannel.read();
                    outChannel.write(resetValue);
                }
                else
                {
                    //inChannel input 
                    outChannel.write(inChannel.read());
                }
            }
        }
    }
}