using System.Linq;

namespace Day15
{
    static internal class Part2
    {
        public static void Execute()
        {
            var sequence = File.ReadAllLines("input.txt").First();
            var boxes = new Dictionary<int, List<(string Label, int FocalLength)>>();

            foreach (var step in sequence.Split(','))
            {
                var label = new string(step.TakeWhile(x => x != '=' && x != '-').ToArray());

                var labelHash = 0;
                foreach (var character in label)
                {
                    labelHash = (labelHash + character) * 17 % 256;
                }

                if (step[label.Length] == '=')
                {
                    boxes.TryAdd(labelHash, []);
                    var newFocalLength = int.Parse(new string(step.Take(new Range(new Index(label.Length + 1), new Index(step.Length))).ToArray()));
                    var currentLenses = boxes[labelHash];
                    var currentLensePosition = currentLenses.FindIndex(x => x.Label == label);
                    if (currentLensePosition == -1)
                    {
                        currentLenses.Add((label, newFocalLength));
                    } else
                    {
                        currentLenses.Remove(currentLenses[currentLensePosition]);
                        currentLenses.Insert(currentLensePosition, (label, newFocalLength));
                    }
                }
                else if (step[label.Length] == '-')
                {
                    if (boxes.TryGetValue(labelHash, out var currentLenses))
                    {
                        currentLenses.RemoveAll(x => x.Label == label);
                    }
                }
            }

            var sum = 0;

            foreach (var entry in boxes)
            {
                var prod = 0;
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    prod += (entry.Key + 1) * (i + 1) * entry.Value[i].FocalLength;
                }
                sum += prod;
            }

            Console.WriteLine(sum);
        }
    }
}
