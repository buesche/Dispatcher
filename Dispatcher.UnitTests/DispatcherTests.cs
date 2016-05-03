using System;
using DispatcherSample;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DispatcherSample.UnitTests
{
    [TestClass]
    public class DispatcherTests
    {

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void DispatcherBasicTest()
        {



            using (Dispatcher dispatcher = new Dispatcher())
            {
                //Assert.IsTrue(dispatcher.MaxJob > 2, "The amount of reported processors is");
                Random rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    switch (rnd.Next(0, 3))
                    {
                        case 0:
                            dispatcher.RegisterCommand(new LongRunningCommand());
                            break;
                        case 1:
                            dispatcher.RegisterCommand(new EmptyCommand());
                            break;
                        case 2:
                            dispatcher.RegisterCommand(new DataCommand
                            {
                                Data = i.ToString()
                            });
                            break;

                    }
                }
                //Workaround
                List<ICommand> results = dispatcher.GetResults();

                while (results.Count < 100)
                {
                    System.Threading.Thread.Sleep(250);
                    results.AddRange(dispatcher.GetResults());
                }

                TestContext.WriteLine("Count of command: {0}", results.Count);
                TestContext.WriteLine("Average Time ...: {0}", results.Average(command => command.Runtime.TotalMilliseconds));

                TestContext.WriteLine("Amount of LongRunningCommand: {0}", results.OfType<LongRunningCommand>().Count());

                TestContext.WriteLine("Amount of DataCommand: {0}", results.OfType<DataCommand>().Count());

                TestContext.WriteLine("Amount of EmptyCommand: {0}", results.OfType<EmptyCommand>().Count());


            }


            //System.Threading.Thread.Sleep(20000);
        }
    }
}
