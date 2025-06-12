using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                migrationBuilder.AddColumn<string>(
                    name: "ImageUrl",
                    table: "Product",
                    type: "nvarchar(200)",
                    maxLength: 200,
                    nullable: true);
            }
            catch
            {
                // Column might already exist, ignore the error
                // This is handled by the SQL check in the safer version
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Product");
        }
    }
}