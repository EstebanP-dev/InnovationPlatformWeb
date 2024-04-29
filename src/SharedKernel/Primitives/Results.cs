namespace SharedKernel.Primitives;

public readonly struct Created;
public readonly struct Deleted;
public readonly struct Success;
public readonly struct Updated;

public sealed class Results
{
    public static Created Created => new();
    public static Deleted Deleted => new();
    public static Success Success => new();
    public static Updated Updated => new();
}
