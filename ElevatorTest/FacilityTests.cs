using Elevator.Model;

namespace ElevatorTest
{
    [TestClass]
    public class FacilityTests
    {
        [TestMethod]
        public void Facility_ValidMovement_FromFloor1ToFloor5()
        {
            // Arrange
            var noOfFloors = 10;
            var noOfElevators = 4;
            var waitTimeMS = 100;
            var currentFloor = 1;
            var selectedFloor = 5;

            // Act
            var facility = new Facility(noOfElevators, noOfFloors, waitTimeMS);
            facility.SelectFloor(currentFloor, selectedFloor);

            for (var i = currentFloor; i <= selectedFloor; i++)
            {
                facility.RunElevators();
            }

            // Assert
            Assert.AreEqual(facility.AreElevatorsEmpty(), true);
        }

        [TestMethod]
        public void Facility_InvalidMovement_FromFloor1StillHasOccupant()
        {
            // Arrange
            var noOfFloors = 10;
            var noOfElevators = 4;
            var waitTimeMS = 100;
            var currentFloor = 1;
            var selectedFloor = 5;
            var errorFloor = 3;

            // Act
            var facility = new Facility(noOfElevators, noOfFloors, waitTimeMS);
            facility.SelectFloor(currentFloor, selectedFloor);

            for (var i = currentFloor; i <= errorFloor; i++)
            {
                facility.RunElevators();
            }

            // Assert
            Assert.AreEqual(facility.AreElevatorsEmpty(), false);
        }

        [TestMethod]
        public void Facility_ValidMovement_FromGroundToFloor5ToFloor3()
        {
            // Arrange
            var noOfFloors = 10;
            var noOfElevators = 4;
            var waitTimeMS = 100;
            var currentFloor = 5;
            var selectedFloor = 3;

            // Act
            var facility = new Facility(noOfElevators, noOfFloors, waitTimeMS);
            facility.SelectFloor(currentFloor, selectedFloor);

            for (var i = 1; i <= currentFloor; i++)
            {
                facility.RunElevators();
            }

            for (var i = currentFloor; i >= selectedFloor; i--)
            {
                facility.RunElevators();
            }

            // Assert
            Assert.AreEqual(facility.AreElevatorsEmpty(), true);
        }

        [TestMethod]
        public void Facility_InvalidMovement_FromGroundToFloor5ThenStopsAndNotProceedOnFloor3()
        {
            // Arrange
            var noOfFloors = 10;
            var noOfElevators = 4;
            var waitTimeMS = 100;
            var currentFloor = 5;
            var selectedFloor = 3;

            // Act
            var facility = new Facility(noOfElevators, noOfFloors, waitTimeMS);
            facility.SelectFloor(currentFloor, selectedFloor);

            for (var i = 1; i <= currentFloor; i++)
            {
                facility.RunElevators();
            }

            // Assert
            Assert.AreEqual(facility.AreElevatorsEmpty(), false);
        }

        [TestMethod]
        public void Facility_ValidMovement_2ElevatorsBusy()
        {
            // Arrange
            var noOfFloors = 10;
            var noOfElevators = 4;
            var waitTimeMS = 100;
            var currentFloor = 1;
            var selectedFloor = 5;

            // Act
            var facility = new Facility(noOfElevators, noOfFloors, waitTimeMS);
            facility.SelectFloor(currentFloor, selectedFloor);

            for (var i = currentFloor; i <= (selectedFloor - 4); i++)
            {
                facility.RunElevators();
            }

            facility.SelectFloor(currentFloor, selectedFloor);
            facility.RunElevators();

            // Assert
            Assert.AreEqual(facility.BusyElevatorsCount(), 2);
        }
    }
}