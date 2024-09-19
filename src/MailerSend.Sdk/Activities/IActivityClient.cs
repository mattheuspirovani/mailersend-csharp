using System;

namespace MailerSend.Sdk.Activities;

public interface IActivityClient
{
    Task<ActivityListResponse?> GetActivitiesAsync(string domain, ActivityListOptions options);
    Task<Activity?> GetActivityAsync(string id);

}
