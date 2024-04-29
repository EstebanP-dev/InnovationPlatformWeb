using System.Collections.Frozen;

namespace SharedKernel.Primitives;

public abstract class SmartEnumeration<TEnum>(int value, string name)
        : IEquatable<SmartEnumeration<TEnum>>
    where TEnum : SmartEnumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum?> Enumerations = CreateEnumerations();

    public int Value { get; } = value;
    public string Name { get; } = name;

#pragma warning disable CA1000
    public static TEnum? FromValue(int value)
    {
        return Enumerations.GetValueOrDefault(value);
    }

    public static TEnum? FromName(string? name)
    {
        return Enumerations
            .Values
            .SingleOrDefault(
                enumeration =>
                    string.Equals(
                        enumeration?.Name,
                        name,
                        StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<TEnum?> SmartEnumerations => Enumerations.Values;
#pragma warning restore CA1000

    public bool Equals(SmartEnumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is SmartEnumeration<TEnum> && Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Name);
    }

#pragma warning disable CA1000
    public static IEnumerable<TEnum?> GetEnumerations()
#pragma warning restore CA1000
    {
        var result = CreateEnumerations()
            .Values
            .ToList();

        return result;
    }

    private static Dictionary<int, TEnum?> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fields = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly);

        var filteredFields = fields
            .Where(fieldInfo =>
                fieldInfo.FieldType == enumerationType);

        var fieldsForType = filteredFields
            .Select(fieldInfo =>
                (TEnum)fieldInfo
                    .GetValue(default));

        return fieldsForType
            .Where(x => x is not null)
            .ToDictionary(x => x!.Value);
    }
}
