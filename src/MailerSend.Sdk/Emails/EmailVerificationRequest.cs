using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class EmailVerificationRequest
{
    [JsonPropertyName("email")]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    public EmailVerificationRequest(string email)
    {
        ValidateEmail(email);
        Email = email;
    }

    private void ValidateEmail(string email)
    {
        var context = new ValidationContext(this) { MemberName = nameof(Email) };
        var results = new List<ValidationResult>();

        Validator.TryValidateProperty(email, context, results);

        if (results.Count > 0)
        {
            throw new ValidationException(results[0].ErrorMessage);
        }
    }
}
