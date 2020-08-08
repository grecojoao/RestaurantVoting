using System;
using Flunt.Notifications;

namespace Voting.Domain.Entities
{
    public abstract class Entity : Notifiable, IEquatable<Entity>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public bool Equals(Entity other) => Id == other?.Id;
    }
}