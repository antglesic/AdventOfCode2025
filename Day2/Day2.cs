var ranges = File.ReadAllText("Day2PuzzleInput.txt")
	.Split(',')
	.Select(line =>
	{
		var parts = line.Split('-');
		var first = long.Parse(parts[0]);
		var last = long.Parse(parts[1]);
		return (first, last);
	})
	.ToList();

var partsSum = ranges
	.SelectMany(range => GetRange(range.first, range.last))
	.Where(IsPartsRepeating)
	.Sum();

Console.WriteLine("Parts repeating sum: " + partsSum);

var sequenceSum = ranges
	.SelectMany(range => GetRange(range.first, range.last))
	.Where(IsSequenceRepeating)
	.Sum();

Console.WriteLine("Sequence repeating sum: " + sequenceSum);

static IEnumerable<long> GetRange(long start, long end)
{
	for (long i = start; i <= end; i++)
	{
		yield return i;
	}
}

static bool IsPartsRepeating(long number)
{
	// number = 123123
	// digitCount = 6
	// divisor = 1000
	int digitCount = number.ToString().Length;
	long divisor = (long)Math.Pow(10, digitCount / 2);

	// firstHalf = 123123 / 1000 = 123
	// secondHalf = 123123 % = 123
	long firstHalf = number / divisor;
	long secondHalf = number % divisor;

	return firstHalf == secondHalf;
}

static bool IsSequenceRepeating(long number)
{
	// number = 123123
	// length = 6
	string numStr = number.ToString();
	int length = numStr.Length;

	for (int patternLength = 1; patternLength <= length / 2; patternLength++)
	{
		// pattern length alsays half of the length
		// 1 <= 6/2 => 1 <= 3
		// 2 <= 3
		// 3 <= 3


		// 6 % 1 = 0
		// 6 % 2 = 0
		// 6 % 3 = 0
		if (length % patternLength == 0)
		{
			// pattern = 1
			// repeatCount = 6 / 1 = 6

			// pattern = 12
			// repeatCount = 6 / 2 = 3

			// pattern = 123
			// repeatCount = 6 / 3 = 2
			string pattern = numStr.Substring(0, patternLength);
			int repeatCount = length / patternLength;

			// repeated = 111111
			// repeated = 121212
			// repeated = 123123
			string repeated = string.Concat(Enumerable.Repeat(pattern, repeatCount));

			// 111111 -> false
			// 121212 -> false
			// 123123 -> true
			if (repeated == numStr && repeatCount >= 2)
			{
				return true;
			}
		}
	}

	return false;
}