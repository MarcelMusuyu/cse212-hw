
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks; // Required for HttpClient, even if not directly using async/await in EarthquakeDailySummary


public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        // Use a HashSet for O(1) average time complexity for lookups, additions, and removals.
        var wordSet = new HashSet<string>(words);
        var resultPairs = new List<string>();

        foreach (var word in words)
        {
            // Check if the word is still in the set.
            // It might have been removed if it formed a pair with a word processed earlier.
            if (wordSet.Contains(word))
            {
                // Reverse the word
                char[] charArray = word.ToCharArray();
                Array.Reverse(charArray);
                string reversedWord = new string(charArray);

                // Special case: if letters are the same (e.g., "aa"), it's not a symmetric pair for this problem.
                // Also, ensure the reversed word is actually different from the original word.
                // Then, check if the reversed word exists in the set.
                if (word != reversedWord && wordSet.Contains(reversedWord))
                {
                    // To ensure consistent output order for tests (e.g., "ma & am" vs "am & ma"),
                    // we'll always put the lexicographically larger word first in the pair string.
                    if (string.Compare(word, reversedWord, StringComparison.Ordinal) > 0)
                    {
                        resultPairs.Add($"{word} & {reversedWord}");
                    }
                    else
                    {
                        resultPairs.Add($"{reversedWord} & {word}");
                    }

                    // Remove both words from the set to ensure they are not processed again
                    // and to maintain O(n) complexity by not re-evaluating already found pairs.
                    wordSet.Remove(word);
                    wordSet.Remove(reversedWord);
                }
            }
        }

        return resultPairs.ToArray();



    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        try
        {
            foreach (var line in File.ReadLines(filename))
            {
                // Split the line by comma as indicated by the test file's structure
                var fields = line.Split(',');

                // Check if the line has at least 4 columns (0-indexed: index 3)
                if (fields.Length > 3)
                {
                    string degree = fields[3].Trim(); // Get the degree and trim whitespace

                    if (!string.IsNullOrEmpty(degree))
                    {
                        // Increment count if degree exists, otherwise add with count 1
                        if (degrees.ContainsKey(degree))
                        {
                            degrees[degree]++;
                        }
                        else
                        {
                            degrees[degree] = 1;
                        }
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: File not found at {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize words: convert to lowercase and remove spaces
        string normalizedWord1 = new string(word1.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        string normalizedWord2 = new string(word2.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();

        // If lengths are different after normalization, they cannot be anagrams
        if (normalizedWord1.Length != normalizedWord2.Length)
        {
            return false;
        }

        // Use a dictionary to store character counts for the first word
        var charCounts = new Dictionary<char, int>();

        foreach (char c in normalizedWord1)
        {
            if (charCounts.ContainsKey(c))
            {
                charCounts[c]++;
            }
            else
            {
                charCounts[c] = 1;
            }
        }

        // Iterate through the second word and adjust counts
        foreach (char c in normalizedWord2)
        {
            if (charCounts.ContainsKey(c))
            {
                charCounts[c]--;
            }
            else
            {
                // Character from word2 not found in word1's counts
                return false;
            }
        }

        // Finally, check if all counts in the dictionary are zero
        // If any count is not zero, it means the character counts don't match
        foreach (var count in charCounts.Values)
        {
            if (count != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        try
        {
            using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            // TODO Problem 5:
            // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
            // on those classes so that the call to Deserialize above works properly.
            // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
            // 3. Return an array of these string descriptions.

            // Check if features are null or empty
            if (featureCollection?.Features == null || featureCollection.Features.Length == 0)
            {
                return new string[] { "No earthquake data found for today." };
            }

            // Create a string out of each place an earthquake has happened today and its magnitude.
            // Return an array of these string descriptions.
            var formattedStrings = featureCollection.Features
                .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag:F2}") // Format magnitude to 2 decimal places
                .ToArray();

            return formattedStrings;

        }
        catch (HttpRequestException e)
        {
            // Handle HTTP errors (e.g., network issues, 404, 500)
            return new string[] { $"Error fetching data: {e.Message}. Please check your internet connection or the API URL." };
        }
        catch (JsonException e)
        {
            // Handle JSON deserialization errors
            return new string[] { $"Error deserializing JSON: {e.Message}. The data format might have changed." };
        }
        catch (Exception e)
        {
            // Catch any other unexpected errors
            return new string[] { $"An unexpected error occurred: {e.Message}" };
        }
    }
}