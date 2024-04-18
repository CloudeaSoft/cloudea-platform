namespace Cloudea.Application.Identity.Contracts;

public sealed record ChangePasswordRequest(string OldPassword, string NewPassword);
