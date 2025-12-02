var rotations = File.ReadAllLines("Day1PuzzleInput.txt")
	.Select(line =>
	{
		Direction direction = line[0] switch
		{
			'L' => Direction.Left,
			'R' => Direction.Right,
			_ => throw new FormatException($"Invalid direction char: {line[0]}")
		};

		int moves = int.TryParse(line.Substring(1), out int val) ? val : 0;
		return (direction, moves);
	})
	.ToList();

if (rotations.Count is 0)
	return;

int startingPosition = 50;
int currentPosition = startingPosition;
int zeroPositions = 0;
int zeroPasses = 0;

foreach (var (direction, moves) in rotations)
{
	int step = direction is Direction.Left ? -1 : 1;

	for (int i = 0; i < moves; i++)
	{
		currentPosition = (currentPosition + step + 100) % 100;

		if (currentPosition == 0)
			zeroPasses++;
	}

	if (currentPosition == 0)
		zeroPositions++;
}

Console.WriteLine("Number of zero positions: {0}", zeroPositions);
Console.WriteLine("Number of zero passes: {0}", zeroPasses);


enum Direction
{
	Left = 'L',
	Right = 'R'
}