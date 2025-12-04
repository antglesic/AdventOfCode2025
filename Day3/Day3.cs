var banks = File.ReadAllLines("Day3PuzzleInput.txt")
	.Select(line =>
	{
		string bank = line;
		return bank;
	})
	.ToList();

var joltSum = GetJolts(banks)
	.Select(jolt =>
	{
		int power = int.Parse(jolt);
		return power;
	})
	.Sum();
//var jolts = GetJolts(new List<string>() { "5313313222133223232222173222221322232243522222432422223223232222322322332122525322132232325523243253" });

Console.WriteLine("Jolt sum: " + joltSum);

long biggerJoltSum = GetBiggerJolts(banks, 12)
	.Select(long.Parse)
	.Sum();

Console.WriteLine("Bigger joltage sum: " + biggerJoltSum);

static List<string> GetJolts(List<string> banks)
{
	List<string> retval = [];

	foreach (var bank in banks)
	{
		// 5313313222133223232222173222221322232243522222432422223223232222322322332122525322132232325523243253
		// length = 100
		int length = bank.Length;
		List<int> numbers = [];

		int max = 0;
		int maxPosition = 0;

		int nextMax = 0;
		int nextMaxPosition = 0;

		//Console.WriteLine("Bank: " + bank);
		//Console.WriteLine("Length: " + length);

		for (int i = 0; i < length; i++)
		{
			// 5
			// 7
			numbers.Add(int.Parse(bank[i].ToString()));

			//Console.WriteLine("Char: " + bank[i].ToString());
			//Console.WriteLine("Old Max: " + max);
			// 5 > 0 && 1 < 100
			// 7 > 5 && 23 < 100
			if (numbers[i] > max && (i + 1 < length))
			{
				// max = 5
				// maxPosition = 0

				// max = 7
				// maxPosition = 22
				max = numbers[i];
				maxPosition = i;
			}

			//Console.WriteLine("New Max: " + max);
		}

		for (int i = maxPosition + 1; i < length; i++)
		{
			//Console.WriteLine("Char: " + bank[i].ToString());
			//Console.WriteLine("Old NextMax: " + nextMax);

			if (numbers[i] > nextMax)
			{
				nextMax = numbers[i];
				nextMaxPosition = i;
			}

			//Console.WriteLine("New NextMax: " + nextMax);
		}

		if (max > 0 && nextMax > 0)
			retval.Add(string.Format("{0}{1}", max, nextMax));

		//Console.WriteLine("Retval: " + retval);
	}

	return retval;
}


static IEnumerable<string> GetBiggerJolts(IEnumerable<string> banks, int digitsToKeep)
{
	foreach (var bank in banks)
	{
		int toDrop = bank.Length - digitsToKeep;
		var stack = new List<char>(bank.Length);

		for (int i = 0; i < bank.Length; i++)
		{
			char d = bank[i];

			while (toDrop > 0 && stack.Count > 0 && stack.Last() < d)
			{
				stack.RemoveAt(stack.Count - 1);
				toDrop--;
			}

			stack.Add(d);
		}

		if (stack.Count > digitsToKeep)
			stack.RemoveRange(digitsToKeep, stack.Count - digitsToKeep);

		yield return new string(stack.ToArray(), 0, digitsToKeep);
	}
}