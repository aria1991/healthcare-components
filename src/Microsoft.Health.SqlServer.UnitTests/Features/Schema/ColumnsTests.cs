// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Data.SqlTypes;
using System.IO;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Health.SqlServer.Features.Schema.Model;
using Xunit;

namespace Microsoft.Health.SqlServer.UnitTests.Features.Schema;

public class ColumnsTests
{
    [Fact]
    public void GivenSqlDataRecordWithVarBinaryColumn_WhenSetVarBinaryValueTwice_ThenFirstValueShouldBeCleaned()
    {
        VarBinaryColumn varBinaryColumn = new VarBinaryColumn("Col1", -1);
        SqlDataRecord record = new SqlDataRecord(varBinaryColumn.Metadata);

        byte[] data1 = new byte[] { 1, 1, 1, 1 };
        byte[] data2 = new byte[] { 1, 1 };
        using Stream input1 = new MemoryStream(data1);
        using Stream input2 = new MemoryStream(data2);

        varBinaryColumn.Set(record, 0, input1);
        Assert.Equal(data1, ((SqlBinary)record.GetSqlValue(0)).Value);
        varBinaryColumn.Set(record, 0, input2);
        Assert.Equal(data2, ((SqlBinary)record.GetSqlValue(0)).Value);
    }

    [Fact(Skip = "true")]
    public void GivenStringValueGreaterThanColumnMax_WhenSettingStringValue_ThenSqlTruncateExceptionThrown()
    {
        var varCharColumn = new VarCharColumn("text", 10);
        var record = new SqlDataRecord(varCharColumn.Metadata);

        Assert.Throws<SqlTruncateException>(() => varCharColumn.Set(record, 0, "Astringwhichislongerthan10characters"));
    }

    [Fact]
    public void GivenANullStringValue_WhenSettingStringValue_ThenSqlDBNullIsSet()
    {
        var varCharColumn = new VarCharColumn("text", 10);
        var record = new SqlDataRecord(varCharColumn.Metadata);

        varCharColumn.Set(record, 0, null);

        Assert.True(record.GetSqlString(0).IsNull);
    }

    [Fact]
    public void GivenANVarCharColumnColumnWithMaxLength_WhenSettingAValue_ThenSqlDBNullIsSet()
    {
        var nVarCharColumn = new NVarCharColumn("textOverflowColumn", -1);
        var record = new SqlDataRecord(nVarCharColumn.Metadata);

        nVarCharColumn.Set(record, 0, "text");

        Assert.True(record.GetSqlString(0).Value.Equals("text", System.StringComparison.Ordinal));
    }


    [Fact(Skip = "Renable after User Story 92202")]
    public void GivenDecimalValueGreaterThanDefinedColumnPrecisionAndScaleMax_WhenSettingDecimalValue_ThenSqlTruncateExceptionThrown()
    {
        var decimalColumn = new DecimalColumn("decimalColumn", 18, 6);
        var record = new SqlDataRecord(decimalColumn.Metadata);
        decimal decimalValue = 1234567890123.0123456M;

        Assert.Throws<SqlTruncateException>(() => decimalColumn.Set(record, 0, decimalValue));
    }
}
