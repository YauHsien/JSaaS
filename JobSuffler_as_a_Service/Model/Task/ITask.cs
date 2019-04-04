using System;
using System.Collections.Generic;
using System.Text;

namespace JSaaS.Model.Task
{
    public interface ITask<T>
    {
        void PerformTask();
        void PerformTask(T Task);
    }
}
