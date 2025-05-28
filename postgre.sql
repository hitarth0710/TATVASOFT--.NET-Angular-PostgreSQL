CREATE TABLE student (
  Firstname VARCHAR(255),
  Lastname VARCHAR(255),
  Enroll INT,
  Branch VARCHAR(50)
);

INSERT INTO student(Firstname, Lastname, Enroll, Branch)
VALUES
('Rahul', 'Desai', 17, 'BIO'),
('Priya', 'Joshi', 27, 'BIO'),
('Karan', 'Patil', 61, 'BIO'),
('Sneha', 'Nair', 38, 'CHEM'),
('Amit', 'Kulkarni', 25, 'PHY'),
('Pooja', 'Shetty', 26, 'PHY'),
('Yash', 'Pandey', 32, 'CHEM'),
('Tanvi', 'Mishra', 40, 'CHEM'),
('Vikas', 'Rao', 49, 'BIO'),
('Simran', 'Kaur', 30, 'BIO');

SELECT Firstname, Enroll FROM student;
SELECT * FROM student;

UPDATE student
SET Department = CASE
    WHEN Branch IN ('BIO', 'BOT') THEN 'Biological Sciences'
    WHEN Branch IN ('PHY', 'CHEM') THEN 'Physical Sciences'
    ELSE Department
END;

DELETE FROM student WHERE enroll='19';
ALTER TABLE student RENAME COLUMN Branch TO Stream;

SELECT * FROM student WHERE Enroll=8;
SELECT * FROM student WHERE Enroll<28;
SELECT * FROM student WHERE Enroll<=28;
SELECT * FROM student WHERE Enroll>28;
SELECT * FROM student WHERE Enroll>=28;
SELECT * FROM student WHERE Enroll!=8;
SELECT * FROM student WHERE Firstname LIKE 'S%';
SELECT * FROM student WHERE Firstname LIKE 'Tan%';
SELECT * FROM student WHERE Firstname ILIKE 'TAN%';
SELECT * FROM student ORDER BY Enroll DESC;
SELECT * FROM student LIMIT 10;
SELECT * FROM student OFFSET 5 LIMIT 10;

SELECT COUNT(*) FROM student;
SELECT AVG(Enroll) FROM student;
SELECT MAX(Enroll), MIN(Enroll) FROM student;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream HAVING COUNT(*) > 2;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream HAVING COUNT(*) < 3;

CREATE TABLE temp_students(enrollment INT);
SELECT * FROM temp_students;
DROP TABLE temp_students;

DELETE FROM student
WHERE Firstname IS NULL
  AND Lastname IS NULL
  AND Enroll IS NULL
  AND Stream IS NULL;

ALTER TABLE student ADD COLUMN student_id SERIAL PRIMARY KEY;
ALTER TABLE student ALTER COLUMN Enroll SET NOT NULL;
ALTER TABLE student ADD Department VARCHAR(255);
ALTER TABLE student
ADD temp_field INT;
ALTER TABLE student
DROP COLUMN temp_field;

CREATE TABLE department (
  dept_id SERIAL PRIMARY KEY,
  dept_name VARCHAR(255) UNIQUE NOT NULL
);
INSERT INTO department (dept_name)
VALUES
('Biological Sciences'),
('Physical Sciences');

ALTER TABLE student ADD COLUMN dept_id INT;

UPDATE student
SET dept_id = CASE
    WHEN Department = 'Biological Sciences' THEN 1
    WHEN Department = 'Physical Sciences' THEN 2
END;

ALTER TABLE student
ADD CONSTRAINT fk_dept
FOREIGN KEY (dept_id) REFERENCES department(dept_id);

INSERT INTO department (dept_name)
VALUES ('Mathematics');

INSERT INTO student (Firstname, Lastname, Enroll, Stream, Department, dept_id)
VALUES ('Asha', 'Menon', 21, 'None', NULL, NULL);

-- inner join
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
INNER JOIN department d ON s.dept_id = d.dept_id;

-- left join
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
LEFT JOIN department d ON s.dept_id = d.dept_id;

-- right join
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
RIGHT JOIN department d ON s.dept_id = d.dept_id;

SELECT s.firstname, s.enroll, d.dept_name
FROM student s
FULL JOIN department d ON s.dept_id = d.dept_id;