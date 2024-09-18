namespace FingersFly.Domain.Exceptions
{
    public class DefinedException : Exception { }

    public class EntityNotFoundException : DefinedException
    {
        public override string Message => "Entity not found!";
    }

    public class NoActionException : DefinedException
    {
        public override string Message => "Failed! No action was taked";
    }
}
