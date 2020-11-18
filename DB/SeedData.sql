INSERT INTO Countries(id, name)
VALUES 
('BGN', 'Bulgaria'),
('ENG', 'England'),
('USA', 'United States Of America'),
('ROU', 'Romania');

INSERT INTO Towns(name, postal_code,country_id)
VALUES 
('Sofia', '1000','BGN'),
('Shumen', '9700','BGN'),
('Varna', '9000','BGN'),
('London', NULL,'ENG'),
('Birmingham', NULL,'ENG'),
('New York', NULL,'USA'),
('Chicago', NULL,'USA'),
('Timisoara', NULL,'ROU');

INSERT INTO Roles(name)
VALUES 
('Admin'),('User');

INSERT INTO Departments(title)
VALUES 
('Finance'),('It-Development'),
('E-Commerse'),('Maintanance');

INSERT INTO Companies(id,name,web_page)
VALUES 
('6a3dd528-f680-401b-ad71-e0f6a4e434d6','TestCompany','www.test-us.com'),
('179c7979-85dd-46cc-b470-af9c23f5e81d','LocaColaCompany','www.drink-loca.com'),
('3003aeaf-d61f-4bfd-827e-664890ead159','TheOfficeCompany','www.office-company.com'),
('5bfa1b2e-3e87-4a0b-9cec-aa68d69c342a','TheUFOCompany','www.ufo-company.com');

INSERT INTO Users(		  id,
				  username,
				  first_name,
				  middle_name,
				  last_name,
				  phone,
				  email,
				  gender,
				  date_of_birth,
				  is_deleted,
				  mailing_address,
				  residence_town_id,
				  password)
VALUES 
('82c3ca85-7d10-476e-926d-54abff1fc213','user1','Purvan','Stamenov','Kostov','0883348852','purvan123@gmail.com',1,TO_DATE('17/12/1985', 'DD/MM/YYYY'),FALSE,'Stara Planina 82 block 6 ap 45',2,'pass123'),
('3ef7170f-fd2e-4edc-892d-6311d89de48f','user2','Vtorislava','Stamenova','Kostova','0883347451','vtorislava321@gmail.com',0,TO_DATE('11/12/1990', 'DD/MM/YYYY'),FALSE,'Chervena Stana 38',1,'pass123'),
('6bb7fb44-1ac2-41f5-a60e-6d46542856c8','user3','Johnatan','Samuel','Smith','0883347476','johny-b-goode@gmail.com',1,TO_DATE('01/10/1991', 'DD/MM/YYYY'),FALSE,'A. Linkoln 13',6,'pass123'),
('baefb2dd-b1f9-4c1a-b98c-21b4e0300d34','user4','Jonica',NULL,'Minune','0883347476','jonica-minune@gmail.com',1,TO_DATE('05/07/1941', 'DD/MM/YYYY'),FALSE,'Semafor 43',8,'pass123');

INSERT INTO UserRoles(user_id,role_id)
VALUES 
('82c3ca85-7d10-476e-926d-54abff1fc213',1),
('82c3ca85-7d10-476e-926d-54abff1fc213',2),
('3ef7170f-fd2e-4edc-892d-6311d89de48f',2),
('6bb7fb44-1ac2-41f5-a60e-6d46542856c8',2),
('baefb2dd-b1f9-4c1a-b98c-21b4e0300d34',2);

INSERT INTO CompanyAddresses (address,email,phone,is_primary,town_id,company_id)
VALUES 
('primary Address of Test Company some street','testCompany_address1@gmail.com','0881235555',TRUE,5,'6a3dd528-f680-401b-ad71-e0f6a4e434d6'),
('secondary Address 2 of Test Company some street','testCompany_alternativeOfficed@gmail.com','0881235557',FALSE,3,'6a3dd528-f680-401b-ad71-e0f6a4e434d6'),
('main Address of LocaColaCompany','loca_cola_main@gmail.com','0871235558',TRUE,7,'179c7979-85dd-46cc-b470-af9c23f5e81d'),
('Ulica tri ushi 23','oficceadress@gmail.com','0481554400',TRUE,1,'3003aeaf-d61f-4bfd-827e-664890ead159'),
('Vladislav Varnenchik 34','oficceadress_varna@gmail.com','0481554401',FALSE,3,'3003aeaf-d61f-4bfd-827e-664890ead159');

INSERT INTO Jobs(title,min_education_lvl,department_id)
VALUES
('Accountant',3,1),
('Cleaning',0,4),
('Cook',1,4),
('Financial Transactions Operator',2,1),
('Quality Assurance',3,2),
('Front-End Developer',3,2),
('Back-End Developer',3,2),
('Salution Architect',4,2),
('Online Assistent',2,3),
('Online Sales',2,3),
('Online Customer Support',2,3);

INSERT INTO Userjobs(	user_id,
			manager_id,
			job_id,
			companyaddress_id,
			monthly_salary,
			week_hours,
			currency,
			start_date,
			end_date)
VALUES
('82c3ca85-7d10-476e-926d-54abff1fc213',NULL,1,1,2000,48,0,TO_DATE('17/12/2015', 'DD/MM/YYYY'),TO_DATE('17/12/2017', 'DD/MM/YYYY')),
('82c3ca85-7d10-476e-926d-54abff1fc213','baefb2dd-b1f9-4c1a-b98c-21b4e0300d34',5,2,3000,48,0,TO_DATE('13/07/2018', 'DD/MM/YYYY'),NULL),
('3ef7170f-fd2e-4edc-892d-6311d89de48f','baefb2dd-b1f9-4c1a-b98c-21b4e0300d34',7,3,3200,48,0,TO_DATE('27/11/2019', 'DD/MM/YYYY'),NULL),
('6bb7fb44-1ac2-41f5-a60e-6d46542856c8',NULL,2,4,1100,24,1,TO_DATE('03/04/2019', 'DD/MM/YYYY'),NULL),
('baefb2dd-b1f9-4c1a-b98c-21b4e0300d34',NULL,8,2,3100,24,1,TO_DATE('01/01/2018', 'DD/MM/YYYY'),NULL),
('baefb2dd-b1f9-4c1a-b98c-21b4e0300d34',NULL,8,3,3500,24,0,TO_DATE('24/09/2017', 'DD/MM/YYYY'),NULL);

