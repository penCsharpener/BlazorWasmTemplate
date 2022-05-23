using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWasmTemplate.Api.Data;

public class CustomApiAuthorizationDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, IdentityRole<int>, int>, IPersistedGrantDbContext where TUser : IdentityUser<int>
{
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    public CustomApiAuthorizationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
    {
        _operationalStoreOptions = operationalStoreOptions;
    }

    public DbSet<PersistedGrant> PersistedGrants { get; set; } = default!;
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = default!;
    public DbSet<Key> Keys { get; set; } = default!;

    Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
    }
}
