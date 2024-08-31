USE [SPECIMEN_TRACKING]
GO
/****** Object:  StoredProcedure [dbo].[SYS_FUNCTIONS_SP]    Script Date: 31/08/2024 3:47:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SYS_FUNCTIONS_SP]
(
@ACTION nvarchar(50) = null,
@USERID float = null,
@SYSTEM nvarchar(100) = null,
@PARENT nvarchar(100) = null
)
AS
BEGIN

IF @ACTION='GET_SYSTEMS'
BEGIN

	SELECT        ID, ProjectID, LicenseKey, SystemID, SystemName, Color, Icon, SysURL
	FROM            LicenseSys ORDER BY SystemID


END

ELSE IF @ACTION = 'GET_FUNCTIONS'
BEGIN
IF @SYSTEM = 'Home'
BEGIN
	SELECT        FUNCTIONS.Parent AS Parent, 
	FUNCTIONS.SequenceID AS SequenceID,FUNCTIONS.NavigationURL + '?menu=' + FUNCTIONS.FunctionName AS NavigationURL, FUNCTIONS.SystemID AS SystemID, FUNCTIONS.SystemName AS SystemName, FUNCTIONS.FunctionName, FUNCTIONS.LevelID, 
							 FUNCTIONS.Active, LicenseSys_1.Icon
	FROM            LicenseSys AS LicenseSys_1 INNER JOIN
							 FUNCTIONS ON LicenseSys_1.SystemID = FUNCTIONS.SystemID AND LicenseSys_1.SystemName = FUNCTIONS.SystemName
WHERE FUNCTIONS.Parent  IS NULL ORDER BY SequenceID 
END
ELSE
BEGIN
SELECT Functions.FunctionName, Functions.Parent, Functions.NavigationURL, Functions.LevelID, Functions.SequenceID,  Functions.SystemName
FROM     Functions 
WHERE  (Functions.Active = 1) AND   (Functions.Parent = @PARENT) 
ORDER BY Functions.SequenceID
END


END

ELSE IF @ACTION ='GET_SETUP_SYSTEM'
BEGIN
SELECT        FUNCTIONS.Parent AS Parent, FUNCTIONS.SequenceID AS SequenceID, FUNCTIONS.NavigationURL AS NavigationURL, FUNCTIONS.SystemID AS SystemID, FUNCTIONS.SystemName AS SystemName, FUNCTIONS.FunctionName, FUNCTIONS.LevelID,FUNCTIONS.Active,FUNCTIONS.Color,FUNCTIONS.Icon
FROM            FUNCTIONS 
WHERE FUNCTIONS.Parent =@PARENT ORDER BY SequenceID 
END
END

