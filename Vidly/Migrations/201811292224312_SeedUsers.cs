namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'acf923e0-a054-4614-b6ae-4e94330bcead', N'admin@vidly.com', 0, N'AKHjGjtvNvzbRtHJ2NW0ookWqIrYoOVGhuBZVYmqsiy7YFFrz4FEl32x/pL0WcCvgw==', N'ecca7170-5909-4799-9a72-de1634020e0a', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ad0923af-aa1a-43bc-bd3b-792404d1b8aa', N'guest@vidly.com', 0, N'AApkw4QgKgpbCpuISCsFzPY0uT9Pn1TaBoUIgYr7yeGG+QVqgJpYs20LZHoIeL7ZFw==', N'16bb66f9-100a-4006-aae3-dfe22ed3c512', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1cb7a8b2-b57e-43bc-8221-5a25c4544600', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'acf923e0-a054-4614-b6ae-4e94330bcead', N'1cb7a8b2-b57e-43bc-8221-5a25c4544600')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
