namespace CQRS.Core.Exeptions
{
    public class AggregateNotFoundExeption: Exception
    {
        public AggregateNotFoundExeption(string message): base(message)
        {
            
        }
    }
}