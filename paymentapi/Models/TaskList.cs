namespace paymentapi.Models
{
    public class TaskList
    {
         public string Id { get; set; }
         public string? Name { get; set; }
        public string? Assignee { get; set; }
        public string? Created { get; set; }
        public string? Due { get; set; }

        public string? FollowUp { get; set; }
        public string? DelegationState { get; set; }    

        public string? Description { get; set; }
        public string? ExecutionId { get; set; }
        public string? Qwner { get; set; }
        public string? ParentTaskId { get; set; }
        public long Priority { get; set; }
        public string? ProcessDefinitionId { get; set; }
       public string? TaskDefinitionKey { get; set; }
        public string?  CaseExecutionId { get; set; }
        public string? CaseInstanceId { get; set; }
        public string? CaseDefinitionId { get; set; }
        public bool Suspended { get; set; }
        public string? FormKey { get; set; }
        public string? TenantId { get; set; }
    }
}
