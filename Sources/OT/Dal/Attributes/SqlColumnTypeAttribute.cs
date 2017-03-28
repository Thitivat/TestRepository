using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace BND.Services.Security.OTP.Dal.Attributes
{
    public class SqlColumnTypeAttribute : Attribute
    {
        public SqlColumnTypeAttribute(string columnType)
        {
            ColumnType = columnType;
        }

        public string ColumnType { get; private set; }
    }

    public class SqlColumnTypeAttributeConvention : PropertyAttributeConfigurationConvention<SqlColumnTypeAttribute>
    {
        public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, SqlColumnTypeAttribute attribute)
        {
            if (!string.IsNullOrWhiteSpace(attribute.ColumnType))
            {
                configuration
                    .Property(memberInfo)
                    .HasColumnType(attribute.ColumnType);
            }
        }
    }
}
