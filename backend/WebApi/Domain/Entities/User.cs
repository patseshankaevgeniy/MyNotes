using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public Guid? GroupId { get; set; }
    public Guid LanguageId { get; set; }
    public Guid ImageId { get; set; }

    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public DateTime CratedDate { get; set; }
    public DateTime EditedDate { get; set; }

    public Group? Group { get; set; }
    public Language Language { get; set; } = default!;
    public ICollection<UserNote>? Notes { get; set; }
    public MembershipStatus? MembershipStatus { get; set; }
}
