using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HepsiApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refreshtokennullableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 290, DateTimeKind.Local).AddTicks(3750), "Sports" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 290, DateTimeKind.Local).AddTicks(3795), "Grocery, Books & Music" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 290, DateTimeKind.Local).AddTicks(3806), "Toys & Computers" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 27, 13, 37, 26, 291, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 27, 13, 37, 26, 291, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 27, 13, 37, 26, 291, DateTimeKind.Local).AddTicks(7917));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 27, 13, 37, 26, 291, DateTimeKind.Local).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 293, DateTimeKind.Local).AddTicks(997), "Aut." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 293, DateTimeKind.Local).AddTicks(1018), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Yaptı ipsam." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 293, DateTimeKind.Local).AddTicks(1031), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "İpsum." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 294, DateTimeKind.Local).AddTicks(8904), "The Football Is Good For Training And Recreational Purposes", 28m, 653.656054757890987m, "Handcrafted Wooden Shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 294, DateTimeKind.Local).AddTicks(8928), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 30m, 946.874569489946173m, "Small Granite Cheese" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 27, 13, 37, 26, 294, DateTimeKind.Local).AddTicks(8947), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 24m, 338.699885230973872m, "Tasty Fresh Shirt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 720, DateTimeKind.Local).AddTicks(691), "Electronics & Music" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 720, DateTimeKind.Local).AddTicks(719), "Electronics, Shoes & Health" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 720, DateTimeKind.Local).AddTicks(735), "Outdoors & Books" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 26, 10, 52, 39, 723, DateTimeKind.Local).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 26, 10, 52, 39, 723, DateTimeKind.Local).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 26, 10, 52, 39, 723, DateTimeKind.Local).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 26, 10, 52, 39, 723, DateTimeKind.Local).AddTicks(25));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 725, DateTimeKind.Local).AddTicks(1215), "Ut." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 725, DateTimeKind.Local).AddTicks(1237), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Exercitationem lambadaki." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 725, DateTimeKind.Local).AddTicks(1251), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Çakıl." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 727, DateTimeKind.Local).AddTicks(6520), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 24m, 323.842096487764486m, "Unbranded Rubber Gloves" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 727, DateTimeKind.Local).AddTicks(6545), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", 27m, 893.61926322217183m, "Sleek Wooden Chicken" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 11, 26, 10, 52, 39, 727, DateTimeKind.Local).AddTicks(6565), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 12m, 51.538021819178392m, "Practical Granite Shoes" });
        }
    }
}
