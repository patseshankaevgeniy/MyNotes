using Application.Members.Models;
using System;

namespace WebApi.Models
{
    public sealed class MemberDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public MemberStatus Status { get; set; }
        public string? FirstName { get; init; }
        public string? SecondName { get; init; }
        public string? Email { get; set; }
    }
}
