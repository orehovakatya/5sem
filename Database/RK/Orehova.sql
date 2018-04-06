CREATE DATABASE School;
GO
USE School;
GO
CREATE TABLE Teacher
(
  id INT NOT NULL IDENTITY(1,1 ) PRIMARY KEY ,
  first_name NVARCHAR(50) NOT NULL ,
  last_name NVARCHAR(50) NOT NULL ,
);

--Зарплата учителя за занятие
CREATE TABLE Price
(
  id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
  name_lesson NVARCHAR(50) NOT NULL ,
  price MONEY NOT NULL ,
);

CREATE TABLE Salary
(
  salary_id INT NOT NULL IDENTITY(1,1 ) PRIMARY KEY ,
  teacher_id INT NOT NULL ,
  price_id INT NOT NULL ,
  CONSTRAINT FK_TEACHERID FOREIGN KEY (teacher_id) REFERENCES Teacher(id),
  CONSTRAINT FK_PRICEID FOREIGN KEY (price_id) REFERENCES Price(id),
);

GO
INSERT Teacher (first_name, last_name) VALUES (N'Иванова',N'Дарья');
INSERT Teacher (first_name, last_name) VALUES (N'Кукушкина',N'Мария');
INSERT Teacher (first_name, last_name) VALUES (N'Сырова',	N'Людмила');
INSERT Teacher (first_name, last_name) VALUES (N'Бородин',N'Александр');
INSERT Teacher (first_name, last_name) VALUES (N'Дубровина',N'Ирина');
INSERT Teacher (first_name, last_name) VALUES (N'Петрова',N'Наталья');
INSERT Teacher (first_name, last_name) VALUES (N'Ласточкин',N'Дмитрий');
INSERT Teacher (first_name, last_name) VALUES (N'Достоевский',N'Федор');
INSERT Teacher (first_name, last_name) VALUES (N'Гоголь',N'Анна');
INSERT Teacher (first_name, last_name) VALUES (N'Пушкина',N'Анастасия');
GO
INSERT Price (name_lesson, price) VALUES (N'Математика',150);
INSERT Price (name_lesson, price) VALUES (N'Информатика',230);
INSERT Price (name_lesson, price) VALUES (N'Физика',280);
INSERT Price (name_lesson, price) VALUES (N'ОБЖ',100);
INSERT Price (name_lesson, price) VALUES (N'Русский язык',261);
INSERT Price (name_lesson, price) VALUES (N'Литература',135);
INSERT Price (name_lesson, price) VALUES (N'Физкультура',245);
INSERT Price (name_lesson, price) VALUES (N'ИЗО',248);
INSERT Price (name_lesson, price) VALUES (N'Химия',312);
INSERT Price (name_lesson, price) VALUES (N'История',214);
GO
INSERT Salary (teacher_id,price_id) VALUES (1,5);
INSERT Salary (teacher_id,price_id) VALUES (2,6);
INSERT Salary (teacher_id,price_id) VALUES (7,8);
INSERT Salary (teacher_id,price_id) VALUES (3,10);
INSERT Salary (teacher_id,price_id) VALUES (4,9);
INSERT Salary (teacher_id,price_id) VALUES (5,8);
INSERT Salary (teacher_id,price_id) VALUES (6,7);
INSERT Salary (teacher_id,price_id) VALUES (7,6);
INSERT Salary (teacher_id,price_id) VALUES (8,5);
INSERT Salary (teacher_id,price_id) VALUES (9,4);
INSERT Salary (teacher_id,price_id) VALUES (10,3);
INSERT Salary (teacher_id,price_id) VALUES (1,2);
INSERT Salary (teacher_id,price_id) VALUES (2,1);
INSERT Salary (teacher_id,price_id) VALUES (3,1);
INSERT Salary (teacher_id,price_id) VALUES (7,1);


--Сколько ушло денег на проведение каждого урока (group by)
SELECT name_lesson, sum(price) as 'В сумме затрачено'
FROM Salary
LEFT JOIN Price ON Salary.price_id = Price.id
GROUP BY name_lesson

--Сколько каждый учитель заработал в итоге? (подзапрос)
SELECT first_name,last_name, sum(price) as P
  FROM
(SELECT teacher_id,price
        FROM Salary
      LEFT JOIN Price ON Salary.price_id = Price.id) AS T
LEFT JOIN Teacher ON T.teacher_id=Teacher.id
GROUP BY teacher_id, first_name,last_name

--Соединяем всё и видим кто, за что и сколько получил (join)
SELECT first_name, last_name, name_lesson, price
  FROM
    (SELECT teacher_id,name_lesson, price
     FROM Salary
       LEFT JOIN Price ON Salary.price_id = Price.id) AS T
  LEFT JOIN Teacher ON T.teacher_id = Teacher.id

