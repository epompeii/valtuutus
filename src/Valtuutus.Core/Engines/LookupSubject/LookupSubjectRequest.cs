using System.Diagnostics.CodeAnalysis;
using Valtuutus.Core.Data;

namespace Valtuutus.Core.Engines.LookupSubject;

public record LookupSubjectRequest : IWithDepth, IWithSnapToken
{
    [SetsRequiredMembers]
    public LookupSubjectRequest(string entityType, string permission, string subjectType, string entityId, int depth = 10)
    {
        EntityType = entityType;
        Permission = permission;
        SubjectType = subjectType;
        EntityId = entityId;
        Depth = depth;
    }

    public LookupSubjectRequest() {}
    
    
    public required string EntityType { get; init; }
    public required string EntityId { get; init; }
    public required string Permission { get; init; }
    public required string SubjectType { get; init; }
    public SnapToken? SnapToken { get; set; }
    public int Depth { get; set; } = 10;
}