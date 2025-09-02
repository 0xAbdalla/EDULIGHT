using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDULIGHT.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Instructor_imageURL",
                table: "AspNetUsers",
                newName: "Student_imageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Student_imageURL",
                table: "AspNetUsers",
                newName: "Instructor_imageURL");
        }
    }
}
