using FreeSql.Extensions.EfCoreFluentApi;

namespace Identity.Entity;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EfCoreTableFluent<AppRole> builder)
    {
    }
}
