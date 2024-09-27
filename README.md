# MailerSend C# SDK

The **MailerSend C# SDK** allows developers to easily integrate with the MailerSend API for sending emails, managing domains, retrieving DNS records, and more, directly from their C# applications.

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
  - [Send an Email](#send-an-email)
- [API Endpoints](#api-endpoints)
  - [Domain Management](#domain-management)
  - [DNS Records](#dns-records)
  - [Email Sending](#email-sending)
- [License](#license)

## Installation

To install the **MailerSend C# SDK**, use **NuGet**. Run the following command in your NuGet Package Manager:

```bash
Install-Package MailerSend.Sdk
```

Or add the package directly to your `.csproj` file:

```xml
<PackageReference Include="MailerSend.Sdk" Version="1.0.0" />
```

## Configuration

To authenticate with MailerSend, you need an **API Key** from your MailerSend account. You can set the API key in your environment variables or pass it directly when instantiating the SDK.

### Example: Setting API Key

```csharp
var apiKey = Environment.GetEnvironmentVariable("MAILERSEND_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("API key is not set. Please set the 'MAILERSEND_API_KEY' environment variable.");
    return;
}
```

Alternatively, you can pass the API key when initializing the client:

```csharp
var emailClient = new EmailClient("your-api-key");
```

## Usage

### Send an Email

Here's an example of how to send an email using the SDK:

```csharp
using MailerSend.Sdk.Emails;

class Program
{
    static async Task Main(string[] args)
    {
        var emailClient = new EmailClient("your-api-key");

        var sendEmailRequest = new SendEmailRequest
        {
            From = new EmailSender { Email = "you@yourdomain.com", Name = "Your Name" },
            To = new List<EmailRecipient>
            {
                new EmailRecipient { Email = "recipient@example.com", Name = "Recipient Name" }
            },
            Subject = "Test Email",
            Text = "This is a plain text email.",
            Html = "<p>This is an HTML email.</p>"
        };

        await emailClient.SendEmailAsync(sendEmailRequest);

        Console.WriteLine("Email sent successfully.");
    }
}
```

### Send with Attachments, CC, BCC, and Personalization

You can also send emails with more advanced options such as attachments, CC, BCC, and personalized content for each recipient.

```csharp
var sendEmailRequest = new SendEmailRequest
{
    From = new EmailSender { Email = "you@yourdomain.com", Name = "Your Name" },
    To = new List<EmailRecipient>
    {
        new EmailRecipient { Email = "recipient@example.com", Name = "Recipient Name" }
    },
    Cc = new List<EmailRecipient>
    {
        new EmailRecipient { Email = "cc@example.com", Name = "CC Recipient" }
    },
    Bcc = new List<EmailRecipient>
    {
        new EmailRecipient { Email = "bcc@example.com", Name = "BCC Recipient" }
    },
    Subject = "Email with Attachments",
    Html = "<p>Check out the attachment below.</p>",
    Attachments = new List<EmailAttachment>
    {
        new EmailAttachment
        {
            Filename = "example.pdf",
            Content = Convert.ToBase64String(File.ReadAllBytes("path/to/example.pdf"))
        }
    },
    Personalization = new List<EmailPersonalization>
    {
        new EmailPersonalization
        {
            Email = "recipient@example.com",
            Data = new Dictionary<string, string> { { "name", "John" } }
        }
    }
};

await emailClient.SendEmailAsync(sendEmailRequest);
```

## API Endpoints

### Domain Management

- **Get Domain Details**: Retrieve domain details such as verification status, DNS records, and more.
  
  Example:
  ```csharp
  var domainDetails = await domainsClient.GetDomainDetailsAsync("domain_id");
  ```

- **Update Domain Settings**: Modify domain settings like tracking and email options.

  Example:
  ```csharp
  var updateRequest = new UpdateDomainSettingsRequest
  {
      SendPaused = true,
      TrackClicks = true
  };
  
  await domainsClient.UpdateDomainSettingsAsync("domain_id", updateRequest);
  ```

### DNS Records

- **Get DNS Records**: Retrieve the DNS records for a given domain.

  Example:
  ```csharp
  var dnsRecords = await domainsClient.GetDnsRecordsAsync("domain_id");
  ```

### Email Sending

- **Send Email**: Send plain text or HTML emails to one or more recipients.
  
  Example:
  ```csharp
  await emailClient.SendEmailAsync(sendEmailRequest);
  ```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
