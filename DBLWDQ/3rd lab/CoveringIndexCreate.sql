CREATE NONCLUSTERED INDEX idx_users_covering
ON [User] ([username])
INCLUDE ([first_name], [last_name], [email])