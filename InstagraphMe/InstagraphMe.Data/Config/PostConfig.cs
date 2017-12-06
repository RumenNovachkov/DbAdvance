namespace InstagraphMe.Data.Config
{
    using InstagraphMe.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostConfig : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Caption)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Picture)
                .WithMany(p => p.Posts)
                .HasForeignKey(e => e.PictureId);
        }
    }
}
