USE DBLWD;
GO

EXECUTE prc_GetProductById @ProductID = 1;

EXECUTE prc_GetProductById @ProductID = 2;

EXECUTE prc_GetProductById @ProductID = NULL;
