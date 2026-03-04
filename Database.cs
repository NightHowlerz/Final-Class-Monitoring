using System;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2;

/// <summary>
/// Central place for the MySQL connection string and factory.
/// Adjust the connection string to match your MySQL Workbench settings.
/// </summary>
public static class Database
{
    // Normalized builder-driven connection string; override via env vars for quick debugging.
    // SMART_ATTENDANCE_DB_HOST, SMART_ATTENDANCE_DB_PORT, SMART_ATTENDANCE_DB_NAME,
    // SMART_ATTENDANCE_DB_USER, SMART_ATTENDANCE_DB_PASSWORD
    private static MySqlConnectionStringBuilder _builder = CreateBuilder();

    // Localhost convenience string (commented so you can quickly switch when needed)
    // public const string LocalhostConnectionString =
    //     "Server=localhost;Port=3306;Database=smart-attendance-monitoring;User ID=root;Password=root;SslMode=None;";

    public static string ConnectionString
    {
        get => _builder.ConnectionString;
        set => _builder = new MySqlConnectionStringBuilder(value);
    }

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    private static MySqlConnectionStringBuilder CreateBuilder()
    {
        return new MySqlConnectionStringBuilder
        {
            //Server = Get("SMART_ATTENDANCE_DB_HOST", "192.168.1.22"),
            //Port = GetUInt("SMART_ATTENDANCE_DB_PORT", 3306),
            //Database = Get("SMART_ATTENDANCE_DB_NAME", "smart_attendance_monitoring"),
            //UserID = Get("SMART_ATTENDANCE_DB_USER", "user3"),
            //Password = Get("SMART_ATTENDANCE_DB_PASSWORD", "root"),
            //SslMode = MySqlSslMode.Preferred,
            //AllowPublicKeyRetrieval = true,       // avoids stream errors with caching_sha2 when TLS is required
            //ConnectionTimeout = 15,
            //DefaultCommandTimeout = 3000

            Server = Get("SMART_ATTENDANCE_DB_HOST", "localhost"),
            Port = GetUInt("SMART_ATTENDANCE_DB_PORT", 3306),
            Database = Get("SMART_ATTENDANCE_DB_NAME", "smart-attendance-monitoring"),
            UserID = Get("SMART_ATTENDANCE_DB_USER", "root"),
            Password = Get("SMART_ATTENDANCE_DB_PASSWORD", "root"),
            SslMode = MySqlSslMode.Preferred,
            AllowPublicKeyRetrieval = true,       // avoids stream errors with caching_sha2 when TLS is required
            ConnectionTimeout = 15,
            DefaultCommandTimeout = 3000
        };
    }

    private static string Get(string env, string fallback) =>
        Environment.GetEnvironmentVariable(env) ?? fallback;

    private static uint GetUInt(string env, uint fallback)
    {
        var value = Environment.GetEnvironmentVariable(env);
        return uint.TryParse(value, out var parsed) ? parsed : fallback;
    }
}
