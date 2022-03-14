using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMeApi.Migrations
{
    public partial class AddUserToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserInfos",
                type: "datetime(6)",
                nullable: true,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "创建时间")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserInfos",
                type: "datetime(6)",
                nullable: true,
                comment: "生日",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "生日");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "PostItNotes",
                type: "datetime(6)",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "修改时间")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PostItNotes",
                type: "datetime(6)",
                nullable: true,
                comment: "发布时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "发布时间")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "PostItNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PostItNotes_UserInfoId",
                table: "PostItNotes",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostItNotes_UserInfos_UserInfoId",
                table: "PostItNotes",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostItNotes_UserInfos_UserInfoId",
                table: "PostItNotes");

            migrationBuilder.DropIndex(
                name: "IX_PostItNotes_UserInfoId",
                table: "PostItNotes");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "PostItNotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "创建时间")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "生日",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "生日");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "PostItNotes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "修改时间")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PostItNotes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "发布时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "发布时间")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
