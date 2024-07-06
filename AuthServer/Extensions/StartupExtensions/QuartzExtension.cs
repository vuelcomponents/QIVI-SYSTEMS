using authServer.Cron;
using Quartz;

namespace authServer.Extensions.StartupExtensions;

public static class QuartzExtension
{
    public static IServiceCollection AddQuartzCron(
        this IServiceCollection services,
        string tokenExpiryMinutes
    )
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(RemoveExpiredTokenJob));
            configure
                .AddJob<RemoveExpiredTokenJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInMinutes(int.Parse(tokenExpiryMinutes))
                                .RepeatForever()
                        )
                );
#pragma warning disable CS0618 // Type or member is obsolete
            configure.UseMicrosoftDependencyInjectionJobFactory();
#pragma warning restore CS0618 // Type or member is obsolete
        });
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        return services;
    }
}
