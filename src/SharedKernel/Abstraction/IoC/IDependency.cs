namespace SharedKernel.Abstraction.IoC;

#pragma warning disable CA1040
public interface IDependency
{
}

public interface ISingletonDependency : IDependency
{
}

public interface IScopedDependency : IDependency
{
}

public interface ITransientDependency : IDependency
{
}
#pragma warning restore CA1040
