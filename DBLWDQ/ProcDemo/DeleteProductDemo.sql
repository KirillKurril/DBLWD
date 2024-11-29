USE DBLWD;
GO

EXECUTE prc_DeleteProduct @ProductID = 1;

EXECUTE prc_DeleteProduct @ProductID = NULL;
