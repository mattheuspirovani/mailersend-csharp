# MailerSend SDK for C#

The MailerSend SDK for C# is a comprehensive library designed to simplify integration with the [MailerSend](https://www.mailersend.com/) API. It provides developers with an easy-to-use interface for sending emails, managing domains, tracking activities, and sending SMS messages. This SDK abstracts the complexity of the API, allowing developers to focus on building their applications without worrying about the underlying HTTP details. 

[![Main branch](https://github.com/mattheuspirovani/mailersend-csharp/actions/workflows/main_ci_cd.yml/badge.svg)](https://github.com/mattheuspirovani/mailersend-csharp/actions/workflows/main_ci_cd.yml)

## Table of Contents

- [MailerSend SDK for C#](#mailersend-sdk-for-c)
- [Installation](#installation)
- [Initialization](#initialization)
- [Email Functionality](#email-functionality)
  - [Send Email](#1-send-email)
  - [Send Bulk Email](#2-send-bulk-email)
  - [Get Bulk Email Status](#3-get-bulk-email-status)
  - [Email Verification](#4-email-verification)
  - [Get All Email Verifications](#5-get-all-email-verifications)
- [SMS Functionality](#sms-functionality)
  - [Send SMS](#1-send-sms)
- [Domain Management Functionality](#domain-management-functionality)
  - [Get All Domains](#1-get-all-domains)
  - [Get Domain by ID](#2-get-domain-by-id)
  - [Create a New Domain](#3-create-a-new-domain)
  - [Delete a Domain](#4-delete-a-domain)
  - [Get DNS Records](#5-get-dns-records)
  - [Update Domain Settings](#6-update-domain-settings)
  - [Get Domain Verification Status](#7-get-domain-verification-status)
- [Activity Functionality](#activity-functionality)
  - [Get Activities](#1-get-activities)
  - [Get Activity by ID](#2-get-activity-by-id)
- [Contributing to the Project](#contributing-to-the-project)
- [Official Documentation](#official-documentation)

This table of contents provides quick access to different sections of the SDK documentation, allowing you to easily navigate to the topics of interest.
## Installation

To install the MailerSend SDK for C#, you can use NuGet Package Manager. Run the following command in your terminal or package manager console:

```bash
Install-Package MailerSend.Sdk
```

Alternatively, you can add it directly to your `.csproj` file:

```xml
<PackageReference Include="MailerSend.Sdk" Version="1.0.0" />
```

Once installed, you can begin integrating the SDK into your project by importing the necessary namespaces.

## Getting Started

## Initialization

To begin using the MailerSend SDK, you need to initialize the `MailerSendClient` class by providing your MailerSend API key. This client will give you access to all available services, such as sending emails, managing domains, and more.

### Example

```csharp
using MailerSend.Sdk;

var client = new MailerSendClient("your-api-key");
```

### Optional `HttpClient`

You can optionally pass a custom `HttpClient` if you need to configure specific behaviors (e.g., proxy settings or timeout configurations):

```csharp
var httpClient = new HttpClient();
var client = new MailerSendClient("your-api-key", httpClient);
``` 

After initialization, the `client` object is ready to use for all operations available in the SDK.

## Email Functionality

The MailerSend SDK provides several email-related functionalities, allowing you to send single or bulk emails, check the status of sent emails, and manage email verifications. Below is a list of the available methods, along with examples of how to use them.

### 1. Send Email

Sends a single email to one or more recipients.

```csharp
var sendEmailRequest = new SendEmailRequest(
    new EmailSender("your-email@example.com", "Your Name"),
    new List<EmailRecipient>
    {
        new EmailRecipient("recipient@example.com", "Recipient Name")
    },
    "Subject of the email",
    "This is the plain text body of the email."
);

var status = await client.Email.SendEmailAsync(sendEmailRequest);

if (status == EmailSendStatus.Queued)
{
    Console.WriteLine("Email successfully queued.");
}
```

### 2. Send Bulk Email

Sends multiple emails in bulk. This is useful for sending a large number of personalized emails.

```csharp
var bulkEmailRequests = new List<SendEmailRequest>
{
    new SendEmailRequest(
        new EmailSender("your-email@example.com", "Your Name"),
        new List<EmailRecipient>
        {
            new EmailRecipient("recipient1@example.com", "Recipient 1")
        },
        "Bulk Email Subject",
        "Bulk email text body."
    ),
    new SendEmailRequest(
        new EmailSender("your-email@example.com", "Your Name"),
        new List<EmailRecipient>
        {
            new EmailRecipient("recipient2@example.com", "Recipient 2")
        },
        "Bulk Email Subject",
        "Bulk email text body."
    )
};

var bulkEmailResponse = await client.Email.SendBulkEmailAsync(bulkEmailRequests);

if (bulkEmailResponse != null)
{
    Console.WriteLine($"Bulk email sent with ID: {bulkEmailResponse.BulkEmailId}");
}
```

### 3. Get Bulk Email Status

Retrieves the status of a bulk email that was previously sent.

```csharp
var bulkEmailStatus = await client.Email.GetBulkEmailStatusAsync("bulk-email-id");

if (bulkEmailStatus != null)
{
    Console.WriteLine($"Bulk email state: {bulkEmailStatus.State}");
}
```

### 4. Email Verification

Verifies the validity of an email address to ensure deliverability.

```csharp
var emailVerificationRequest = new EmailVerificationRequest("test@example.com");
var verificationResponse = await client.Email.EmailVerificationAsync(emailVerificationRequest);

if (verificationResponse?.Status == EmailVerificationResult.Valid)
{
    Console.WriteLine("The email address is valid.");
}
```

### 5. Get All Email Verifications

Retrieves a list of all email verifications performed.

```csharp
var verifications = await client.Email.GetAllEmailVerificationsAsync(page: 1, limit: 25);

foreach (var verification in verifications.Data)
{
    Console.WriteLine($"Verification ID: {verification.Id}, Status: {verification.Status.Name}");
}
```

With these methods, you can handle a wide range of email-related tasks, from sending transactional emails to managing bulk campaigns and verifying email addresses.

## SMS Functionality

The MailerSend SDK provides functionality to send SMS messages to one or more recipients. Below is a list of available SMS-related methods, along with usage examples.

### 1. Send SMS

Sends a single SMS message to one or more recipients.

```csharp
var smsRequest = new SmsRequest(
    "SenderName",
    new List<string> { "+1234567890" }, // List of recipients
    "This is the SMS message content."
);

var smsResponse = await client.Sms.SendSmsAsync(smsRequest);

if (smsResponse.IsPaused)
{
    Console.WriteLine("SMS sending is paused.");
}
else
{
    Console.WriteLine($"SMS sent successfully with Message ID: {smsResponse.MessageId}");
}
```

### Example Breakdown:
- **SenderName**: The name or phone number the message will appear to come from.
- **Recipients**: A list of recipient phone numbers in international format.
- **Message**: The body of the SMS message.

### SMS Response

The `SmsResponse` object provides details about the status of the sent message, including:
- **MessageId**: The unique identifier for the SMS message.
- **IsPaused**: Indicates if the SMS sending is paused due to specific conditions (e.g., sending limits).

This method allows you to easily send SMS notifications, alerts, or marketing messages to multiple recipients with a single API call.

## Domain Management Functionality

The MailerSend SDK provides various methods to manage domains, including creating, updating, and retrieving DNS records for your domains. Below are the domain-related methods and usage examples.

### 1. Get All Domains

Retrieves a list of all domains associated with your MailerSend account.

```csharp
var domainList = await client.Domains.GetDomainsAsync(new DomainsListOptions { Page = 1, Limit = 10 });

if (domainList != null)
{
    foreach (var domain in domainList.Data)
    {
        Console.WriteLine($"Domain: {domain.Name}, Verified: {domain.IsVerified}");
    }
}
```

### 2. Get Domain by ID

Retrieves details for a specific domain by its ID.

```csharp
var domain = await client.Domains.GetDomainAsync("domain-id");

if (domain != null)
{
    Console.WriteLine($"Domain: {domain.Name}, SPF Status: {domain.Spf}, DKIM Status: {domain.Dkim}");
}
```

### 3. Create a New Domain

Registers a new domain in your MailerSend account.

```csharp
var createDomainRequest = new CreateDomainRequest("yourdomain.com")
{
    ReturnPathSubdomain = "mail",
    CustomTrackingSubdomain = "track",
    InboundRoutingSubdomain = "inbound"
};

var createdDomain = await client.Domains.CreateDomainAsync(createDomainRequest);

if (createdDomain != null)
{
    Console.WriteLine($"Domain created: {createdDomain.Name}");
}
```

### 4. Delete a Domain

Deletes an existing domain by its ID.

```csharp
var isDeleted = await client.Domains.DeleteDomainAsync("domain-id");

if (isDeleted)
{
    Console.WriteLine("Domain successfully deleted.");
}
else
{
    Console.WriteLine("Failed to delete the domain.");
}
```

### 5. Get DNS Records

Retrieves DNS records for a specific domain. These records include SPF, DKIM, and MX configuration details.

```csharp
var dnsRecords = await client.Domains.GetDnsRecordsAsync("domain-id");

if (dnsRecords != null)
{
    Console.WriteLine($"SPF Record: {dnsRecords.Spf?.Hostname}, Value: {dnsRecords.Spf?.Value}");
    Console.WriteLine($"DKIM Record: {dnsRecords.Dkim?.Hostname}, Value: {dnsRecords.Dkim?.Value}");
}
```

### 6. Update Domain Settings

Updates specific settings for a domain, such as pausing email sending or enabling custom tracking.

```csharp
var updateRequest = new UpdateDomainSettingsRequest
{
    SendPaused = true,
    TrackClicks = true,
    CustomTrackingEnabled = true
};

var updatedDomain = await client.Domains.UpdateDomainSettingsAsync("domain-id", updateRequest);

if (updatedDomain != null)
{
    Console.WriteLine($"Domain updated: {updatedDomain.Name}, Send Paused: {updatedDomain.DomainSettings?.SendPaused}");
}
```

### 7. Get Domain Verification Status

Checks the verification status of SPF, DKIM, MX, and other DNS records for a domain.

```csharp
var verificationStatus = await client.Domains.GetDomainVerificationStatusAsync("domain-id");

if (verificationStatus != null)
{
    Console.WriteLine($"SPF Verified: {verificationStatus.Data?.Spf}, DKIM Verified: {verificationStatus.Data?.Dkim}");
}
```

---

These methods allow you to easily manage your domains, configure DNS settings, and ensure your domain is properly set up for sending emails through MailerSend.

## Activity Functionality

The MailerSend SDK allows you to track and retrieve activity logs related to your emails and SMS messages. You can retrieve a list of activities for a domain or fetch details of a specific activity.

### 1. Get Activities

Retrieves a list of activities for a specific domain, such as email sends, opens, clicks, or bounces.

```csharp
var activityOptions = new ActivityListOptions(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow)
{
    Page = 1,
    Limit = 10,
    Event = "opened"
};

var activities = await client.Activity.GetActivitiesAsync("domain-id", activityOptions);

if (activities != null)
{
    foreach (var activity in activities.Data)
    {
        Console.WriteLine($"Activity ID: {activity.Id}, Type: {activity.Type}, Recipient: {activity.Recipient}");
    }
}
```

### 2. Get Activity by ID

Retrieves detailed information about a specific activity by its ID.

```csharp
var activity = await client.Activity.GetActivityAsync("activity-id");

if (activity != null)
{
    Console.WriteLine($"Activity ID: {activity.Id}, Type: {activity.Type}, State: {activity.State}, Recipient: {activity.Recipient}");
}
```

### ActivityListOptions Parameters

- **DomainId**: The ID of the domain for which to retrieve activities.
- **Event**: Filter activities based on event type (e.g., "opened", "clicked", "bounced").
- **DateFrom**: The start date for filtering activities.
- **DateTo**: The end date for filtering activities.
- **Page**: The page number for paginated results.
- **Limit**: The number of activities per page (default is 25).

### Example Breakdown:

- **GetActivitiesAsync**: Retrieves a list of activities filtered by a date range and event type. You can use it to track what actions your recipients are taking with the emails and SMS you send.
- **GetActivityAsync**: Provides detailed information about a specific activity, such as email opens, clicks, or bounces.

---

With these methods, you can easily monitor the engagement of your email and SMS campaigns, ensuring you have insights into how your recipients are interacting with the messages you send through MailerSend.

## Contributing to the Project

Contributions to the MailerSend SDK for C# are welcome! Whether you’re fixing a bug, improving documentation, or adding new features, your help is valuable.

### Steps to Contribute:

1. **Fork the Repository:**
   - Start by forking the repository to your GitHub account.

2. **Clone the Repository:**
   - Clone the forked repository to your local machine:
   ```bash
   git clone https://github.com/your-username/MailerSend.Sdk.git
   ```

3. **Create a New Branch:**
   - Create a new branch for your contribution:
   ```bash
   git checkout -b feature/your-feature-name
   ```

4. **Make Your Changes:**
   - Make sure to follow the code style and guidelines of the project. If you're fixing a bug, please provide a clear and concise description of the issue and the solution in your pull request.

5. **Run Tests:**
   - Ensure that all tests pass before submitting your changes. You can run tests using:
   ```bash
   dotnet test
   ```

6. **Commit Your Changes:**
   - Commit your changes with a meaningful commit message:
   ```bash
   git commit -m "Add feature XYZ"
   ```

7. **Push to Your Fork:**
   - Push your changes to your forked repository:
   ```bash
   git push origin feature/your-feature-name
   ```

8. **Create a Pull Request:**
   - Open a pull request (PR) from your forked repository to the main repository’s `master` or `main` branch. Provide a clear description of your changes, why they are necessary, and any potential impacts.

### Contribution Guidelines:

- **Coding Standards:** Follow the existing coding style used in the project.
- **Testing:** Ensure that any new code is covered by tests. If you're adding a new feature, make sure it includes unit tests.
- **Documentation:** If your change affects functionality, update the relevant documentation in the repository.

### Reporting Issues:

If you encounter any bugs or have a feature request, feel free to open an issue on GitHub. When reporting issues, please provide as much detail as possible to help us address the problem quickly.

---

Your contributions make the MailerSend SDK better for everyone. Thank you for helping improve the project!

## Official Documentation

For complete and up-to-date documentation on how to use the MailerSend API, you can refer to the official MailerSend documentation:

- **MailerSend API Documentation:**  
  [https://developers.mailersend.com](https://developers.mailersend.com)

This official documentation provides detailed information about all available API endpoints, usage examples, authentication methods, error handling, and much more. It's the best resource for understanding the full capabilities of the MailerSend platform and how to integrate it with your applications.

If you’re using this SDK, the official documentation is a great reference for understanding the API features that the SDK implements.



