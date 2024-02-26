﻿using System.Data;
using Authorizee.Core.Data;
using Dapper;

namespace Authorizee.Data.SqlServer.Utils;

public static class SqlBuilderExtensions
{
    public static SqlBuilder FilterRelations(this SqlBuilder builder, RelationTupleFilter tupleFilter)
    {
        builder = builder.Where("entity_type = @EntityType", new {EntityType = new DbString()
        {
            Value = tupleFilter.EntityType,
            IsAnsi = true,
            Length = 256
        }});
        builder = builder.Where("entity_id = @EntityId", new {EntityId = new DbString()
        {
            Value = tupleFilter.EntityId,
            IsAnsi = true,
            Length = 64
        }});
        builder = builder.Where("relation = @Relation", new {Relation = new DbString()
        {
            Value = tupleFilter.Relation,
            IsAnsi = true,
            Length = 64
        }});

        if (!string.IsNullOrEmpty(tupleFilter.SubjectId))
            builder = builder.Where("subject_id = @SubjectId", new {SubjectId = new DbString()
            {
                Value = tupleFilter.SubjectId,
                IsAnsi = true,
                Length = 64
            }});
        
        if (!string.IsNullOrEmpty(tupleFilter.SubjectRelation))
            builder = builder.Where("subject_relation = @SubjectRelation", new {SubjectRelation =new DbString()
            {
                Value = tupleFilter.SubjectRelation,
                IsAnsi = true,
                Length = 64
            }});
        
        if (!string.IsNullOrEmpty(tupleFilter.SubjectType))
            builder = builder.Where("subject_type = @SubjectType", new {SubjectType = new DbString()
            {
                Value = tupleFilter.SubjectType,
                IsAnsi = true,
                Length = 256
            }});
        
        return builder;
    }
    
    public static SqlBuilder FilterRelations(this SqlBuilder builder, EntityRelationFilter entityRelationFilter,
        string subjectType, IEnumerable<string> entitiesIds, string? subjectRelation)
    {
        var entitiesIdsArr = entitiesIds as string[] ?? entitiesIds.ToArray();
        
        if (!string.IsNullOrEmpty(subjectType))
            builder = builder.Where("subject_type = @SubjectType", new {SubjectType = new DbString()
            {
                Value = subjectType,
                IsAnsi = true,
                Length = 256
            }});
        
        if (!string.IsNullOrEmpty(entityRelationFilter.EntityType))
            builder = builder.Where("entity_type = @EntityType", new {EntityType = new DbString()
            {
                Value = entityRelationFilter.EntityType,
                IsAnsi = true,
                Length = 256
            }});
        
        if (!string.IsNullOrEmpty(entityRelationFilter.Relation))
            builder = builder.Where("relation = @Relation", new {Relation = new DbString()
            {
                Value = entityRelationFilter.Relation,
                IsAnsi = true,
                Length = 64
            }});

        if (entitiesIdsArr.Length > 0)
        {
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            foreach (var entityId in entitiesIdsArr)
                dt.Rows.Add(entityId);
            builder = builder.Where("entity_id in (select id from @entitiesIds)", new {entitiesIds = dt.AsTableValuedParameter("TVP_ListIds")});
        }
        
        if (!string.IsNullOrEmpty(subjectRelation))
            builder = builder.Where("subject_relation = @subjectRelation", new {subjectRelation = new DbString()
            {
                Value = subjectRelation,
                IsAnsi = true,
                Length = 64
            }});
        
        return builder;
    }
    
    public static SqlBuilder FilterRelations(this SqlBuilder builder, EntityRelationFilter entityFilter, IList<string> subjectsIds, string subjectType)
    {
        if (!string.IsNullOrEmpty(entityFilter.EntityType))
            builder = builder.Where("entity_type = @EntityType", new {EntityType = new DbString()
            {
                Value = entityFilter.EntityType,
                IsAnsi = true,
                Length = 256
            }});
        
        if (!string.IsNullOrEmpty(entityFilter.Relation))
            builder = builder.Where("relation = @Relation", new {Relation = new DbString()
            {
                Value = entityFilter.Relation,
                IsAnsi = true,
                Length = 64
            }});
        
        builder.Where("subject_type = @SubjectType", new {SubjectType = new DbString()
        {
            Value = subjectType,
            IsAnsi = true,
            Length = 256
        }});

        if (subjectsIds.Count > 0)
        {
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            foreach (var subjectId in subjectsIds)
                dt.Rows.Add(subjectId);
            builder = builder.Where("subject_id in (select id from @subjectsIds)", new {subjectsIds = dt.AsTableValuedParameter("TVP_ListIds")});
        }
        
        return builder;
    }

    
    public static SqlBuilder FilterAttributes(this SqlBuilder builder, EntityAttributeFilter filter)
    {
        builder = builder.Where("entity_type = @EntityType", new {EntityType = new DbString()
        {
            Value = filter.EntityType,
            IsAnsi = true,
            Length = 256
        }});
        builder = builder.Where("attribute = @Attribute", new {Attribute =new DbString()
        {
            Value = filter.Attribute,
            IsAnsi = true,
            Length = 64
        }});
        
        if (!string.IsNullOrWhiteSpace(filter.EntityId))
            builder = builder.Where("entity_id = @EntityId", new {EntityId = new DbString()
            {
                Value = filter.EntityId,
                IsAnsi = true,
                Length = 64
            }});
        
        return builder;
    }
    
    public static SqlBuilder FilterAttributes(this SqlBuilder builder, AttributeFilter filter, IEnumerable<string> entitiesIds)
    {
        var entitiesIdsArr = entitiesIds as string[] ?? entitiesIds.ToArray();
        
        builder = builder.Where("entity_type = @EntityType", new {EntityType = new DbString()
        {
            Value = filter.EntityType,
            IsAnsi = true,
            Length = 256
        }});
        builder = builder.Where("attribute = @Attribute", new {Attribute = new DbString()
        {
            Value = filter.Attribute,
            IsAnsi = true,
            Length = 64
        }});

        if (entitiesIdsArr.Length != 0)
        {
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            foreach (var entityId in entitiesIdsArr)
                dt.Rows.Add(entityId);
            builder = builder.Where("entity_id in (select id from @entitiesIds)", new {entitiesIds = dt.AsTableValuedParameter("TVP_ListIds")});
        }

        
        return builder;
    }
}