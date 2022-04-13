
IF OBJECT_ID('Feedback', 'U') IS NOT NULL
DROP TABLE Feedback;

IF OBJECT_ID('FeedbackCategory', 'U') IS NOT NULL
DROP TABLE FeedbackCategory;




CREATE TABLE FeedbackCategory
(
	FeedbackCategoryId UNIQUEIDENTIFIER NOT NULL,
	FeedbackCategoryName NVARCHAR(50)

	CONSTRAINT PK_FeedbackCategory PRIMARY KEY (FeedbackCategoryId)
);

CREATE TABLE Feedback
(
	FeedbackId UNIQUEIDENTIFIER NOT NULL,
	FeedbackCategoryId UNIQUEIDENTIFIER NOT NULL,
	ObjectStoreCheckId UNIQUEIDENTIFIER NOT NULL,
	[Text] NVARCHAR(255) NOT NULL,
	[Date] DATE NOT NULL,
	Resloved BIT NOT NULL

	CONSTRAINT PK_Feedback PRIMARY KEY (FeedbackId),
	CONSTRAINT FK_Feedback_FeedbackCategory FOREIGN KEY (FeedbackCategoryId) REFERENCES FeedbackCategory(FeedbackCategoryId) ON DELETE CASCADE,
	CONSTRAINT CHK_Feedback_Date CHECK ([Date] >= GETDATE())

)


