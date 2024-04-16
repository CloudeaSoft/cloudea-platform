namespace Cloudea.Application.Identity.Contracts
{
    public class UpdateUserProfileRequest
    {
        public string DisplayName { get; set; } = default!;

        public string Signature { get; set; } = default!;
    }
}
