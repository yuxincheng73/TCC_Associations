using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using TCCAssociation.EntityFrameworkCore.Seed;

namespace TCCAssociation.EntityFrameworkCore
{
    [DependsOn(
        typeof(TCCAssociationCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class TCCAssociationEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<TCCAssociationDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        TCCAssociationDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        TCCAssociationDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TCCAssociationEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
              SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
