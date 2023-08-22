using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommentIdToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Comment_Id", // Update the foreign key reference here if needed
                table: "Animal");

            // Rename primary key column in the "Comment" table
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comment",
                newName: "CommentId");

            // Rename any foreign key columns referencing "Comment" in other tables
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Animal",
                newName: "CommentId");

            // ... Update other RenameColumn calls for other related tables ...

            // Update foreign key constraint for the "Animal" table
            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Comment_CommentId", // Update the foreign key reference here if needed
                table: "Animal",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            // ... Add other AddForeignKey calls for other related tables ...
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Comment_CommentId", // Update the foreign key reference here if needed
                table: "Animal");

            // Rename primary key column back to "Id" in the "Comment" table
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comment",
                newName: "Id");

            // Rename any foreign key columns referencing "Comment" back to "Id" in other tables
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Animal",
                newName: "Id");

            // ... Update other RenameColumn calls for other related tables ...

            // Update foreign key constraint for the "Animal" table
            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Comment_Id", // Update the foreign key reference here if needed
                table: "Animal",
                column: "Id",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // ... Add other AddForeignKey calls for other related tables ...
        }

    }
}
