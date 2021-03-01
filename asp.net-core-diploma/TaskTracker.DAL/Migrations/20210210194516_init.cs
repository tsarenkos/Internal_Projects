using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTracker.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    FileName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.UniqueConstraint("AK_Tags_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TaskTrackerUser",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTrackerUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TaskСategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskСategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInTaskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInTaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TargetDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    IsRepeating = table.Column<bool>(nullable: false),
                    TaskСategoryId = table.Column<int>(nullable: true),
                    TaskPriorityId = table.Column<int>(nullable: true),
                    ParentTaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTasks_MyTasks_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTasks_Priorities_TaskPriorityId",
                        column: x => x.TaskPriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MyTasks_TaskСategories_TaskСategoryId",
                        column: x => x.TaskСategoryId,
                        principalTable: "TaskСategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: false),
                    MyTaskId = table.Column<int>(nullable: false),
                    DateOfSending = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_MyTasks_MyTaskId",
                        column: x => x.MyTaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepeatingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PeriodCode = table.Column<int>(nullable: false),
                    Multiplier = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepeatingTasks_MyTasks_Id",
                        column: x => x.Id,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepeatingTasks_PeriodTypes_PeriodCode",
                        column: x => x.PeriodCode,
                        principalTable: "PeriodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskEditGrants",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false),
                    FriendId = table.Column<string>(maxLength: 450, nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    IsGranted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEditGrants", x => new { x.TaskId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_TaskEditGrants_TaskTrackerUser_FriendId",
                        column: x => x.FriendId,
                        principalTable: "TaskTrackerUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEditGrants_MyTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TasksFiles",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksFiles", x => new { x.TaskId, x.FileId });
                    table.ForeignKey(
                        name: "FK_TasksFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasksFiles_MyTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    MyTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTags", x => new { x.MyTaskId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TaskTags_MyTasks_MyTaskId",
                        column: x => x.MyTaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersInTasks",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    MyTaskId = table.Column<int>(nullable: false),
                    UserInTaskTypeCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInTasks", x => new { x.UserId, x.MyTaskId });
                    table.ForeignKey(
                        name: "FK_UsersInTasks_MyTasks_MyTaskId",
                        column: x => x.MyTaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersInTasks_TaskTrackerUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TaskTrackerUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersInTasks_UserInTaskTypes_UserInTaskTypeCode",
                        column: x => x.UserInTaskTypeCode,
                        principalTable: "UserInTaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUsers", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUsers_TaskTrackerUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TaskTrackerUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "ContentType", "FileName" },
                values: new object[,]
                {
                    { 1, "image/jpeg", "crabsburger.jpg" },
                    { 2, "image/jpeg", "manycrabsburgers.jpg" }
                });

            migrationBuilder.InsertData(
                table: "PeriodTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ежедневно" },
                    { 2, "еженедельно" },
                    { 3, "ежемесячно" },
                    { 4, "ежегодно" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "высокий" },
                    { 4, "критический" },
                    { 1, "обычный" },
                    { 2, "низкий" }
                });

            migrationBuilder.InsertData(
                table: "TaskTrackerUser",
                column: "UserId",
                values: new object[]
                {
                    "2",
                    "3"
                });

            migrationBuilder.InsertData(
                table: "TaskСategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "home" },
                    { 2, "личная" },
                    { 3, "обучение" },
                    { 4, "работа" },
                    { 5, "бизнес" },
                    { 6, "прочие" }
                });

            migrationBuilder.InsertData(
                table: "UserInTaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "владелец" },
                    { 2, "друг" }
                });

            migrationBuilder.InsertData(
                table: "MyTasks",
                columns: new[] { "Id", "Details", "EndDate", "IsRepeating", "Name", "ParentTaskId", "StartDate", "TargetDate", "TaskPriorityId", "TaskСategoryId" },
                values: new object[,]
                {
                    { 1, "Диплом", null, true, "Встреча по Zoom", null, new DateTime(2021, 2, 4, 20, 56, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 4, 23, 56, 0, 0, DateTimeKind.Unspecified), 4, 1 },
                    { 7, "Тема - потоки", null, false, "Занятие по ASP.NET Core", null, new DateTime(2021, 2, 7, 18, 30, 58, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 7, 21, 30, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 3, "нужно покрасить стены", null, false, "Купить краску", null, new DateTime(2021, 2, 6, 2, 56, 22, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 4, "Для ремонта", null, true, "Покрасить стены", null, new DateTime(2021, 2, 6, 2, 57, 22, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 5, "test2", null, true, "task2", null, new DateTime(2021, 2, 7, 21, 2, 58, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 9, "Печь Крабсбургеры", null, false, "Задача Боба", null, new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4 },
                    { 10, "Печь много Крабсбургеров", null, true, "Задача Боба 2", null, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4 },
                    { 2, "Поработать с документами", null, true, "Съездить в офис", null, new DateTime(2021, 2, 5, 14, 55, 44, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "MyTasks",
                columns: new[] { "Id", "Details", "EndDate", "IsRepeating", "Name", "ParentTaskId", "StartDate", "TargetDate", "TaskPriorityId", "TaskСategoryId" },
                values: new object[,]
                {
                    { 8, "test3", null, true, "task3", 5, new DateTime(2021, 2, 3, 21, 2, 58, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 6, "ASP.NET Core", null, false, "Защита диплома", 5, new DateTime(2021, 2, 6, 21, 2, 58, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "RepeatingTasks",
                columns: new[] { "Id", "Multiplier", "PeriodCode" },
                values: new object[,]
                {
                    { 1, 3, 1 },
                    { 2, 4, 1 },
                    { 5, 2, 2 },
                    { 4, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "TasksFiles",
                columns: new[] { "TaskId", "FileId" },
                values: new object[,]
                {
                    { 9, 1 },
                    { 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "UsersInTasks",
                columns: new[] { "UserId", "MyTaskId", "UserInTaskTypeCode" },
                values: new object[,]
                {
                    { "2", 4, 1 },
                    { "2", 3, 1 },
                    { "2", 7, 1 },
                    { "2", 1, 1 },
                    { "2", 5, 1 },
                    { "2", 9, 2 },
                    { "3", 9, 1 },
                    { "3", 10, 1 },
                    { "2", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "UsersInTasks",
                columns: new[] { "UserId", "MyTaskId", "UserInTaskTypeCode" },
                values: new object[] { "2", 6, 1 });

            migrationBuilder.InsertData(
                table: "UsersInTasks",
                columns: new[] { "UserId", "MyTaskId", "UserInTaskTypeCode" },
                values: new object[] { "2", 8, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_ParentTaskId",
                table: "MyTasks",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_TaskPriorityId",
                table: "MyTasks",
                column: "TaskPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_TaskСategoryId",
                table: "MyTasks",
                column: "TaskСategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MyTaskId",
                table: "Notifications",
                column: "MyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_UserId",
                table: "NotificationUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepeatingTasks_PeriodCode",
                table: "RepeatingTasks",
                column: "PeriodCode");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEditGrants_FriendId",
                table: "TaskEditGrants",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksFiles_FileId",
                table: "TasksFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTags_TagId",
                table: "TaskTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInTasks_MyTaskId",
                table: "UsersInTasks",
                column: "MyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInTasks_UserInTaskTypeCode",
                table: "UsersInTasks",
                column: "UserInTaskTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "RepeatingTasks");

            migrationBuilder.DropTable(
                name: "TaskEditGrants");

            migrationBuilder.DropTable(
                name: "TasksFiles");

            migrationBuilder.DropTable(
                name: "TaskTags");

            migrationBuilder.DropTable(
                name: "UsersInTasks");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PeriodTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TaskTrackerUser");

            migrationBuilder.DropTable(
                name: "UserInTaskTypes");

            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "TaskСategories");
        }
    }
}
