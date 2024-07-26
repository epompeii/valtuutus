﻿using Valtuutus.Data.Tests.Shared;

namespace Valtuutus.Data.SqlServer.Tests;

[Collection("SqlServerSpec")]
public sealed class CheckEngineSpecs : DataCheckEngineSpecs
{
    public CheckEngineSpecs(SqlServerFixture fixture)
    {
        _fixture = fixture;
    }

    protected override void AddSpecificProvider(IValtuutusDataBuilder builder)
    {
        builder.AddSqlServer(_ => _fixture.DbFactory);
    }
}