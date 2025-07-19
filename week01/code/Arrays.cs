public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create an array to hold the result with the size equal to the number of multiples requested
        // Step 2: Loop from 1 up to length
        // Step 3: Calculate the current multiple by multiplying the number by the loop index (i)
        // Step 4: Store the calculated multiple in the array at position i-1 (because arrays are zero-indexed)
        // Step 5: Return the filled array of multiples

        double[] multiples = new double[length];

        for (int i = 1; i <= length; i++)
        {
            multiples[i - 1] = number * i;
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Understand that rotating right by 'amount' means taking the last 'amount' elements and moving them to the front of the list, while shifting the rest to the right.

        // Step 2: Use List.GetRange to slice the list into two parts:
        //         - The last 'amount' elements (to be moved to the front)
        //         - The first part of the list (elements before the last 'amount')

        // Step 3: Create a new list by concatenating the last 'amount' elements followed by the first part.

        // Step 4: Clear the original list and add all elements from the new list back into it, so the rotation modifies the original list in-place.

        List<int> lastPart = data.GetRange(data.Count - amount, amount);
        List<int> firstPart = data.GetRange(0, data.Count - amount);

        data.Clear();

        data.AddRange(lastPart);
        data.AddRange(firstPart);
    }
}
