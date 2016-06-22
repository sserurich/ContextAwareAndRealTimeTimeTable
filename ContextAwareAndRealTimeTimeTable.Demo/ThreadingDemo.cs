using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ContextAwareAndRealTimeTimeTable.Demo
{
    public class ThreadingDemo
    {
        public static void A()
        {
            
            Console.WriteLine('A');
        }

       public static void B()
        {
            Thread.Sleep(100);
            Console.WriteLine('B');
        }
    }
}
