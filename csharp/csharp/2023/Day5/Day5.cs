using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day5;

public record Map(long Destination, long Source, long Distance);

static class Day5
{
    public static void Part1()
    {
        var input = File.ReadAllText(Directory.GetCurrentDirectory() + "/2023/Day5/Data.txt");
        var lines = input.Split("\r\n\r\n")
            .Select(x => x.Split("\r\n"))
            .ToList();

        var seeds = lines[0][0].Split(":")[1]
            .Split(" ")
            .Where(x => x != "")
            .Select(long.Parse)
            .ToList();

        var maps = lines.Skip(1)
            .Select(group => group.Skip(1)
                .Select(x =>
                {
                    var mapValues = x.Split(" ").Where(y => y != "").Select(long.Parse).ToList();
                    return new Map(mapValues[0], mapValues[1], mapValues[2]);
                }).ToList()).ToList();

        var lowestLocation = new List<long>();
        var lowestLocation2 = long.MaxValue;
        foreach (var t in seeds)
        {
            var currentValue = t;
            foreach (var currentMap in maps)
            {
                foreach (var row in currentMap)
                {
                    var upperLimit = row.Source + row.Distance;
                    if (row.Source > currentValue || currentValue > upperLimit)
                    {
                        continue;
                    }

                    var diff = row.Destination - row.Source;
                    var res = diff + currentValue;
                    currentValue = res;
                    break;
                }
            }


            lowestLocation.Add(currentValue);
        }

        var result = lowestLocation.Min();
        result.Should().Be(157211394);
        Console.WriteLine(lowestLocation2);
    }

    public static void Part2()
    {
        var input = File.ReadAllText(Directory.GetCurrentDirectory() + "/2023/Day5/Data.txt");
        var lines = input.Split("\r\n\r\n")
            .Select(x => x.Split("\r\n"))
            .ToList();

        var seeds = lines[0][0].Split(":")[1]
            .Split(" ")
            .Where(x => x != "")
            .Select(long.Parse)
            .ToList()
            .Chunk(2)
            .ToList();

        var maps = lines.Skip(1)
            .Select(group => group.Skip(1)
                .Select(x =>
                {
                    var mapValues = x.Split(" ").Where(y => y != "").Select(long.Parse).ToList();
                    return new Map(mapValues[0], mapValues[1], mapValues[2]);
                }).ToList()).ToList();

        var lowestLocation = long.MaxValue;
        Console.WriteLine(seeds.Count);
        // takes 10 ish minutes to run, could be optimized
        for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var t = seeds[seedIndex];
            for (var i = t[0]; i < t[0] + t[1]; i++)
            {
                var currentValue = i;
                foreach (var currentMap in maps)
                {
                    foreach (var row in currentMap)
                    {
                        var upperLimit = row.Source + row.Distance;
                        if (row.Source > currentValue || currentValue > upperLimit)
                        {
                            continue;
                        }

                        var diff = row.Destination - row.Source;
                        var res = diff + currentValue;
                        currentValue = res;
                        break;
                    }
                }

                if (currentValue < lowestLocation)
                {
                    lowestLocation = currentValue;
                }
            }

            Console.WriteLine($"Finished with seed group {seedIndex} out of {seeds.Count}");
        }

        Console.WriteLine(lowestLocation);
    }
}