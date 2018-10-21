--SELECT * FROM sys.fn_helpcollations() WHERE [name] LIKE N'ukrain%';
--GO

CREATE DATABASE morphDB
COLLATE Ukrainian_100_CI_AI;
GO


use morphDB;
GO


-------------------------------------------------------------------------
-- Main Tables

-------------------------------------------------------------------------
-- Table ��'����

IF OBJECT_ID('[MorphObjects]') is null
CREATE TABLE [MorphObjects]
(
	id_object int IDENTITY NOT NULL -- id_��'����
	, [name] nvarchar(4000) NOT NULL -- ����� ��'����
	, characteristic nvarchar(4000)-- ��������������
	, id_classification int NULL -- id �����������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [MorphObjects]
ADD CONSTRAINT PK_id_object
PRIMARY KEY (id_object);
GO

-- Add for table a foreign key on column id_classification
ALTER TABLE [MorphObjects]
ADD CONSTRAINT FK_MorphObjects_Classifications_id_classification --this table , reference table, column
FOREIGN KEY (id_classification) REFERENCES [Classifications](id_classification)
GO


-------------------------------------------------------------------------
-- Table �������

IF OBJECT_ID('[Functions]') is null
CREATE TABLE [Functions]
(
	id_function int IDENTITY NOT NULL -- id_�������
	, [name] nvarchar(4000)	NOT NULL --����� �������
	, characteristics nvarchar(4000) NULL --�������������� �������
	, [weight] decimal NULL -- ������� ���������� �������, ��� decimal(7,4) ����� �����: 999,9999
	, id_classification int NULL -- id ������������
)
ELSE 
	print 'table exists'
GO


-- Add for table a primary key
ALTER TABLE [Functions]
ADD CONSTRAINT PK_id_function
PRIMARY KEY (id_function);
GO

-- Add for table a foreign key on column id_classification
ALTER TABLE [Functions]
ADD CONSTRAINT FK_Functions_Classifications_id_classification --this table , reference table, column
FOREIGN KEY (id_classification) REFERENCES [Classifications](id_classification)
GO



-------------------------------------------------------------------------
-- Table ������ ������

IF OBJECT_ID('[Solutions]') is null
CREATE TABLE [Solutions]
(
    id_solution int IDENTITY NOT NULL -- id_������
    , [name] nvarchar(4000) NOT NULL -- ����� ������
	, characteristic nvarchar(4000) NULL -- ��������������
	, bibliographic_description nvarchar(4000) NULL -- ������������� ���� ������
	--, id_parentSolution int NULL -- � id_������������ (��������) ������
	, [weight] decimal NULL -- ������� ���������� ������, ��� decimal(7,4) ����� �����: 999,9999
	, id_classification int NULL -- id ������������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [Solutions]
ADD CONSTRAINT PK_id_solution
PRIMARY KEY (id_solution);
GO

-- Add for table a foreign key on column id_classification
ALTER TABLE [Solutions]
ADD CONSTRAINT FK_Solutions_Classifications_id_classification --this table , reference table, column
FOREIGN KEY (id_classification) REFERENCES [Classifications](id_classification)
GO

-------------------------------------------------------------------------
-- Table ����������� 

IF OBJECT_ID('[Modifications]') is null
CREATE TABLE [Modifications]
(
    id_modification int IDENTITY NOT NULL -- id_������
    , [name] nvarchar(4000) NOT NULL -- ����� ������
	, characteristic nvarchar(4000) NULL -- ��������������
	, bibliographic_description nvarchar(4000) NULL -- ������������� ���� ������
	--(�� �����!!!), [weight] decimal NULL -- ������� ���������� �����������, ��� decimal(7,4) ����� �����: 999,9999
	, id_classification int NULL -- id ������������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [Modifications]
ADD CONSTRAINT PK_id_modification
PRIMARY KEY (id_modification);
GO

-- Add for table a foreign key on column id_classification
ALTER TABLE [Modifications]
ADD CONSTRAINT FK_Modifications_Classifications_id_classification --this table, reference table, column
FOREIGN KEY (id_classification) REFERENCES [Classifications](id_classification)
GO

-------------------------------------------------------------------------
-- Table ������������ (�������� �������, ��� �������� ������������� ��'����, �������, ������ �� �� ������������ 
-- �� ��������� �� 򳺿 �� ���� ����� ������� �������� 

IF OBJECT_ID( '[Classifications]') is null
CREATE TABLE [Classifications]
(
	id_classification int IDENTITY NOT NULL -- id_������������
	, [name] nvarchar(4000) NOT NULL -- ����� ������������
	, [description] nvarchar(4000) NULL -- ���� 
	, id_parentClassification int NULL -- � ����������� (�������) ������������

)
ELSE
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [Classifications]
ADD CONSTRAINT PK_id_classification
PRIMARY KEY (id_classification);
GO

-- Add for table a foreign key on column id_parentClassifications
ALTER TABLE [Classifications]
ADD CONSTRAINT FK_Classifications_Classifications_id_parentClassification --this table , reference table, column
FOREIGN KEY (id_parentClassification) REFERENCES [Classifications](id_classification)
GO

-------------------------------------------------------------------------
-- Table ���

IF OBJECT_ID('[Goals]') is null
CREATE TABLE [Goals]
(
    id_goal int IDENTITY NOT NULL -- id_���
    , [name] nvarchar(4000) NOT NULL -- ����� ���
	, characteristic nvarchar(4000) NULL -- ��������������
	, [weight] decimal NULL -- ������� ���������,  ��� decimal(7,4) ����� �����: 999,9999
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [Goals]
ADD CONSTRAINT PK_id_goal
PRIMARY KEY (id_goal);
GO


-------------------------------------------------------------------------
-- Table ��������� �����

IF OBJECT_ID('[ParametersGoals]') is null
CREATE TABLE [ParametersGoals]
(
    id_parameter int IDENTITY NOT NULL -- id_��������� ���
    , [name] nvarchar(4000) NOT NULL -- ����� ���������
	, unit nvarchar(20) NULL -- ������� �����
	, [avg] decimal NULL -- �������� ������ �������� ���������, ��� decimal(7,4) ����� �����: 999,9999
	, id_goal int NULL -- id_���, �� ��� ���������� ����� ��������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [ParametersGoals]
ADD CONSTRAINT PK_id_parameter
PRIMARY KEY (id_parameter);
GO

-- Add for table a foreign key on column id_goal
ALTER TABLE [ParametersGoals]
ADD CONSTRAINT FK_ParametersGoals_Goals_id_goal --this table , reference table, column 
FOREIGN KEY (id_goal) REFERENCES [Goals](id_goal)
ON UPDATE CASCADE ON DELETE NO ACTION 
GO


-------------------------------------------------------------------------
-- ������� ��� ���������� ��������� 
-------------------------------------------------------------------------
-- ������� ������������ ��'����

IF OBJECT_ID('[FunctionsOfMorphObjects]') is null
CREATE TABLE [FunctionsOfMorphObjects]
(
    id_object int  NOT NULL -- id_��'����
	, id_function int NOT NULL -- id_�������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [FunctionsOfMorphObjects]
ADD CONSTRAINT PK_Composite_id_objectFunction
PRIMARY KEY (id_object, id_function);
GO

-- Add for table a foreign key on column id_object
ALTER TABLE [FunctionsOfMorphObjects]
ADD CONSTRAINT FK_FunctionsOfMorphObjects_MorphObjects_Id_object --this table , reference table, column
FOREIGN KEY (id_object) REFERENCES [MorphObjects](id_object)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Add for table a foreign key on column id_function
ALTER TABLE [FunctionsOfMorphObjects]
ADD CONSTRAINT FK_FunctionsOfMorphObjects_Functions_Id_function --this table , reference table, column
FOREIGN KEY (id_function) REFERENCES [Functions](id_function)
ON UPDATE CASCADE ON DELETE CASCADE;
GO


-------------------------------------------------------------------------
-- Table ������ ������ ��� ������� 

IF OBJECT_ID('[SolutionsOfFunctions]') is null
CREATE TABLE [SolutionsOfFunctions]
(
    id_solution int  NOT NULL -- id_������
	, id_function int NOT NULL -- id_�������
	, rating decimal NULL --������� ���������� ������ ������ ��� �������
)
ELSE 
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [SolutionsOfFunctions]
ADD CONSTRAINT PK_Composite_id_solutionFunction
PRIMARY KEY (id_solution, id_function);
GO

-- Add for table a foreign key on column id_solution
ALTER TABLE [SolutionsOfFunctions]
ADD CONSTRAINT FK_SolutionsOfFunctions_Solutions_Id_solution --this table , reference table, column
FOREIGN KEY (id_solution) REFERENCES [Solutions](id_solution)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Add for table a foreign key on column id_function
ALTER TABLE [SolutionsOfFunctions]
ADD CONSTRAINT FK_SolutionsOfFunctions_Functions_Id_function --this table , reference table, column
FOREIGN KEY (id_function) REFERENCES [Functions](id_function)
ON UPDATE CASCADE ON DELETE CASCADE;
GO


-------------------------------------------------------------------------
-- Table ����������� ��� ��� ���. �����

IF OBJECT_ID('[ParametersGoalsForSolutions]') is null
CREATE TABLE [ParametersGoalsForSolutions]
(
	id_parameter int NOT NULL -- id ��������� ���
	, id_solution int NOT NULL -- id ������
	, rating decimal NULL -- ������� ������������ ������ ������ ��� ��������� ���
)
else
	print 'table exists'
GO

-- Add for table a primary key
ALTER TABLE [ParametersGoalsForSolutions]
ADD CONSTRAINT PK_Composite_id_parameterSolution
PRIMARY KEY (id_parameter, id_solution);
GO

-- Add for table a foreign key on column id_parameter
ALTER TABLE [ParametersGoalsForSolutions]
ADD CONSTRAINT FK_ParametersGoalsForSolutions_ParametersGoals_Id_parameter--this table , reference table, column
FOREIGN KEY (id_parameter) REFERENCES [ParametersGoals](id_parameter)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Add for table a foreign key on column id_solution
ALTER TABLE [ParametersGoalsForSolutions]
ADD CONSTRAINT FK_ParametersGoalsForSolutions_Solutions_Id_solution --this table , reference table, column
FOREIGN KEY (id_solution) REFERENCES [Solutions](id_solution)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-------------------------------------------------------------------------
-- Table ����������� ��� ��� �����������

IF OBJECT_ID('[ParametersGoalsForModifications]') is null
CREATE TABLE [ParametersGoalsForModifications]
(
	id_parameter int NOT NULL -- id ��������� ���
	, id_modification int  NOT NULL -- id �����������
	, rating decimal NULL -- ������� ������������ ������ ���������� ��� ��������� ���
)
ELSE 
	print 'table exists'
GO


-- Add for table a primary key
ALTER TABLE [ParametersGoalsForModifications]
ADD CONSTRAINT PK_Composite_id_parameterModification
PRIMARY KEY (id_parameter, id_modification);
GO

-- Add for table a foreign key on column id_parameter
ALTER TABLE [ParametersGoalsForModifications]
ADD CONSTRAINT FK_ParametersGoalsForModifications_ParametersGoals_Id_parameter --this table , reference table, column
FOREIGN KEY (id_parameter) REFERENCES [ParametersGoals](id_parameter)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Add for table a foreign key on column id_modification
ALTER TABLE [ParametersGoalsForModifications]
ADD CONSTRAINT FK_ParametersGoalsForModifications_Modifications_Id_modification --this table , reference table, column
FOREIGN KEY (id_modification) REFERENCES [Modifications] (id_modification)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-------------------------------------------------------------------------
-- Table ����������� ���. �����

IF OBJECT_ID('[ModificationsOfSolutions]') is null
CREATE TABLE [ModificationsOfSolutions]
(
	id_solution	int NOT NULL -- id ������
	, id_modification int NOT NULL -- id ����������� ��� ������
)
ELSE 
	print 'table exists'
GO

ALTER TABLE [ModificationsOfSolutions]
ADD CONSTRAINT PK_Composite_id_solutionModification
PRIMARY KEY (id_solution, id_modification);
GO

-- Add for table a foreign key on column id_function
ALTER TABLE [ModificationsOfSolutions]
ADD CONSTRAINT FK_ModificationsOfSolutions_Solutions_Id_solution --this table , reference table, column
FOREIGN KEY (id_solution) REFERENCES [Solutions] (id_solution)
ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Add for table a foreign key on column id_object
ALTER TABLE [ModificationsOfSolutions]
ADD CONSTRAINT FK_ModificationsOfSolutions_Modifications_Id_modification --this table , reference table, column
FOREIGN KEY (id_modification) REFERENCES [Modifications] (id_modification)
ON UPDATE CASCADE ON DELETE CASCADE;
GO




