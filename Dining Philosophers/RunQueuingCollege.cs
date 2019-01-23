// copyright 2012-13 Jon Kerridge
// Let's Do It In Parallel

using System;
using CSPlang;
using CSPlang.Any2;

namespace Dining_Philosophers
{
    public class RunQueuingCollege
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many Philosophers?\t");
            int philosophers = Int32.Parse(Console.ReadLine());
            Any2OneChannel service = Channel.any2one();
            One2AnyChannel deliver = Channel.one2any();
            One2OneChannel supply = Channel.one2one();

            IamCSProcess[] philosopherList = new IamCSProcess[philosophers];
            for (int i = 0; i < philosopherList.Length; i++)
            {
                philosopherList.SetValue(new Philosopher(philosopherId: i,
                    service: service.Out(),
                    deliver:
                    deliver.In()), i);
            }


            IamCSProcess[] processList =
            {
                new QueuingServery(service: service.In(),
                    deliver: deliver.Out(),
                    supply: supply.In()),
                new Kitchen(supply: supply.Out())
            };

            IamCSProcess[] network = new IamCSProcess[philosopherList.Length + processList.Length];

            //Array.Resize<IamCSProcess>(ref processList, processList.Length + philosopherList.Length);
            Array.Copy(philosopherList, network, philosopherList.Length);
            Array.Copy(processList, 0, network, philosopherList.Length, processList.Length);


           // processList = processList + philosopherList;

            new CSPParallel(network).run();
        }
    }
}