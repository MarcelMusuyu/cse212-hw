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
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

    
        /*
            Problem Solving Process

            A. Understand the Problem: The work here is to produce an array of double numbers
                which will contain a N items which are multiples of T. N and T are parameters of this function  
            
            B. Design and Plan: After carefully restating the problem statement, we need to figure out the number of variables
            and their type that would be required to produce the specific output.
            The first variable is the array of doubles which will be the output of this function.
            The second variable is the number which is a double and will be used to calculate the multiples.
            The third variable is the length which is an integer and will be used to determine the size of the array.
             
            The Mathematical Algorithm or Logic for this problem is as follows:

                The multiple of P number is each number in the array divided by the P number produce an whole number.
                Another logic is, a multiple of P number is the outout obtained when P number is added by itself

            As the number of items in the array is N, we can use a for loop to iterate from 0 to N-1 and multiply the index by T to get the multiples. 

            Data Structure: A fixed size array of doubles will be used to store the multiples.

            C. Implementation     
        
        */  
        var result = new double[length]; // Create an array of the specified length
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1); // Calculate the multiple and store it in the array
        }

        return result; // replace this return statement with your own
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
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
            Problem Solving Process

            A. Understand the Problem: The work here is to rotate the list of integers to the right by a specified amount.
            
            B. Design and Plan: After carefully restating the problem statement, we need to figure out the number of variables
            and their type that would be required to produce the specific output.
            The first variable is the list of integers which will be the input to this function.
            The second variable is the amount which is an integer and will be used to determine how many positions to rotate.

            The Algorithm or Logic for this problem is as follows:

                1. We will check if amount parameter is less than or equal the length of the list. 
                2. We will loop the list based the amount of times, retrieve each element of the list from the end of the list and temporary store it in a variable.
                3. We will use the Insert method to add the elements to the front of the list.
                4. We will remove the last element of the list using the RemoveAt method.

                 
               
        

            Data Structure: A dynamic list of integers will be used to store the rotated values.

            C. Implementation
        
        */

        if (amount <= data.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                int lastElement = data[data.Count - 1]; // Get the last element of the list
                data.Insert(0, lastElement); // Insert it at the beginning of the list
                data.RemoveAt(data.Count - 1); // Remove the last element from the list
            }
        
        }
        else
        {
            throw new ArgumentOutOfRangeException("amount", "Amount must be less than or equal to the length of the list.");
        }
          

    }
}
