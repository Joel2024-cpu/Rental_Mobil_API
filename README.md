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

1. Buka pgAdmin
2. Buat database baru dengan nama:

```
rental_mobil
```

3. Jalankan query berikut:

```sql
CREATE TABLE cars (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    price INT,
    status VARCHAR(50),
    deleted_at TIMESTAMP NULL
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100)
);

CREATE TABLE rentals (
    id SERIAL PRIMARY KEY,
    user_id INT,
    car_id INT,
    rent_date DATE,
    return_date DATE,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (car_id) REFERENCES cars(id)
);
```

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
| POST   | /api/users     | Tambah data user                |
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
