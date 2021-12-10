using System;

namespace Core.Job.Services
{
    public interface IPrintJob
    {
        void Print();
    }

    public class PrintJob : IPrintJob
    {
        public void Print()
        {
            Console.WriteLine($"Hanfire recurring job!");
        }
    }
}
