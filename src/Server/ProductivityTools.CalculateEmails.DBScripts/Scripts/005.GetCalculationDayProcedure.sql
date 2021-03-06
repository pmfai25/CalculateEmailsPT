﻿CREATE PROCEDURE [outlook].[GetCalculationDay]
	@startDate DATE,
	@endDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS (SELECT 1 FROM [outlook].CalculateEmails WHERE [Date]=@startDate)
		BEGIN
			INSERT INTO [outlook].CalculateEmails ([Date])
			VALUES (@startDate)
		END
	SELECT [CalculateEmailsId],[Date],[MailCountAdd],[MailCountSent],[MailCountProcessed]
	,[TaskCountAdded],[TaskCountRemoved],[TaskCountFinished]
	FROM [outlook].[CalculateEmails]
	WHERE [Date] BETWEEN @startDate AND @endDate

END