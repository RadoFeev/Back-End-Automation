using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    public class LiftSimulator
    {
        const int MAX_SEATS_PER_CABIN = 4;

        //the class method with 2 parameters - integer and integer array. It is returning string.
        public string FitPeopleOnTheLiftAndGetResult(int waitingPeopleCount, int[] inputLiftState)
        {
            int[] liftState = FitPeopleOnTheLift(waitingPeopleCount, inputLiftState);
            int peoplePlaced = liftState.Sum() - inputLiftState.Sum();
            int peopleLeft = waitingPeopleCount - peoplePlaced;
            int maxCapacity = liftState.Length * MAX_SEATS_PER_CABIN;
            int freeSeats = maxCapacity - liftState.Sum();

            // the string for the end result that will be returned from the method
            string result = string.Empty;

            
            if (peoplePlaced < waitingPeopleCount)
                result += $"There isn't enough space! {peopleLeft} people in a queue!"; //adding message to the string result when waiting people for the lift are over lift capacity
            else if (peoplePlaced == waitingPeopleCount)
                if (freeSeats == 0)
                    result += "All people placed and the lift if full."; //adding message to the string result when all lift seats are occupied (there aren't empty seats, nor waiting people)
                else
                    result += $"The lift has {freeSeats} empty spots!"; //adding message to the string result when waiting people are less than lift seat - there are lefted empty seats

            result += $"{Environment.NewLine}{String.Join(' ', liftState)}"; // in all cases adding this string to the string result. It is printing the whole array as space separated integer on the Console.

            return result.TrimEnd(); //return the string result
        }

        //the class method with 2 parameters - integer and integer array. It is returning integer array.
        public int[] FitPeopleOnTheLift(int peopleCount, int[] inputLiftState)
        {
            // Check lift state: overflow / underflow of cabins
            if (inputLiftState == null || inputLiftState.Length == 0)
            {
                throw new ArgumentException(
                    "Invalid lift size. It should have positive number of cabins");
            }

            //  Check the lift size: number of cabins
            foreach (int seats in inputLiftState)
            {
                if (seats < 0 || seats > MAX_SEATS_PER_CABIN)
                    throw new ArgumentException("Invalid lift seat: " + seats);
            }

            // Check the count of waiting people in the queue
            if (peopleCount <= 0)
            {
                throw new ArgumentException("People waiting should be > 0");
            }

            // Clone the liftState
            int[] liftState = inputLiftState.ToArray();

            // Fill the waiting people into cabin seats
            for (int cabin = 0; cabin < liftState.Length; cabin++)
            {
                while (liftState[cabin] < MAX_SEATS_PER_CABIN)
                {
                    if (peopleCount == 0)
                        break;
                    liftState[cabin]++;
                    peopleCount--;
                }
                if (peopleCount == 0)
                    break;
            }

            return liftState; //return the integer array
        }
    }
}
