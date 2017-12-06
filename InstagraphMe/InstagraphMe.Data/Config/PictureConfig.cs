namespace InstagraphMe.Data.Config
{
    using InstagraphMe.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PictureConfig : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(e => e.Id);


        }
    }
}
