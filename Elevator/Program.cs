using Elevator.Model;
using System;

namespace Elevator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var noOfElevators = 4;
            var noOfFloors = 10;
            var waitingTime = 10000;

            Console.WriteLine("Initializing facility & its elevators....");

            var facility = new Facility(noOfElevators, noOfFloors, waitingTime);

            RunElevators(facility);

            var random = new Random();

            for (var i = 0; i < 50; i++)
            {
                var currentFloor = random.Next(1, noOfFloors);
                var selecteFloor = random.Next(1, noOfFloors);

                facility.SelectFloor(currentFloor, selecteFloor);
                Thread.Sleep(random.Next(2000, 5000));
            }
        }

        static void RunElevators(Facility facility)
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    facility.RunElevators();
                }
            });
        }
    }
}
