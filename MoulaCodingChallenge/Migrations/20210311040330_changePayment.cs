using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoulaCodingChallenge.Migrations
{
    public partial class changePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserName" },
                values: new object[] { 1, 1, 104.11m, "Eren" });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserName" },
                values: new object[] { 2, 2, 104.4m, "Eren" });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserName" },
                values: new object[] { 3, 3, 104.1m, "Mikasa" });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "PaymentId", "AccountId", "Amount", "ClosedReason", "Date", "Status" },
                values: new object[,]
                {
                    { 1L, 1, 10.33m, null, new DateTime(2021, 3, 11, 3, 53, 30, 100, DateTimeKind.Utc).AddTicks(8733), "Open" },
                    { 2L, 1, 11.33m, null, new DateTime(2021, 3, 11, 3, 54, 30, 100, DateTimeKind.Utc).AddTicks(8733), "Open" },
                    { 3L, 1, 12.33m, null, new DateTime(2021, 3, 11, 3, 55, 30, 100, DateTimeKind.Utc).AddTicks(8733), "Open" },
                    { 4L, 1, 13.33m, null, new DateTime(2021, 3, 11, 3, 56, 30, 100, DateTimeKind.Utc).AddTicks(8733), "Open" },
                    { 5L, 2, 10.33m, null, new DateTime(2021, 3, 11, 3, 53, 30, 101, DateTimeKind.Utc).AddTicks(5366), "Open" },
                    { 6L, 2, 11.33m, null, new DateTime(2021, 3, 11, 3, 54, 30, 101, DateTimeKind.Utc).AddTicks(5366), "Open" },
                    { 7L, 2, 12.33m, null, new DateTime(2021, 3, 11, 3, 55, 30, 101, DateTimeKind.Utc).AddTicks(5366), "Open" },
                    { 8L, 2, 13.33m, null, new DateTime(2021, 3, 11, 3, 56, 30, 101, DateTimeKind.Utc).AddTicks(5366), "Open" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "PaymentId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
