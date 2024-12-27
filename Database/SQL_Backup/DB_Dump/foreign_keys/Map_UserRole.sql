ALTER TABLE [dbo].[Map_UserRole] WITH CHECK ADD 
   FOREIGN KEY([RoleId]) REFERENCES [dbo].[M_Role] ([RoleId])

GO
ALTER TABLE [dbo].[Map_UserRole] WITH CHECK ADD 
   FOREIGN KEY([UDID]) REFERENCES [dbo].[UserDetails] ([UDID])

GO
