namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public object OperationClaimId;

        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaim { get; set; }
    }
}