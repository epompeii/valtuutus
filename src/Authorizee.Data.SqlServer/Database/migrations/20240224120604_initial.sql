-- Create "attributes" table
CREATE TABLE [attributes] ([id] bigint IDENTITY (1, 1) NOT NULL, [entity_type] varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [entity_id] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [attribute] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [value] varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [created_tx_id] bigint NOT NULL, CONSTRAINT [PK_attributes] PRIMARY KEY CLUSTERED ([id] ASC));
CREATE NONCLUSTERED INDEX [idx_attributes_entity_id_entity_type_attribute] ON [dbo].[attributes]
       (
       	[entity_id] ASC,
       	[entity_type] ASC,
       	[attribute] ASC
       )
       INCLUDE([value]);
              
       CREATE NONCLUSTERED INDEX [idx_attributes_attribute_entity_type_entity_id] ON [dbo].[attributes]
       (
       	[attribute] ASC,
       	[entity_type] ASC
       )
       INCLUDE([entity_id],[value]);
              
              
       -- Create "relation_tuples" table
CREATE TABLE [relation_tuples] ([id] bigint IDENTITY (1, 1) NOT NULL, [entity_type] varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [entity_id] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [relation] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [subject_type] varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [subject_id] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, [subject_relation] varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, [created_tx_id] bigint NOT NULL, CONSTRAINT [PK_relation_tuples] PRIMARY KEY CLUSTERED ([id] ASC));

CREATE NONCLUSTERED INDEX [idx_relation_tuples_entity_type_relation_subject_type_subject_id] ON [dbo].[relation_tuples]
       (
       	[entity_type] ASC,
       	[relation] ASC,
       	[subject_type] ASC,
       	[subject_id] ASC
       );
              
   CREATE NONCLUSTERED INDEX [idx_relation_tuples_entity_type_entity_id_relation] ON [dbo].[relation_tuples]
   (
    [entity_type] ASC,
    [entity_id] ASC,
    [relation] ASC
   )
   INCLUDE([subject_type],[subject_id],[subject_relation]);
          
   CREATE NONCLUSTERED INDEX [idx_relation_tuples_relation_subject_type_subject_id_entity_type] ON [dbo].[relation_tuples]
   (
    [relation] ASC,
    [subject_type] ASC,
    [subject_id] ASC,
    [entity_type] ASC
   )
   INCLUDE([id],[entity_id],[subject_relation]);       
          
   CREATE NONCLUSTERED INDEX [idx_relation_tuples_relation_entity_type_entity_id] ON [dbo].[relation_tuples]
   (
    [relation] ASC,
    [entity_type] ASC,
    [entity_id] ASC
   )
   INCLUDE([subject_type],[subject_id],[subject_relation]);
          
   CREATE NONCLUSTERED INDEX [idx_relation_tuples_entity_type_relation_subject_type_subject_id_id] ON [dbo].[relation_tuples]
   (
    [entity_type] ASC,
    [relation] ASC,
    [subject_type] ASC,
    [subject_id] ASC,
    [id] ASC
   )
   INCLUDE([entity_id],[subject_relation]);       
       
       -- Create custom type to be used as a list of ids - entity or subject
CREATE TYPE TVP_ListIds AS TABLE
    (
    [id] [VARCHAR](64) NOT NULL,
    index tvp_id (id)
    );

CREATE TABLE [transactions] ([id] bigint NOT NULL, [created_at] datetime2(7) NOT NULL, CONSTRAINT [PK_transactions] PRIMARY KEY CLUSTERED ([id] ASC));    