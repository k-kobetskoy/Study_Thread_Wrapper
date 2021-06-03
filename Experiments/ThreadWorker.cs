using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Experiments
{
    class ThreadWorker<Tresult>
    {
        private readonly Func<object, Tresult> func;
        private object param;
        private Tresult result=default;

        public Tresult Result
        {
            get{
                while (!IsCompleted)
                {
                    Console.WriteLine("Wait");
                    Thread.Sleep(150);
                }
                return result;
            }
            private set => result = value;
        }

        public bool IsCompleted { get; private set; } = false;
        public bool IsSuccess { get; private set; } = false;
        public bool IsFaulted { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public ThreadWorker(Func<object, Tresult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public Tresult Start(object methodParam)
        {
            this.param = methodParam;
            var thread = new Thread(ThreadExec);
            thread.Start();
            return Result;
        }

        public void ThreadExec()
        {
            try
            {
                result = func.Invoke(param);
            }
            catch (Exception e)
            {
                Exception = e;
                IsFaulted = true;
            }
            finally
            {
                IsSuccess = Exception is null;
                IsCompleted = true;
            }
        }

        public void Wait()
        {
            while (IsCompleted == false)
            {
                Console.WriteLine("Wait");
                Thread.Sleep(150);
            }
        }

    }
}
