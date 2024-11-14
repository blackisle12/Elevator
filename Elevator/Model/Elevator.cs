namespace Elevator.Model
{
    public class Elevator(int id, int floorCount)
    {
        private readonly int _floorCount = floorCount;
        private bool _isFetching = false;

        public int Id => id;
        public int Current { get; private set; } = 1;
        public bool IsGoingUp { get; private set; } = true;

        public Queue<int> Queue { get; set; } = new Queue<int>();

        public void AddQueue(int selectedFloor, int? currentFloor = null)
        {
            if (selectedFloor <= 0 || selectedFloor > _floorCount)
            {
                Console.WriteLine($"Elevator '{id}': Floor selected '{selectedFloor}' is invalid.");
                return;
            }

            if (selectedFloor != Current || (currentFloor != null && currentFloor != Current) && !Queue.Any(q => q == selectedFloor))
            {
                if (currentFloor == null)
                {
                    Console.WriteLine($"Elevator '{id}': Going from floor '{Current}' to floor '{selectedFloor}'");
                    Queue.Enqueue(selectedFloor);
                }
                else
                {
                    Console.WriteLine($"Elevator '{id}': Currently on floor '{Current}', now moving to floor '{currentFloor}' and fetch passenger, then to '{selectedFloor}'");
                    Queue.Enqueue(currentFloor.Value);
                    Queue.Enqueue(selectedFloor);
                }
            }
        }

        public void Maneuver()
        {
            if (!Queue.Any())
            {
                return;
            }

            var selectedFloor = Queue.Peek();

            if (Current < selectedFloor)
            {
                IsGoingUp = true;
                Current += 1;
            }
            else if (Current > selectedFloor)
            {
                IsGoingUp = false;
                Current -= 1;
            }

            Console.WriteLine($"Elevator '{id}': Now moving '{(IsGoingUp ? "up" : "down")}' to '{Current}' floor.");

            if (Current == selectedFloor)
            {
                Queue.Dequeue();
                Console.WriteLine($"Elevator '{id}:': Passengers either boarded or left the elevator at floor '{Current}'.");
            }
        }
    }
}
