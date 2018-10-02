CREATE TABLE Jobs(job_name NVARCHAR(50) NOT NULL,
				  jdate_from DATE PRIMARY KEY,
				  fee MONEY);

CREATE TABLE Departments(department_name NVARCHAR(50) NOT NULL,
                         jd_from DATE NOT NULL,
		                 date_to DATE PRIMARY KEY,
						 workers_amount INT,
	 				     monthly_pay MONEY,		         
						 
	 					 FOREIGN KEY(jd_from) REFERENCES Jobs(jdate_from));


INSERT INTO Jobs(jdate_from, job_name) VALUES ('1900-01-01', 'Инженер-конструктор');
INSERT INTO Jobs(jdate_from, job_name) VALUES ('1900-02-01', 'Инженер-технолог');

INSERT INTO Departments(department_name, jd_from, date_to) VALUES ('КБ', '1900-01-01', '1900-04-01');
INSERT INTO Departments(department_name, jd_from, date_to) VALUES ('ТО', '1900-02-01', '1900-05-01');