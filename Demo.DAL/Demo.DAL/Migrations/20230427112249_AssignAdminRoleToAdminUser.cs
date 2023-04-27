using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace Demo.DAL.Migrations
{
    public partial class AssignAdminRoleToAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[Security].[UserRoles] ([UserId],[RoleId]) values('3d81ac09-2594-4df2-8d40-67d07f729c5e', (SELECT Id FROM[Security].[Roles] WHERE Name = 'Admin'))");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId ='3d81ac09-2594-4df2-8d40-67d07f729c5e'");
        }
    }
}
