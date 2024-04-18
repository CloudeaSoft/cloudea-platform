namespace Cloudea.Application.Identity.Contracts;

public sealed record ResetPasswordRequest(string Email, string NewPassword, string VerCode);
