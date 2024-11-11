using Elevator.Model;

namespace Elevator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var noOfElevators = ReadInteger("How many elevators in the facility? (Default selected is 4)", defaultValue: 4);
            var noOfFloors = ReadInteger("How many floors in the facility? (Default selected is 10)", defaultValue: 10);
            var waitingTime = ReadInteger("How many second does an elevator wait on each floor? (Default selected is 10)", defaultValue: 10);

            Console.WriteLine("Initializing facility & its elevators....");

            var facility = new Facility(noOfElevators, noOfFloors, waitingTime * 1000);

            RunElevators(facility);

            while (true)
            {
                var currentFloor = ReadInteger($"What is your current floor? (From 1 to {noOfFloors}).", defaultValue: 1);
                var selectedFloor = ReadInteger($"Now from your current floor of {currentFloor}, which floor do you want to go? (From 1 to {noOfFloors}).", defaultValue: currentFloor);
                facility.SelectFloor(currentFloor, selectedFloor);
            }
        }

        static int ReadInteger(string message, int defaultValue = 0)
        {
            var result = defaultValue;
            var isValid = false;

            while (!isValid)
            {
                Console.WriteLine(message);

                var input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please input a valid number");
                }
            }

            return result;
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
