USE DBLWD;
GO

CREATE PROCEDURE prc_DeleteProduct
    @ProductID INT
AS
BEGIN 
    DELETE p
    FROM Product P
    WHERE p.id = @ProductID;

    IF @@ROWCOUNT > 0 --кол-во строк, затронутых последней операцией
        SELECT 'Product successfully deleted' AS Result;
    ELSE
        SELECT 'No product was deleted' AS Result;
END

