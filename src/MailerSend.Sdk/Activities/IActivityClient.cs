using System;

namespace MailerSend.Sdk.Activities;

public interface IActivityClient
{
    Task<ActivityListResponse?> GetActivitiesAsync(ActivityListOptions? options = null);
    Task<Activity?> GetActivityAsync(string id);

}
