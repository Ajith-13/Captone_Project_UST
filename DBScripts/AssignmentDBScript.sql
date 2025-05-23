USE [CaptoneProject_AssignmentDB]
GO
SET IDENTITY_INSERT [dbo].[AssignmentQuestions] ON 

INSERT [dbo].[AssignmentQuestions] ([Id], [Title], [Description], [UploadDate], [DueDate], [TotalMarks], [CourseId], [ModuleId], [TrainerId]) VALUES (1015, N'Assignment 1', N'Define CNN in Deep Learning?', CAST(N'2025-04-26T07:06:06.6249210' AS DateTime2), CAST(N'2025-04-27T00:00:00.0000000' AS DateTime2), 200, 2003, 1012, N'cee7d31a-1e8f-48fd-a149-14e7388c940c')
INSERT [dbo].[AssignmentQuestions] ([Id], [Title], [Description], [UploadDate], [DueDate], [TotalMarks], [CourseId], [ModuleId], [TrainerId]) VALUES (2004, N'Intro ', N'What is data engineering in short', CAST(N'2025-04-29T18:53:37.9616880' AS DateTime2), CAST(N'2025-05-01T00:00:00.0000000' AS DateTime2), 20, 3003, 2006, N'cee7d31a-1e8f-48fd-a149-14e7388c940c')
INSERT [dbo].[AssignmentQuestions] ([Id], [Title], [Description], [UploadDate], [DueDate], [TotalMarks], [CourseId], [ModuleId], [TrainerId]) VALUES (2005, N'Beginners guide', N'Make a beginners guide for the Data science course.', CAST(N'2025-04-30T05:34:02.9859568' AS DateTime2), CAST(N'2025-05-01T00:00:00.0000000' AS DateTime2), 20, 3003, 2006, N'cee7d31a-1e8f-48fd-a149-14e7388c940c')
INSERT [dbo].[AssignmentQuestions] ([Id], [Title], [Description], [UploadDate], [DueDate], [TotalMarks], [CourseId], [ModuleId], [TrainerId]) VALUES (2006, N'Assignment 1', N'Why c# is important?', CAST(N'2025-05-02T04:11:02.9519589' AS DateTime2), CAST(N'2025-05-03T00:00:00.0000000' AS DateTime2), 50, 3005, 2007, N'cee7d31a-1e8f-48fd-a149-14e7388c940c')
INSERT [dbo].[AssignmentQuestions] ([Id], [Title], [Description], [UploadDate], [DueDate], [TotalMarks], [CourseId], [ModuleId], [TrainerId]) VALUES (2007, N'Assignment 1', N'Importance of python', CAST(N'2025-05-02T04:51:24.2022936' AS DateTime2), CAST(N'2025-05-03T00:00:00.0000000' AS DateTime2), 20, 3006, 2008, N'cee7d31a-1e8f-48fd-a149-14e7388c940c')
SET IDENTITY_INSERT [dbo].[AssignmentQuestions] OFF
GO
SET IDENTITY_INSERT [dbo].[Assignments] ON 

INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (1005, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/5275a166-16a4-4593-beea-31c545ae7d9f_resume-sample.pdf', CAST(N'2025-04-27T14:07:15.9919886' AS DateTime2), 18, 1015)
INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (1006, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/2a1b7480-7aff-4513-9557-10d46eabc959_resume-sample.pdf', CAST(N'2025-04-27T14:16:09.5770285' AS DateTime2), 23, 1015)
INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (2005, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/b51bf443-232b-44da-a2eb-3b261c399b3c_resume-sample.pdf', CAST(N'2025-04-29T18:58:18.8573896' AS DateTime2), 0, 2004)
INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (2006, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/632495bf-f4ef-41ea-9a20-615d54e159a9_AssignmentAnswer-sample.pdf', CAST(N'2025-04-30T05:35:05.8785430' AS DateTime2), 12, 2005)
INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (2007, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/c8de59e7-e26c-4819-8fe8-2a901cef5fcf_AssignmentAnswer-sample.pdf', CAST(N'2025-05-02T04:11:56.0372372' AS DateTime2), 40, 2006)
INSERT [dbo].[Assignments] ([Id], [LearnerId], [FilePath], [SubmittedAt], [MarksScored], [AssignmentQuestionId]) VALUES (2008, N'f186c1eb-3692-4bd7-9e60-a5de6a400f67', N'/assignment/7bb68ae9-3de6-4bdc-b7e4-99a4cb65dc84_AssignmentAnswer-sample.pdf', CAST(N'2025-05-02T04:54:59.6540278' AS DateTime2), 18, 2007)
SET IDENTITY_INSERT [dbo].[Assignments] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250407173147_firstmigration', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417083549_streakandleaderboard', N'9.0.3')
GO
SET IDENTITY_INSERT [dbo].[Leaderboards] ON 

INSERT [dbo].[Leaderboards] ([Id], [LearnerId], [TotalMarks], [StreakPoints], [AssignmentMarks], [Rank]) VALUES (1, N'1', 24, 1, 23, 1)
SET IDENTITY_INSERT [dbo].[Leaderboards] OFF
GO
SET IDENTITY_INSERT [dbo].[Streaks] ON 

INSERT [dbo].[Streaks] ([Id], [LearnerId], [LastInteractionDate], [CurrentStreak]) VALUES (1, N'1', CAST(N'2025-04-17T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Streaks] OFF
GO
