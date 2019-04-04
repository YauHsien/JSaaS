/*
 *  Job Shuffler as a Service. 通用派工程式
 *  
 *  一個主控程式，在一區工作儲存槽裡，依序揀選一份工作，交付執行。
 *  
 *  有別於可想而知的，此工作槽大概是以佇列 (Queue) 或以堆疊 (Stack) 方式實現，但是，
 *  實情是往往每當手上有一批工作，任何一件工作可能帶來更多工作，於是此工作槽在一般情況
 *  表現出佇列的行為，而在某個時候，在佇列的前端則須表現出堆疊的行為。所以工作槽是一組
 *  雙向佇列。
 *  
 *  當每一件工作派出去讓 CPU 運作之後，它在系統中要在獨立的執行緒 (Thread) 裡執行，
 *  然後， JSaaS 再指派下一件工作，然後再指派下一件工作......每一件工作都只需要獨立、
 *  隔離的運作範圍，因此，甚至可以讓每一件工作發出去為獨立的行程 (Process) 。但是，
 *  我們不採用多行程模型，因為一來若使用了行程，這個程式就得學著跟作業系統周旋，二來，
 *  我們不熟悉多行程的程式控管技術。於是，再提一次：一個接著一個的工作，指派出去，
 *  以一個又一個執行緒的方式運作，主控程式等待它們陸續結束。
 *  
 *  主控程式不會浪費時間去監控所派出去的工作，每一個派出去的工作將運作的情況忠實地
 *  寫在記錄檔裡，以供程式人員做檢測用途；主控程式只須按照本份，派出適當的工作量
 *  給 CPU 。因此，主控程式能接受一個參數指示每秒可以派出多少件工作，使它小心地
 *  保持著所要求的速率。
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace JobSuffler_as_a_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicesProvider = BuildDi();
            var runner = servicesProvider.GetRequiredService<Runner>();

            runner.DoAction("Action1");

            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();

            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
        private static ServiceProvider BuildDi()
        {
            return new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .AddTransient<Runner>()
                .BuildServiceProvider();
        }
        public class Runner
        {
            private readonly ILogger<Runner> _logger;

            public Runner(ILogger<Runner> logger)
            {
                _logger = logger;
            }

            public void DoAction(string name)
            {
                _logger.LogDebug(20, "Doing hard work! {Action}", name);
            }


        }
    }
}
