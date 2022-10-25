using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Poco.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Poco.Task> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
