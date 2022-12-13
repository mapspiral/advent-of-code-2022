using System.Collections;

public sealed record ValueList(string Text) : Value, IEnumerable<Value>
{
    public IEnumerator<Value> GetEnumerator()
    {
        var i = 1;
        do
        {
            var part = GetNext(i);
            if (string.IsNullOrEmpty(part))
            {
                yield break;
            }
            yield return Convert(part);
            i += part.Length + 1;
        } while (i < Text.Length);
    }
    
    public bool? HasCorrectOrder(ValueList otherValues)
    {
        var leftValues = this.ToArray();
        var rightValues = otherValues.ToArray();
        bool? hasCorrectOrder = null;
        for (var currentIndex = 0; currentIndex < leftValues.Length; currentIndex++)
        {
            if (currentIndex >= rightValues.Length)
            {
                break;
            }
            var left = leftValues[currentIndex];
            var right = rightValues[currentIndex];
            if (left is IntValue l1 && right is IntValue r1)
            {
                if (l1.Value == r1.Value)
                {
                    continue;
                }
                return l1.Value < r1.Value;
            }

            if (left is ValueList l2 && right is ValueList r2)
            {
                hasCorrectOrder = l2.HasCorrectOrder(r2);
            }
            else if (left is IntValue l3)
            {
                hasCorrectOrder = new ValueList($"[{l3.Value}]").HasCorrectOrder((ValueList)right!);
            }
            else if (right is IntValue r3)
            {
                hasCorrectOrder = ((ValueList)left).HasCorrectOrder(new ValueList($"[{r3.Value}]")); 
            }

            if (hasCorrectOrder.HasValue)
            {
                return hasCorrectOrder.Value;
            }
        }

        return leftValues.Length == rightValues.Length
            ? null
            : leftValues.Length < rightValues.Length;
    }

    private Value Convert(string input) => input[0] >= '0' && input[0] <= '9'
        ? new IntValue(int.Parse(input))
        : new ValueList(input);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private string GetNext(int start)
    {
        if (Text[start] == '[')
        {
            var bracketCount = 0;
            for (var i = start+1; i < Text.Length; i++)
            {
                if (Text[i] == '[')
                {
                    bracketCount++;
                    continue;
                }

                if (Text[i] == ']')
                {
                    if (bracketCount == 0)
                    {
                        var part = Text[start..(i+1)];
                        return part;
                    }

                    bracketCount--;
                }
            }
        }
            
        for (var i = start; i < Text.Length; i++)
        {
            if (Text[i] == ',' || Text[i] == ']')
            {
                var part = Text[start..i];
                return part;
            }
        }

        return Text[start..];
    }
}