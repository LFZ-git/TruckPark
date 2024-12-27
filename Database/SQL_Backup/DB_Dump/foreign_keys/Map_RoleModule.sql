ALTER TABLE [dbo].[Map_RoleModule] WITH CHECK ADD 
   FOREIGN KEY([ModuleId]) REFERENCES [dbo].[M_Modules] ([ModuleId])

GO
ALTER TABLE [dbo].[Map_RoleModule] WITH CHECK ADD 
   FOREIGN KEY([ModuleId]) REFERENCES [dbo].[M_Modules] ([ModuleId])

GO
ALTER TABLE [dbo].[Map_RoleModule] WITH CHECK ADD 
   FOREIGN KEY([RoleId]) REFERENCES [dbo].[M_Role] ([RoleId])

GO
ALTER TABLE [dbo].[Map_RoleModule] WITH CHECK ADD 
   FOREIGN KEY([RoleId]) REFERENCES [dbo].[M_Role] ([RoleId])

GO
