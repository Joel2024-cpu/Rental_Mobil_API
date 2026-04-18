# Rental Mobil API

## a) Deskripsi Project

Rental Mobil API adalah sistem backend berbasis REST API yang digunakan untuk mengelola data penyewaan mobil. Sistem ini memungkinkan pengguna untuk melakukan pengelolaan data mobil, data user, serta transaksi penyewaan (rental).

Domain yang dipilih adalah **manajemen rental mobil**, yang mencakup:

* Pengelolaan data mobil
* Pengelolaan data pengguna
* Pengelolaan transaksi penyewaan

---

## b) Teknologi yang Digunakan

* **Bahasa Pemrograman**: C#
* **Framework**: ASP.NET Core Web API
* **Database**: PostgreSQL
* **Library ORM**: Dapper
* **Tools**:

  * Visual Studio 2022
  * pgAdmin
  * Postman (testing API)

---

## c) Langkah Instalasi & Menjalankan Project

1. Clone repository:

```bash
git clone https://github.com/username/rental-mobil-api.git
```

2. Buka project di Visual Studio 2022

3. Install dependency (jika belum):

* Dapper
* Npgsql

4. Atur koneksi database di `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=rental_mobil;Username=postgres;Password="
}
```

5. Jalankan project:

* Tekan **Ctrl + F5** atau klik **Start**

---

## d) Cara Import Database

Berikut langkah-langkah untuk mengimpor database menggunakan PostgreSQL melalui pgAdmin:

1. Membuat Database

1. Buka aplikasi **pgAdmin**
2. Login ke server PostgreSQL
3. Klik kanan pada **Databases** → pilih **Create → Database**
4. Isi nama database:

   ```
   rental_mobil
   ```
5. Klik **Save**

---
2. Membuka Query Tool

1. Pilih database `rental_mobil`
2. Klik kanan → **Query Tool**

---
3. Menjalankan Script Database

Salin dan jalankan script SQL berikut di Query Tool:

```sql
-- DROP TABLE (agar bisa dijalankan ulang tanpa error)
DROP TABLE IF EXISTS rentals CASCADE;
DROP TABLE IF EXISTS cars CASCADE;
DROP TABLE IF EXISTS users CASCADE;

-- USERS
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100),
    password VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- CARS
CREATE TABLE cars (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    price INT,
    status VARCHAR(50),
    deleted_at TIMESTAMP NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- RENTALS
CREATE TABLE rentals (
    id SERIAL PRIMARY KEY,
    user_id INT,
    car_id INT,
    rent_date DATE,
    return_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (car_id) REFERENCES cars(id)
);

-- INDEX
CREATE INDEX idx_user_id ON rentals(user_id);
CREATE INDEX idx_car_id ON rentals(car_id);

-- DATA USERS
INSERT INTO users (name, email, password) VALUES
('Rafli', 'rafli@mail.com', '123'),
('Jul', 'jul@mail.com', '123'),
('Arif', 'arif@mail.com', '123'),
('Muhammad', 'muhammad@mail.com', '123'),
('Zulfikar', 'zulfikar@mail.com', '123'),
('Admin', 'admin@gmail.com', '123');

-- DATA CARS
INSERT INTO cars (name, price, status) VALUES
('Avanza', 300000, 'available'),
('Xenia', 280000, 'available'),
('Brio', 170000, 'available'),
('Innova', 350000, 'available'),
('Pajero', 580000, 'available');

-- DATA RENTALS
INSERT INTO rentals (user_id, car_id, rent_date, return_date) VALUES
(1,1,'2026-04-01','2026-04-03'),
(2,2,'2026-04-02','2026-04-04'),
(3,3,'2026-04-03','2026-04-05'),
(4,4,'2026-04-04','2026-04-06'),
(5,5,'2026-04-05','2026-04-07');
```

---
4. Menjalankan Query

Klik tombol **Execute (▶)** atau tekan **F5** untuk menjalankan seluruh script.

---
5. Verifikasi Database

Untuk memastikan data berhasil diimport, jalankan:

```sql
SELECT * FROM users;
SELECT * FROM cars;
SELECT * FROM rentals;
```

Jika data muncul, berarti database berhasil dibuat dan siap digunakan.

---

### 6. Konfigurasi Koneksi di Project

Pastikan file `appsettings.json` pada project sudah sesuai:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=rental_mobil;Username=postgres;Password="
}
```

Sesuaikan:

* Username PostgreSQL
* Password PostgreSQL

---


---

## e) Daftar Endpoint API

| Method | URL            | Keterangan                      |
| ------ | -------------- | ------------------------------- |
| GET    | /api/cars      | Ambil semua data mobil          |
| GET    | /api/cars/{id} | Ambil data mobil berdasarkan ID |
| POST   | /api/cars      | Tambah data mobil               |
| PUT    | /api/cars/{id} | Update data mobil               |
| DELETE | /api/cars/{id} | Hapus data mobil (soft delete)  |
| GET    | /api/users     | Ambil semua data user           |
| GET    | /api/users/{id}| Ambil data user berdasarkan id  |
| DELETE | /api/users/{id}| Hapus data user                 |  
| GET    | /api/rentals   | Ambil data rental (join)        |
| POST   | /api/rentals   | Tambah data rental              |

---

## f) Link Video Presentasi

Berikut adalah link video presentasi project:



---

## Catatan

* Sistem menggunakan **soft delete** pada tabel cars
* Validasi ID diterapkan pada endpoint GET by ID
* Response API menggunakan format JSON konsisten:

```json
{
  "status": "success",
  "data": {...}
}
```

atau

```json
{
  "status": "error",
  "message": "Data tidak ditemukan"
}
```
