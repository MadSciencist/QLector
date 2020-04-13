using QLector.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace QLector.Security
{
    public class TokenOptionsSection : IValidatable
    {
        [Required(ErrorMessage = "TokenOptionsSection: Key is required")]
        public string Key { get; set; }

        [Required(ErrorMessage = "TokenOptionsSection: TokenLifetimeMinutes is required")]
        [Range(1, int.MaxValue, ErrorMessage = "TokenOptionsSection: TokenLifetimeMinutes min value must be greater than 0")]
        public int TokenLifetimeMinutes { get; set; }

        [Required(ErrorMessage = "TokenOptionsSection: Audience is required")]
        public string Audience { get; set; }

        [Required(ErrorMessage = "TokenOptionsSection: Issuer is required")]
        public string Issuer { get; set; }

        public void Validate()
        {
            var ctx = new ValidationContext(this, null, null);
            Validator.ValidateObject(this, ctx, validateAllProperties: true);
        }
    }
}
