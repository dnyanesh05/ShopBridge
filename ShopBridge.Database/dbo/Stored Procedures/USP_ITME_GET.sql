CREATE PROCEDURE [dbo].[USP_ITEMS_GET]    
@xml as xml     
AS
BEGIN      
      
 --===================================================================          
 -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.          
 --===================================================================          
 SET NOCOUNT ON;          
 --INSERT INTO test (id,val)          
 --select 2, CAST (@xml as nvarchar(max))          
 --===================================================================          
 -- Local variables to hold exception data          
 --===================================================================          
DECLARE           
    @columnName NVARCHAR(50) = ''          
   ,@searchString NVARCHAR(100) =''          
   ,@SQL NVARCHAR(max) = ''                      
   ,@entityKey NVARCHAR(500) = ''          
   ,@PageNumber NVARCHAR(100) = 0          
   ,@PageSize NVARCHAR(100) = 0          
   ,@SortByField NVARCHAR(100) = ''          
   ,@OrderBy NVARCHAR(100) = ''          
   ,@OrderByCriteria NVARCHAR(100) = ''          
   ,@totalNumberOfRecords NVARCHAR(100)          
   ,@whereClause NVARCHAR(MAX) = ''          
   ,@SQLForTotalRecords NVARCHAR(MAX) = ''          
   ,@selectQuery NVARCHAR(MAX) =''          
   ,@selectStatement NVARCHAR(MAX) =''          
   ,@search NVARCHAR(MAX) = ''          
   ,@tableVariableSql NVARCHAR(MAX) = ''    
   ,@ReturnConditionID INT = 0
   ,@SQLCountAll NVARCHAR(MAX)      
   ,@ParmDefinition nvarchar(100)= N'@TotalRows INT OUTPUT';       
         
      
    BEGIN TRY      
      
 SELECT           
  @columnName =  entity.value('ColumnName[1]','nvarchar(50)')            
    ,@searchString = entity.value('SearchString[1]','nvarchar(100)')            
    ,@entityKey =entity.value('EntityKey[1]', 'nvarchar(150)')      
  FROM @xml.nodes('/FilterCriteria/ProcessFields')AS model(entity) OPTION(OPTIMIZE FOR (@xml = NULL));        
      
   SELECT @pageNumber =  entity.value('PageNumber[1]','nvarchar(100)') ,         
    @pageSize = entity.value('PageSize[1]','nvarchar(100)')  ,       
    @sortByField = entity.value('SortByField[1]','nvarchar(500)'),         
    @orderBy = entity.value('OrderBy[1]','nvarchar(100)')   ,      
    @search = entity.value('Search[1]','nvarchar(max)')         
  FROM @xml.nodes('/FilterCriteria')AS model(entity) OPTION(OPTIMIZE FOR (@xml = NULL));       
    
   -- Declaration of the temporary table          
   DECLARE @tempTable AS TABLE           
   (          
    ID INT IDENTITY(1,1) NOT NULL,          
    ColumnName NVARCHAR(500),          
    SearchValue NVARCHAR(500)          
   );         
        
   INSERT INTO @tempTable (ColumnName, SearchValue)          
   SELECT LTRIM(RTRIM(entity.value('Key[1]','NVARCHAR(100)'))),          
      LTRIM(RTRIM(entity.value('Value[1]','NVARCHAR(100)')))           
   FROM @xml.nodes('FilterCriteria/FilterCollection/KeyValuePair')AS model(entity) OPTION(OPTIMIZE FOR (@xml = NULL));        
    
   IF(@sortByField <> '' AND @orderBy <> '')          
   BEGIN          
    SET @OrderByCriteria = @sortByField +' '+ @orderBy + ' '          
   END          
   ELSE          
   BEGIN          
    SET @OrderByCriteria = 'Item.ItemID desc'          
   END       
       
       
  --select @entityKey  
    
  SET @SQLCountAll ='SELECT @TotalRows=COUNT(1)      
  FROM tbl_Master_Item Item          
   INNER JOIN tbl_Master_Item_Category CT ON CT.ItemCategoryID = Item.ItemCategoryID     
   WHERE Item.IsActive=1 ' 
  
    
	SET @whereClause = '';
    
    IF EXISTS(SELECT 1 from @tempTable)     
	BEGIN      
  
	  SELECT @whereClause += ' AND ' + ColumnName + ' LIKE N''%' + SearchValue + '%''' from @tempTable WHERE NOT ColumnName IN ('itemName', 'itemDescription', 'itemCategory', 'price');      
    
	   IF EXISTS(SELECT 1 from @tempTable WHERE ColumnName ='itemName')        
		BEGIN    
			SELECT @whereClause += ' AND Item.'+ColumnName+' LIKE N''%'+ SearchValue + '%''' from @tempTable WHERE ColumnName ='itemName';       
		END    
		 IF EXISTS(SELECT 1 from @tempTable WHERE ColumnName ='itemDescription')        
		  BEGIN    
			SELECT @whereClause += ' AND Item.'+ColumnName+' LIKE N''%'+ SearchValue + '%''' from @tempTable WHERE ColumnName ='itemDescription';       
		  END    
		IF EXISTS(SELECT 1 from @tempTable WHERE ColumnName ='itemCategory')        
		  BEGIN    
		   SELECT @whereClause += ' AND CT.'+ColumnName+' LIKE N''%'+ SearchValue + '%''' from @tempTable WHERE ColumnName ='itemCategory';
		  END   
		IF EXISTS(SELECT 1 from @tempTable WHERE ColumnName ='price')        
		  BEGIN    
			SELECT @whereClause += ' AND Item.'+ColumnName+' LIKE N''%'+ SearchValue + '%''' from @tempTable WHERE ColumnName ='price';       
		  END     
  
	END         
      
 SET @SQLCountAll = @SQLCountAll + ' ' + @whereClause      
  
EXEC sp_executesql @SQLCountAll, @ParmDefinition, @TotalRows=@totalNumberOfRecords OUTPUT      
    
     
    
	  SET @SelectStatement ='SELECT Item.ItemID,          
	  Item.ItemName,    
	  Item.ItemDescription    
	  ,CT.CategoryName AS ItemCategory,        
	  Item.Price  
	  ,'+CAST(@totalNumberOfRecords as NVARCHAR(100))+' as TotalNumberOfRecords    
       
	  FROM tbl_Master_Item Item          
		INNER JOIN tbl_Master_Item_Category CT ON CT.ItemCategoryID = Item.ItemCategoryID
	  WHERE  RMA.IsActive=1  '  ;    
   
    
 SET @SelectStatement=@SelectStatement + @whereClause +      
       ' ORDER BY ' + @OrderByCriteria +           
       ' OFFSET '+ @PageSize+' * ('+@PageNumber+' - 1) ROWS            
         FETCH NEXT '+@PageSize+' ROWS ONLY OPTION (RECOMPILE);'     
  --print @SelectStatement  
 EXEC  (@SelectStatement);      
  END TRY      
  BEGIN CATCH      
    --===================================================================          
    --Log the error using the following SP          
    --===================================================================          
    INSERT INTO tbl_Log ([Message], [Source], [ErrorLineNo], [Type], [CreatedBy], [CreatedOn])          
    VALUES( ERROR_MESSAGE(), ISNULL(ERROR_PROCEDURE(),'USP_ITEMS_GET'), ERROR_LINE(), '', 2, GETUTCDATE());          
          
 END CATCH      
END
