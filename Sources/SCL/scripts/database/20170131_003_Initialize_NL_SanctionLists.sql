PRINT 'Preparing all variables..'
BEGIN /* Preparing all variables */
	DECLARE @subject_type_person int,@subject_type_enterprise int,
			@status_active int,@list_type_nl int,@dummy_id int,
			@gender_unknown int,@gender_male int,@gender_female int,@gender_not_applicable int,
			@reg_id int,@entity_id int
	SET @subject_type_person = (SELECT TOP 1 SubjectTypeId FROM sl.EnumSubjectTypes WHERE Name = 'Person')
	SET @subject_type_enterprise = (SELECT TOP 1 SubjectTypeId FROM sl.EnumSubjectTypes WHERE Name = 'Enterprise')
	SET @status_active = (SELECT TOP 1 StatusId FROM sl.EnumStatuses WHERE Name = 'Active')
	SET @list_type_nl = (SELECT TOP 1 ListTypeId FROM sl.EnumListTypes WHERE Name = 'NL')
	SET @dummy_id = -1
	SET @gender_unknown = 0
	SET @gender_male = 1
	SET @gender_female = 2
	SET @gender_not_applicable = 9
END

BEGIN TRANSACTION;
BEGIN TRY
	PRINT 'Inserting MinBuZa-2015.373100..'
    BEGIN /* MinBuZa-2015.373100 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.373100','2015-07-16','stcrt-2015-21395','https://zoek.officielebekendmakingen.nl/stcrt-2015-21395.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Musa',null,'Aşoğlu','Musa Aşoğlu',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1961,8,15,'Hendek','TUR')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END

	PRINT 'Inserting MinBuZa-2015.183268..'
	BEGIN /* MinBuZa-2015.183268 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.183268','2015-05-06','stcrt-2015-12878','https://zoek.officielebekendmakingen.nl/stcrt-2015-12878.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Abdelhakim',null,'Boukich','Abdelhakim Boukich',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1993,12,15,'Den Haag','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END

	PRINT 'Inserting MinBuZa-2015.192919..'
	BEGIN /* MinBuZa-2015.192919 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.192919','2015-05-06','stcrt-2015-12879','https://zoek.officielebekendmakingen.nl/stcrt-2015-12879.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Enes',null,'Köprülü','Enes Köprülü',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1995,04,26,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END

	PRINT 'Inserting MinBuZa-2015.133073..'
	BEGIN /* MinBuZa-2015.133073 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.133073','2015-03-23','stcrt-2015-8459','https://zoek.officielebekendmakingen.nl/stcrt-2015-8459.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Maher',null,'Hattabe','Maher Hattabe',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1995,03,31,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'29','Amsterdam','1021 NH','Koperslagerij','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.133071..'
	BEGIN /* MinBuZa-2015.133071 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.133071','2015-03-23','stcrt-2015-8458','https://zoek.officielebekendmakingen.nl/stcrt-2015-8458.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Anis',null,'Zerguit','Anis Zerguit',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1991,07,02,'Roermond','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.133070..'
	BEGIN /* MinBuZa-2015.133070 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.133070','2015-03-23','stcrt-2015-8460','https://zoek.officielebekendmakingen.nl/stcrt-2015-8460.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Soufiane',null,'Zerguit','Soufiane Zerguit',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1988,1,1,'Roermond','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.133074..'
	BEGIN /* MinBuZa-2015.133074 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.133074','2015-03-23','stcrt-2015-8461','https://zoek.officielebekendmakingen.nl/stcrt-2015-8461.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Thijs',null,'Belmonte','Thijs Belmonte',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1989,7,21,'Dordrecht','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.5498..'
	BEGIN /* MinBuZa-2015.5498 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.5498','2015-01-07','stcrt-2015-700','https://zoek.officielebekendmakingen.nl/stcrt-2015-700.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Oussama',null,'Chanou','Oussama Chanou',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1996,3,2,'Den Haag','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'30','Den Haag','2515 KC','Tullinghstraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.5361..'
	BEGIN /* MinBuZa-2015.5361 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.5361','2015-01-07','stcrt-2015-698','https://zoek.officielebekendmakingen.nl/stcrt-2015-698.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Saleh','Eddine','Akkouh','Saleh Eddine Akkouh',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1996,5,21,'Den Bosch','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'29','Zoetermeer','2722 AT','De la Gardestraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.5526..'
	BEGIN /* MinBuZa-2015.5526 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.5526','2015-01-07','stcrt-2015-697','https://zoek.officielebekendmakingen.nl/stcrt-2015-697.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Abdellah',null,'Rahmani','Abdellah Rahmani',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1987,4,13,'Zaouia','MAR')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'208 A','Den Haag','2526 GJ','Van Ostadestraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2015.5607..'
	BEGIN /* MinBuZa-2015.5607 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2015.5607','2015-01-07','stcrt-2015-696','https://zoek.officielebekendmakingen.nl/stcrt-2015-696.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Abousufian','el','Fassi','Abousufian el Fassi',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1992,3,6,'Vlissingen','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'16','Den Haag','2532 AG','Schaloenstraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.499332..'
	BEGIN /* MinBuZa-2014.499332 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.499332','2014-11-09','stcrt-2014-26138','https://zoek.officielebekendmakingen.nl/stcrt-2014-26138.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Marouane',null,'Boulahyani','Marouane Boulahyani',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1995,2,11,'Rheden','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'6','Arnhem','6832 BN','Boterbloemstraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.499334..'
	BEGIN /* MinBuZa-2014.499334 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.499334','2014-11-09','stcrt-2014-26137','https://zoek.officielebekendmakingen.nl/stcrt-2014-26137.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Soufian','el','Jelali','Soufian el Jelali',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1987,3,30,'Naarden','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'213','Huizen','1273 RJ','Westkade','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.499336..'
	BEGIN /* MinBuZa-2014.499336 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.499336','2014-11-09','stcrt-2014-26135','https://zoek.officielebekendmakingen.nl/stcrt-2014-26135.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Hatim','Mohamed','Rodgers','Hatim Mohamed Rodgers',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1989,7,2,'Den Haag','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'3','Leidschendam','2262 GA','Burgemeester Caan van Necklaan','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.499330..'
	BEGIN /* MinBuZa-2014.499330 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.499330','2014-11-09','stcrt-2014-26134','https://zoek.officielebekendmakingen.nl/stcrt-2014-26134.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Ismail',null,'Akhnikh','Ismail Akhnikh',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1982,10,22,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.499338..'
	BEGIN /* MinBuZa-2014.499338 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.499338','2014-11-09','stcrt-2014-26132','https://zoek.officielebekendmakingen.nl/stcrt-2014-26132.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Abdelkarim','el','Atrach','Abdelkarim el Atrach',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1986,10,25,'Arnhem','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.395912..'
	BEGIN /* MinBuZa-2014.395912 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.395912','2014-08-01','stcrt-2014-22642','https://zoek.officielebekendmakingen.nl/stcrt-2014-22642.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Mohamed',null,'Aden','Mohamed Aden',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1993,1,2,'Zeist','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'7','Rotterdam','3041 JL','Prof. Jonkersweg','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'PI Rotterdam, locatie De Schie',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting MinBuZa-2014.395920..'
	BEGIN /* MinBuZa-2014.395920 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('MinBuZa-2014.395920','2014-08-01','stcrt-2014-22643','https://zoek.officielebekendmakingen.nl/stcrt-2014-22643.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Khalid','Haider','Khudarhim','Khalid Haider Khudarhim',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1976,10,28,null,'KWT')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DVB/TN/180-2013..'
	BEGIN /* DVB/TN/180-2013 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DVB/TN/180-2013','2013-12-20','stcrt-2013-36710','https://zoek.officielebekendmakingen.nl/stcrt-2013-36710.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Omar',null,'Hmima','Omar Hmima',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1991,9,15,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'114 B','Amsterdam',' 1094 BC','Molukkenstraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0374-2012..'
	BEGIN /* DJZ/BR/0374-2012 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0374-2012','2012-04-17','stcrt-2012-7967','https://zoek.officielebekendmakingen.nl/stcrt-2012-7967.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Burcu',null,'Tutucu','Burcu Tutucu',@gender_female)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1986,2,25,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'47 A','Amsterdam','1091 PS','Afrikanerplein','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0016-2012..'
	BEGIN /* DJZ/BR/0016-2012 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0016-2012','2012-01-11','stcrt-2012-916','https://zoek.officielebekendmakingen.nl/stcrt-2012-916.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Sohayb','Ben','Daggoun','Sohayb Ben Daggoun',@gender_male)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1987,8,18,'Winterswijk','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'53','Den Haag','2526 LZ','Pieter Lastmankade','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'thans uitgeschreven uit de bevolkingsadministratie',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Other'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DVB/TN/054-2011..'
	BEGIN /* DVB/TN/054-2011 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DVB/TN/054-2011','2011-04-18','stcrt-2011-7209','https://zoek.officielebekendmakingen.nl/stcrt-2011-7209.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_enterprise,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,null,null,null,'Stichting Al Aqsa',@gender_not_applicable)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'121','Heerlen','6412 PB','Carboonstraat','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- Identification
		INSERT INTO sl.Identifications(OriginalIdentificationId,EntityId,RegulationId,DocumentNumber,IssueCity,IssueDate,IssueCountryIso3,IdentificationTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'41073460','Heerlen','1993-08-24','NLD',(SELECT TOP 1 IdentificationTypeId FROM sl.EnumIdentificationTypes WHERE Name='RegNumber'))
		UPDATE sl.Identifications SET OriginalIdentificationId=SCOPE_IDENTITY() WHERE IdentificationId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DVB/TN-240/10..'
	BEGIN /* DVB/TN-240/10 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DVB/TN-240/10','2011-03-15','stcrt-2011-5018','https://zoek.officielebekendmakingen.nl/stcrt-2011-5018.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_enterprise,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,null,null,null,'De Tamil Kunst en Culturele Organisatie Nederland',@gender_not_applicable)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,'779','Zeist','3706 EB','Laan van Vollenhove','NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- Identification
		INSERT INTO sl.Identifications(OriginalIdentificationId,EntityId,RegulationId,DocumentNumber,IssueCity,IssueDate,IssueCountryIso3,IdentificationTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'40637429','Zeist','1994-02-26','NLD',(SELECT TOP 1 IdentificationTypeId FROM sl.EnumIdentificationTypes WHERE Name='RegNumber'))
		UPDATE sl.Identifications SET OriginalIdentificationId=SCOPE_IDENTITY() WHERE IdentificationId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0403-2010..'
	BEGIN /* DJZ/BR/0403-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0403-2010','2010-06-08','stcrt-2010-9224','https://zoek.officielebekendmakingen.nl/stcrt-2010-9224.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Lingaratnam',null,'Thambiayah','Lingaratnam Thambiayah',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1954,5,25,'Trincomalee','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Breda',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0401-2010..'
	BEGIN /* DJZ/BR/0401-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0401-2010','2010-06-08','stcrt-2010-9227','https://zoek.officielebekendmakingen.nl/stcrt-2010-9227.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Ramachandran',null,'Selliah','Ramachandran Selliah',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1958,8,19,'Jaffna','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Amsterdam',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0400-2010..'
	BEGIN /* DJZ/BR/0400-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0400-2010','2010-06-08','stcrt-2010-9229','https://zoek.officielebekendmakingen.nl/stcrt-2010-9229.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Srirangan',null,'Ramalingam','Srirangan Ramalingam',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1964,12,19,'Point Pedro','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Vught',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0398-2010..'
	BEGIN /* DJZ/BR/0398-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0398-2010','2010-06-08','stcrt-2010-9233','https://zoek.officielebekendmakingen.nl/stcrt-2010-9233.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Thiruna',null,'Elavarasan','Thiruna Elavarasan',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1960,5,12,'Jaffna','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Zwaag',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0402-2010..'
	BEGIN /* DJZ/BR/0402-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0402-2010','2010-06-08','stcrt-2010-9234','https://zoek.officielebekendmakingen.nl/stcrt-2010-9234.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Jeyabalan',null,'Ratnasingam','Jeyabalan Ratnasingam',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1972,3,2,'Polikandy','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Haarlem',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0399-2010..'
	BEGIN /* DJZ/BR/0399-2010 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0399-2010','2010-06-08','stcrt-2010-9235','https://zoek.officielebekendmakingen.nl/stcrt-2010-9235.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Joseph','Manoharan','Jesuratnam','Joseph Manoharan Jesuratnam',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1971,8,11,'Ilavalai','LKA')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Vught',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/1224-07..'
	BEGIN /* DJZ/BR/1224-07 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/1224-07','2007-12-18','stcrt-2007-248-p7-SC83830','https://zoek.officielebekendmakingen.nl/stcrt-2007-248-p7-SC83830.html','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Samir',null,'Azzouz','Samir Azzouz',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1986,7,27,'Amsterdam','NLD')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
		
		-- Address
		INSERT INTO sl.Addresses(OriginalAddressId,EntityId,RegulationId,Number,City,Zipcode,Street,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,null,'Vught',null,null,'NLD')
		UPDATE sl.Addresses SET OriginalAddressId=SCOPE_IDENTITY() WHERE AddressId=SCOPE_IDENTITY()
		
		-- ContactInfo
		INSERT INTO sl.ContactInfo(OriginalContactInfoId,EntityId,RegulationId,Value,ContactInfoTypeId)
		VALUES (@dummy_id,@entity_id,@reg_id,'gedetineerd',(SELECT TOP 1 ContactInfoTypeId FROM sl.EnumContactInfoTypes WHERE Name='Place'))
		UPDATE sl.ContactInfo SET OriginalContactInfoId=SCOPE_IDENTITY() WHERE ContactInfoId=SCOPE_IDENTITY()
	END
	
	PRINT 'Inserting DJZ/BR/0185-07..'
	BEGIN /* DJZ/BR/0185-07 */
		-- Regulation
		INSERT INTO sl.Regulations(RegulationTitle,PublicationDate,PublicationTitle,PublicationUrl,Programme,ListTypeId)
		VALUES ('DJZ/BR/0185-07','2007-02-21','geldigheidsdatum_19-01-2015','http://wetten.overheid.nl/BWBR0021311/geldigheidsdatum_19-01-2015','SLT2007',@list_type_nl)
		SET @reg_id = SCOPE_IDENTITY()
		
		-- Entity
		INSERT INTO sl.Entities(OriginalEntityId,RegulationId,SubjectTypeId,StatusId,ListTypeId)
		VALUES (@dummy_id,@reg_id,@subject_type_person,@status_active,@list_type_nl)
		SET @entity_id = SCOPE_IDENTITY()
		UPDATE sl.Entities SET OriginalEntityId=@entity_id WHERE EntityId=@entity_id
		
		-- NameAlias
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Murat',null,'Öfkeli','Murat Öfkeli',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,'Abu',null,'Jarrah','Abu Jarrah',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		INSERT INTO sl.NameAliases(OriginalNameAliasId,EntityId,RegulationId,FirstName,MiddleName,LastName,WholeName,GenderId)
		VALUES (@dummy_id,@entity_id,@reg_id,null,null,'Ibrahim','Ibrahim',@gender_unknown)
		UPDATE sl.NameAliases SET OriginalNameAliasId=SCOPE_IDENTITY() WHERE NameAliasId=SCOPE_IDENTITY()
		
		-- Birth
		INSERT INTO sl.Births(OriginalBirthId,EntityId,RegulationId,[Year],[Month],[Day],Place,CountryIso3)
		VALUES (@dummy_id,@entity_id,@reg_id,1970,6,28,'Gaziantep','TUR')
		UPDATE sl.Births SET OriginalBirthId=SCOPE_IDENTITY() WHERE BirthId=SCOPE_IDENTITY()
	END

    -- UPDATES
	PRINT 'Adding entry to updates table.'
	BEGIN
		INSERT INTO sl.Updates(ListTypeId, Username, PublicDate, UpdatedDate) 
		VALUES (2, 'SYSTEM', GETDATE(), GETDATE()) 
	END
	
	PRINT 'Initialized NL sanction list successfully.'
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    
    -- Throw error
    DECLARE @ErMessage nvarchar(2048), @ErSeverity int, @ErState int
	SELECT @ErMessage=ERROR_MESSAGE(),@ErSeverity=ERROR_SEVERITY(),@ErState=ERROR_STATE()
	RAISERROR(@ErMessage,@ErSeverity,@ErState)
END CATCH;

IF @@TRANCOUNT > 0 COMMIT TRANSACTION;