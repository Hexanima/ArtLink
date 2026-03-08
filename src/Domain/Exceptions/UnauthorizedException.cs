namespace Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("No tienes permiso de hacer esto") { }
}
