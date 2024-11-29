USE DBLWD;
GO

--EXECUTE prc_GetProductsList;

--EXECUTE prc_GetProductsList @CategoryID = 1;

--EXECUTE prc_GetProductsList @SearchTerm = 'диск';

--EXECUTE prc_GetProductsList 
--    @MinPrice = 1000.00,
--    @MaxPrice = 5000.00;

--EXECUTE prc_GetProductsList @InStock = 1;

--EXECUTE prc_GetProductsList
--	@CategoryID = 1,
--    @MinPrice = 15.00,
--    @InStock = 1,
--    @SearchTerm = 'S'
