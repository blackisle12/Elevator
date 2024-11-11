namespace ElevatorTest
{
    [TestClass]
    public class ElevatorTests
    {
        [TestMethod]
        public void Elevator_Init_DefaultSettings()
        {
            // Arrange
            var noOfFloors = 10;

            // Act
            var elevator = new Elevator.Model.Elevator(id: 1, noOfFloors);

            // Assert
            Assert.AreEqual(elevator.Current, 1);
            Assert.AreEqual(elevator.IsGoingUp, true);
        }

        [TestMethod]
        public void Elevator_ValidFloor_AddQueue()
        {
            // Arrange
            var noOfFloors = 10;
            var selectedFloor = 5;

            // Act
            var elevator = new Elevator.Model.Elevator(id: 1, noOfFloors);
            elevator.AddQueue(selectedFloor);

            // Assert
            Assert.AreEqual(elevator.Queue.TryPeek(out int floor), true);
            Assert.AreEqual(floor, selectedFloor);
        }

        [TestMethod]
        public void Elevator_InvalidFloor_NotAddQueue()
        {
            // Arrange
            var noOfFloors = 5;
            var selectedFloor = 10;

            // Act
            var elevator = new Elevator.Model.Elevator(id: 1, noOfFloors);
            elevator.AddQueue(selectedFloor);

            // Assert
            Assert.AreEqual(elevator.Queue.TryPeek(out int floor), false);
        }

        [TestMethod]
        public void Elevator_Valid_MoveToSecondFloor()
        {
            // Arrange
            var noOfFloors = 2;
            var selectedFloor = 2;

            // Act
            var elevator = new Elevator.Model.Elevator(id: 1, noOfFloors);
            elevator.AddQueue(selectedFloor);
            elevator.Maneuver();

            // Assert
            Assert.AreEqual(elevator.Queue.TryDequeue(out int floor), false);
            Assert.AreEqual(selectedFloor, elevator.Current);
        }

        [TestMethod]
        public void Elevator_Invalid_NotYetOnThirdFloor()
        {
            // Arrange
            var noOfFloors = 3;
            var selectedFloor = 3;

            // Act
            var elevator = new Elevator.Model.Elevator(id: 1, noOfFloors);
            elevator.AddQueue(selectedFloor);
            elevator.Maneuver();

            // Assert
            Assert.AreEqual(elevator.Queue.TryDequeue(out int floor), true);
            Assert.AreNotEqual(selectedFloor, elevator.Current);
        }
    }
}