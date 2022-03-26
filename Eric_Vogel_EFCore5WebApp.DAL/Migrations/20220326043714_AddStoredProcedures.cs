using Microsoft.EntityFrameworkCore.Migrations;

namespace Eric_Vogel_EFCore5WebApp.DAL.Migrations
{
    public partial class AddStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonPerson",
                newName: "PersonPerson",
                newSchema: "dbo");

            var proc1 = @"
            IF OBJECT_ID('GetPersonsByState', 'P') IS NOT NULL
            DROP PROC UpdateProfilesCountry
            GO
            CREATE PROCEDURE [dbo].[GetPersonsByState]
                @State varchar(255)
            AS
            BEGIN
                SELECT p.*
                FROM Persons p
                INNER JOIN Addresses a on p.Person_Id = a.PersonId
                WHERE a.State = @State
            END";

            var proc2 = @"
            IF OBJECT_ID('AddLookUpItem', 'P') IS NOT NULL
            DROP PROC AddLookUpItem
            GO
            CREATE PROCEDURE [dbo].[AddLookUpItem]
                @Code varchar(255),
                @Description varchar(255),
                @LookUpTypeId int
            AS
            BEGIN
                INSERT INTO LookUps (Code, Description, LookUpType) VALUES (@Code,
                @Description, @LookUpTypeId)
            END";

            migrationBuilder.Sql(proc1);
            migrationBuilder.Sql(proc2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonPerson",
                schema: "dbo",
                newName: "PersonPerson");

            migrationBuilder.Sql(@"DROP PROC GetPersonsByState");
            migrationBuilder.Sql(@"DROP PROC AddLookUpItem");
        }
    }
}
