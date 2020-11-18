CREATE TABLE Countries (
	id		CHAR(3) UNIQUE,
	name		CHAR(128),
	PRIMARY KEY(id)
);

CREATE TABLE Towns (
	id		INT GENERATED ALWAYS AS IDENTITY,
	name		CHAR(128) NOT NULL,
	postal_code	VARCHAR(16),
	country_id  	CHAR(3) NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT fk_country
    		FOREIGN KEY(country_id) 
	  	REFERENCES Countries(id)
);

CREATE TABLE Roles (
	id		INT GENERATED ALWAYS AS IDENTITY,
	name		CHAR(16) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE Departments (
	id		INT GENERATED ALWAYS AS IDENTITY,
	title		VARCHAR(128) NOT NULL UNIQUE,
	PRIMARY KEY(id)
);

CREATE TABLE Companies (
	id		VARCHAR(128) NOT NULL UNIQUE,
	name		VARCHAR(128) NOT NULL,
	web_page	VARCHAR(128),
	PRIMARY KEY(id)
);

CREATE TABLE Users (
	id			VARCHAR(128) NOT NULL UNIQUE,
	username		VARCHAR(16) NOT NULL,
	first_name		VARCHAR(64) NOT NULL,
	middle_name		VARCHAR(64),
	last_name		VARCHAR(64) NOT NULL,
	password		VARCHAR(256) NOT NULL,
	phone			VARCHAR(16) NOT NULL,
	email			VARCHAR(128) NOT NULL,
	gender			INT NOT NULL,
	mailing_address		VARCHAR(256) NOT NULL,
	date_of_birth		DATE NOT NULL,
	is_deleted		BOOLEAN NOT NULL,	
	residence_town_id	INT NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT fk_town
    		FOREIGN KEY(residence_town_id) 
	  	REFERENCES Towns(id)
);

CREATE TABLE UserRoles (
	user_id			VARCHAR(128) NOT NULL,
	role_id			INT NOT NULL,
	PRIMARY KEY(user_id,role_id),	
	CONSTRAINT fk_user
		FOREIGN KEY(user_id) 
		REFERENCES Users(id),
	CONSTRAINT fk_role
		FOREIGN KEY(role_id) 
		REFERENCES Roles(id)
);

CREATE TABLE CompanyAddresses (
	id			INT  GENERATED ALWAYS AS IDENTITY,
	address 		VARCHAR(128) NOT NULL,
	email			VARCHAR(128),
	phone			VARCHAR(16),
	is_primary		BOOLEAN NOT NULL,
	town_id			INT NOT NULL,
	company_id		VARCHAR(128) NOT NULL,
	PRIMARY KEY(id),	
	CONSTRAINT fk_town
		FOREIGN KEY(town_id) 
		REFERENCES Towns(id),
	CONSTRAINT fk_company
		FOREIGN KEY(company_id) 
		REFERENCES Companies(id)
);

CREATE TABLE Jobs (
	id			INT  GENERATED ALWAYS AS IDENTITY,
	title 			VARCHAR(128) NOT NULL,
	min_education_lvl	INT NOT NULL,
	department_id		INT NOT NULL,
	PRIMARY KEY(id),	
	CONSTRAINT fk_department
		FOREIGN KEY(department_id) 
		REFERENCES Departments(id)
);

CREATE TABLE UserJobs (
	id			INT  GENERATED ALWAYS AS IDENTITY,
	user_id 		VARCHAR(128) NOT NULL,
	manager_id		VARCHAR(128),
	job_id			INT NOT NULL,
	companyAddress_id	INT NOT NULL,	
	monthly_salary	NUMERIC(12,4) NOT NULL,
	currency		INT NOT NULL,
	week_hours		INT,
	start_date		DATE NOT NULL,
	end_date		DATE,
	PRIMARY KEY(id),	
	CONSTRAINT fk_user
		FOREIGN KEY(user_id) 
		REFERENCES Users(id),
	CONSTRAINT fk_manager
		FOREIGN KEY(manager_id) 
		REFERENCES Users(id),
	CONSTRAINT fk_job
		FOREIGN KEY(job_id) 
		REFERENCES Jobs(id),
	CONSTRAINT fk_companyAddress
		FOREIGN KEY(companyAddress_id) 
		REFERENCES CompanyAddresses(id)
);