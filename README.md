# TATVASOFT--.NET-Angular-PostgreSQL

This repository provides a detailed SQL script (`Postgre.sql`) that illustrates essential SQL concepts using a **student** and **department** schema—perfect for practicing and mastering SQL with PostgreSQL.

---

## 📋 Summary

The script demonstrates a broad array of basic and advanced SQL techniques, such as:

- **Table Setup**
  - Creation of `student` and `department` tables
- **Data Population**
  - Example entries for students and departments
- **Schema Alterations**
  - Renaming columns
  - Adding new columns
  - Applying constraints
- **Data Operations**
  - `UPDATE`, `DELETE`, and managing `NULL` values
- **Constraints**
  - Usage of `PRIMARY KEY`, `NOT NULL`, `UNIQUE`, `FOREIGN KEY`
- **Querying Data**
  - `SELECT` statements with `WHERE` filters
  - Pattern searches with `LIKE` and `ILIKE`
  - Sorting results using `ORDER BY`
  - Aggregate functions (`COUNT`, `AVG`, `MAX`, `MIN`)
  - Grouping and filtering with `GROUP BY` and `HAVING`
- **Result Pagination**
  - Implementing `LIMIT` and `OFFSET`
- **Join Examples**
  - `INNER JOIN`, `LEFT JOIN`, `RIGHT JOIN`, and `FULL JOIN`
- **Temporary Tables**
  - Creating and removing a temporary table

---

## 🛠 Usage Instructions

1. **Launch** your PostgreSQL client or interface (such as `psql` or `pgAdmin`).
2. **Execute the script** by running the following command in your terminal:

```bash
psql -U your_username -d your_database -f Postgre.sql
```

> Be sure to substitute `your_username` and `your_database` with your actual PostgreSQL login and database name.

---

## 📂 Contents

- `Postgre.sql`: The main SQL script containing all operations for the student and department schema.

---

## ✅ Requirements

- PostgreSQL installed locally
- Basic knowledge of SQL

---

## 👨‍🎓 For Learners

This set is designed for students and newcomers who want practical experience with PostgreSQL and relational database modeling.

---

## 📧 Support

For feedback or questions, please open an issue on GitHub or contact the maintainer.
