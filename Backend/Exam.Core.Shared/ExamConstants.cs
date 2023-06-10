namespace Exam;

/// <summary>
///     Constants that are configuration and is the Core of the project
/// </summary>
public class ExamConstants
{
    public const string ConnectionStringName = "Default";
    public const string DefaultSchemaName = "web";

    /// <summary>
    ///     Configurations for the application. This config can be a table in the database with TenantId
    ///     if we have multiple tenant and we are going to allow tenants with different config settings.
    /// </summary>
    public class Config
    {
        public const bool IsSoftDelete = true;
        public const bool IsOverwriteSoftDeletedOnAdd = true;
        public const bool IsIncludeSoftDeletedOnGet = false;
    }

    /// <summary>
    ///     Anything that is a configuration for the Entity
    /// </summary>
    public class Entity
    {
        public const string DefaultRobotDirection = "East";
    }
}