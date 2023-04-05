using Lift;

namespace LiftTests
{
    public class LiftTests
    {
        // filed for the class LiftSimulator. We need it to be able to use the class instance outside of the SetUp method.
        private LiftSimulator lift;
        [SetUp]
        public void Setup()
        {
            //Create the instance of the class with empty constructor (as the class definition request) that will be called for every unit test.
            lift = new LiftSimulator();
        }

        [Test]
        //valid input for FitPeopleOnTheLift method
        public void FitPeopleOnTheLiftMethodShouldWorkAsExpected()
        {
            // expected array result that should be returned from the method
            int[] expected = { 4, 4, 4, 3 };

            // actual result that methods returns with given parameters
            int[] actual = lift.FitPeopleOnTheLift(15, new[] { 0, 0, 0, 0 });

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        //there are two test cases for invalid people count test for FitPeopleOnTheLift method
        public void FitPeopleOnTheLiftMethodSholdThrowExpectionWhenPeopleCountUnderOrEqualToZero(int waitingPeople)
        {
            Assert.That(() =>
            {
                lift.FitPeopleOnTheLift(waitingPeople, new int[] { 0, 0, 0 });
            }, Throws.ArgumentException.With.Message.EqualTo("People waiting should be > 0")); //You can use InstanceOf and doesn't need to check for exception message
        }

        [TestCase(new int[] { 5 })]
        [TestCase(new int[] { -1 })]
        //Two test cases for cabin size - below zero and over the max possible value of 4 for FitPeopleOnTheLift method
        public void FitPeopleOnTheLiftMethodSholdThrowExpectionWhenUnderZeroOrOverMaxCapacityOfCabine(int[] arr)
        {
            Assert.That(() =>
            {
                lift.FitPeopleOnTheLift(15, arr);
            }, Throws.ArgumentException.With.Message.EqualTo("Invalid lift seat: " + arr[0])); //You can use InstanceOf and doesn't need to check for exception message
        }

        [TestCase(null)]
        [TestCase(new int[0])]
        //Two test cases for lift state (null or empty array) for FitPeopleOnTheLift method
        public void FitPeopleOnTheLiftMethodSholdThrowExpectionWhenNullOrEmptyArray(int[] arr)
        {
            Assert.That(() =>
            {
                lift.FitPeopleOnTheLift(15, arr);
            }, Throws.ArgumentException.With.Message.EqualTo("Invalid lift size. It should have positive number of cabins")); //You can use InstanceOf and doesn't need to check for exception message
        }

        [Test]
        //Test case for not enough space on the lift for FitPeopleOnTheLiftAndGetResult method
        public void FitPeopleOnTheLiftAndGetResultShouldReturnProperMsgWhenNotEnoughSpace()
        {
            //Expected message that will be returned from FitPeopleOnTheLiftAndGetResult method
            string expected = "There isn't enough space! 10 people in a queue!\r\n4 4 4";

            //Actual result from the method when people over lift capacity is given
            string actual = lift.FitPeopleOnTheLiftAndGetResult(20, new[] { 0, 2, 0 });

            //Assert the equality of expected and actual results
            Assert.AreEqual(expected, actual);
        }

        [Test]
        //Test case for lefted empty spaces on the lift when people are less than lift capacity
        public void FitPeopleOnTheLiftAndGetResultShouldReturnProperMsgWHenHaveLeftedSlots()
        {
            //Expected message that will be returned from FitPeopleOnTheLiftAndGetResult method
            string expected = "The lift has 1 empty spots!\r\n4 4 4 3";

            //Actual result from the method when people are below the lift max capacity
            string actual = lift.FitPeopleOnTheLiftAndGetResult(15, new[] { 0, 0, 0, 0 });

            //Assert the equality of expected and actual results
            Assert.AreEqual(expected, actual);
        }

        [Test]
        //Test case when people count and lift capacity are equal - the lift is full and no empty seat is lefted.
        public void FitPeopleOnTheLiftAndGetResultShouldReturnProperMsgWhenLiftIsFull()
        {
            //Expected message that will be returned from FitPeopleOnTheLiftAndGetResult method
            string expected = "All people placed and the lift if full.\r\n4 4 4";

            //Actual result message when lift capacity is full.
            string actual = lift.FitPeopleOnTheLiftAndGetResult(3, new[] { 2, 4, 3 });

            //Assert the equality of expected and actual results
            Assert.AreEqual(expected, actual);
        }

        
    }
}