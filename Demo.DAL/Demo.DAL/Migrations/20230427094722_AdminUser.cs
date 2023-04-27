using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DAL.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [Address], [IsAgree], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3d81ac09-2594-4df2-8d40-67d07f729c5e', NULL, 1, N'Admin', N'ADMIN', N'Admin@Admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAENOtI07xueJz6jXJnLFWn8Bdb0bZKOPtYC0MtdokIBBaeIhCfsD9oC6xPIshfHsCdA==', N'GTWEUDWOGRUNOEZYOXRG7YSPUFATPA3Q', N'287ad65d-7181-4071-bbd6-d20a77a54a24', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Users] WHERE Id='3d81ac09-2594-4df2-8d40-67d07f729c5e'");
        }
    }
}
