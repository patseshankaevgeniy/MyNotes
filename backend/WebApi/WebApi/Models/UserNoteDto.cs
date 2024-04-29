using Domain.Entities;
using System;

namespace WebApi.Models
{
    public sealed class UserNoteDto
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Сompletion { get; set; }
        public Guid? UserId { get; set; }
        public NotePriority Priority { get; set; }
        public bool? IsActual { get; set; }
    }
}
