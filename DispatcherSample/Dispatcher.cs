using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public sealed class Dispatcher : IDisposable
    {

        private readonly object SyncRoot = new object();

        private Queue<ICommand> InputQueue = new Queue<ICommand>();

        private ManualResetEvent SignalDispatcherThread = new ManualResetEvent(false);

        public bool IsDispossed { get; private set; }

        private Thread DispatcherThread;


        //TODO, auch andere Typen
        private List<Task> RunningJobs = new List<Task>();

        private List<ICommand> Results = new List<ICommand>();

        public int MaxJob { get; set; } = Environment.ProcessorCount;

        public Dispatcher()
        {
            DispatcherThread = new Thread(DispatcherWorker);
            DispatcherThread.IsBackground = true;
            DispatcherThread.Name = this.GetType().FullName;
            DispatcherThread.Start();
        }


        public void RegisterCommand(ICommand command)
        {
            lock (SyncRoot)
            {
                InputQueue.Enqueue(command);
                SignalDispatcherThread.Set();
            }

        }

        public List<ICommand> GetResults()
        {
            lock(SyncRoot)
            {
                List<ICommand> results = Results.ToList();
                Results.Clear();
                return results;
            }

        }
    

        public void Dispose()
        {
            lock (SyncRoot)
            {
                if (IsDispossed)
                {
                    return;
                }

                IsDispossed = true;

                SignalDispatcherThread.Set();
                DispatcherThread.Join(1000);
                if (DispatcherThread.IsAlive)
                {
                    DispatcherThread.Abort();
                }

                SignalDispatcherThread.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        private void DispatcherWorker()
        {
            while (!IsDispossed)
            {
                try
                {

                    SignalDispatcherThread.WaitOne();

                    if (IsDispossed)
                        return;


                    lock (SyncRoot)
                    {
                        // Gibt es neue Jobs?
                        if (InputQueue.Count > 0 && RunningJobs.Count < MaxJob)
                        {
                            ICommand commandToStart = InputQueue.Dequeue();


                            //TODO
                            //switch (commandToStart.ExecutionType)
                            //{

                            //ExecutionType.Process:
                                //Process.startProcess...

                            //}

                           
                            /////// StartTastk Methode (Icommand commandToStart)
                            RunningJobs.Add(Task.Factory.StartNew((commandToExecute) =>
                            {
                                Command command = commandToExecute as Command;
                                command.Execute(new DispatcherContext(CancellationToken.None));
                                lock (SyncRoot)
                                {
                                    Task taskToRemove = RunningJobs.Single(task => task.Id == Task.CurrentId);
                                    RunningJobs.Remove(taskToRemove);
                                    command.EndTime = DateTime.UtcNow;
                                    Results.Add(command);
                                    SignalDispatcherThread.Set();
                                }
                            },
                            commandToStart));
                            ////////
                        }
                        else
                        {
                            //Schlafen gehen falls schon mehr als 
                            SignalDispatcherThread.Reset();
                        }

                        //Habe ich noch Kapazität?

                    }




                }
                catch (Exception exception)
                {

                }
            }
        }


    }
}
