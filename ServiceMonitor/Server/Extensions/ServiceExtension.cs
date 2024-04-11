using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ServiceMonitor.Server.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigurePostgresqlDb(this IServiceCollection services)
        {
            var databaseUrl = "Host=xixizu.duckdns.org;Port=5432;Database=webpush;Username=postgres;Password=P@ssword1;";
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            services.AddDbContext<ServiceMonitorContext>(options =>
                options.UseNpgsql(builder.ToString()));
        }
        public static void ConfigureSqlite3(this IServiceCollection services) {

            services.AddDbContext<ServiceMonitorContext>(options =>
                options.UseSqlite("Data Source=servicemon.db"));
        }
    }
}
