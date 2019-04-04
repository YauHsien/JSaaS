using System;
using System.Collections.Generic;
using System.Text;

namespace JSaaS.Model.Task
{
    public abstract class AbstractTask<T> : ITask<T>
    {
        public abstract IList<T> Jobs { get; set; }
        public abstract void PerformTask();
        public abstract void PerformTask(T job);
    }
}
