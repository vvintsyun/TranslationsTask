using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationsTask.Migrations
{
    /// <inheritdoc />
    public partial class AddGetTasksCountByMonthProjectFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE TYPE dbo.IdList
AS TABLE
(
  Id INT
);
GO

CREATE FUNCTION GetTasksCountByMonthProject(@projects dbo.IdList READONLY, @translators dbo.IdList READONLY, @includeNullAssignee BIT)  
RETURNS TABLE
AS  
RETURN  
    SELECT p.Name as Project, MONTH(t.Deadline) as Month, YEAR(t.Deadline) as Year, COUNT(*) as Count FROM Tasks t
    JOIN Projects p on p.Id = t.ProjectId
    WHERE t.ProjectId in (SELECT Id FROM @projects) AND (t.AssigneeId in (SELECT Id FROM @translators) OR @includeNullAssignee = 1 AND t.AssigneeId IS NULL)
    GROUP BY p.Name, MONTH(t.Deadline), YEAR(t.Deadline)
GO
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
