/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Scheduler
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.CPU.x86.Threading.Misc;
namespace Cosmos.CPU.x86.Threading
{
    public static class Scheduler
    {
        static Thread CurrentTask;
        static IQueue<Thread> ThreadQueue;

        public static Process SystemProcess;

        public static Process RunningProcess
        {
            get { return CurrentTask.Process; }
        }

        public static Thread RunningThread
        {
            get { return CurrentTask; }
        }

        public static void Init(uint KernelDirectory, uint StackStart)
        {
            ThreadQueue = new IQueue<Thread>(100);

            SystemProcess = new Process("System", KernelDirectory);
            CurrentTask = new Thread(SystemProcess, 0, StackStart, 10000, false);

            CurrentTask.Start();
        }

        public static void AddThread(Thread th)
        {
            ThreadQueue.Enqueue(th);
        }

        //[Label("__Switch_Task__")]
        public static uint SwitchTask(uint aStack)
        {
            var NextTask = InvokeNext();

            if (CurrentTask == NextTask)
                return aStack;

            CurrentTask.SaveStack(aStack);
            if (NextTask.Process != CurrentTask.Process)
                NextTask.Process.SetEnvironment();

            CurrentTask = NextTask;
            return NextTask.LoadStack();
        }

        private static Thread InvokeNext()
        {
            var state = CurrentTask.Status;
            switch (state)
            {
                case ThreadState.Running:
                    ThreadQueue.Enqueue(CurrentTask);
                    break;
                default:// Do nothing for not active
                    break;
            }

            return ThreadQueue.Dequeue();
        }
    }
}
