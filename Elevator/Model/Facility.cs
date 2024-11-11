namespace Elevator.Model
{
    public class Facility
    {
        private readonly int _waitingTimeMS = 10000;
        private readonly List<Elevator> _elevators;

        public Facility(int elevatorCount, int floorCount, int waitingTimeMS)
        {
            _waitingTimeMS = waitingTimeMS;
            _elevators = new List<Elevator>();

            for (var i = 1; i <= elevatorCount; i++)
            {
                _elevators.Add(new Elevator(i, floorCount));
            }
        }

        public void SelectFloor(int currentFloor, int selectedFloor)
        {
            Elevator nearestElevator = null;

            if (currentFloor == selectedFloor)
            {
                return;
            }

            var isGoingUp = selectedFloor > currentFloor;

            if (isGoingUp)
            {
                //get all empty or elevator going up (above the selected floor and current floor)
                nearestElevator = _elevators
                    .Where(e =>
                        (e.IsGoingUp && currentFloor >= e.Current && selectedFloor > e.Current) ||
                        e.Queue.Count == 0)
                    .OrderBy(e => Math.Abs(e.Current - currentFloor))
                    .FirstOrDefault();
            }
            else
            {
                //get all empty or elevator going down (above the selected floor and current floor)
                nearestElevator = _elevators
                    .Where(e =>
                        (!e.IsGoingUp && currentFloor <= e.Current && selectedFloor < e.Current) ||
                        e.Queue.Count == 0)
                    .OrderBy(e => Math.Abs(e.Current - currentFloor))
                    .FirstOrDefault();
            }

            if (nearestElevator != null)
            {
                nearestElevator.AddQueue(selectedFloor, nearestElevator.Current == currentFloor ? null : currentFloor);
            }
        }

        public void RunElevators()
        {
            _elevators.ForEach(e =>
            {
                e.Maneuver();
            });

            Thread.Sleep(_waitingTimeMS);
        }

        public int BusyElevatorsCount()
        {
            return _elevators.Count(e => e.Queue.Count > 0);
        }

        public bool AreElevatorsEmpty()
        {
            return BusyElevatorsCount() == 0;
        }
    }
}
