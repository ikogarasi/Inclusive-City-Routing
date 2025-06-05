# 📘 Inclusive City - Інклюзивне місто

> _Веб-застосунок для навігації містом, адаптований для людей з особливими потребами, з функціоналом оцінки доступності міських об'єктів та побудови оптимальних маршрутів._

---

## 👤 Автор

- **ПІБ**: Віталій Куцан
- **Група**: ФеІ-42
- **Керівник**: ас. Віталій Парубочий
- **Дата виконання**: 01.06.2025

---

## 📌 Загальна інформація

- **Тип проєкту**: Веб-застосунок
- **Frontend**: React 18 + TypeScript + Vite
- **Backend**: ASP.NET Core 8.0 Web API + Clean Architecture
- **База даних**: PostgreSQL + Entity Framework Core
- **Маршрутизація**: OSRM (Open Source Routing Machine) + Docker
- **Хмарне сховище**: Azure Blob Storage

---

## 🧠 Опис функціоналу

- 🗺️ Інтерактивна карта міста з Leaflet.js
- ♿ Система оцінки доступності міських об'єктів
- 🛤️ Побудова інклюзивних маршрутів з урахуванням доступності
- 📝 Система відгуків та рейтингів від користувачів
- 📸 Завантаження та зберігання фотографій об'єктів в Azure Blob Storage
- 🔍 Пошук та фільтрація об'єктів за критеріями доступності
- 🌐 REST API з автодокументацією Swagger

---

## 🧱 Опис основних класів / файлів

| Клас / Файл                           | Призначення                                   |
| ------------------------------------- | --------------------------------------------- |
| `frontend/src/App.tsx`                | Головний компонент React-застосунку           |
| `frontend/src/pages/`                 | Сторінки застосунку (карта, деталі об'єктів)  |
| `InclusiveCity.API/Program.cs`        | Точка входу .NET API сервера                  |
| `InclusiveCity.API/Controllers/`      | REST API контролери                           |
| `InclusiveCity.Domain/Entities/`      | Доменні сутності (Structure, Review, Rating)  |
| `InclusiveCity.Application/Features/` | Бізнес-логіка та CQRS команди/запити          |
| `InclusiveCity.Persistence/Data/`     | Контекст бази даних Entity Framework          |
| `InclusiveCity.Azure.BlobStorage/`    | Сервіс для роботи з Azure Blob Storage        |
| `OSRM-API/docker-compose.yaml`        | Конфігурація Docker для сервера маршрутизації |

---

## ▶️ Як запустити проєкт "з нуля"

### 1. Встановлення інструментів

- **Node.js** v18.0.0+ + npm v9.0.0+
- **.NET SDK** 8.0+
- **PostgreSQL** 15+ (локально або хмарна база)
- **Docker Desktop** + Docker Compose
- **Visual Studio** 2022 або **VS Code**

### 2. Клонування репозиторію

```bash
git clone https://github.com/your-user/inclusive-city.git
cd inclusive-city
```

### 3. Встановлення залежностей

```powershell
# Frontend
cd frontend
npm install

# Backend
cd ..\InclusiveCity
dotnet restore
```

### 4. Створення конфігураційних файлів

#### Для backend (`InclusiveCity.API/appsettings.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Username=postgres;Password=yourpassword;Database=InclusiveCity"
  },
  "BlobConnectionString": "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=youraccountkey;EndpointSuffix=core.windows.net",
  "BlobReviewsImagesContainerName": "reviews",
  "BlobStructureImagesContainerName": "structure",
  "OsrmApi": {
    "GetComputedRouteUrl": "http://localhost:5000/route/v1/walking/<:lon1>,<:lat1>;<:lon2>,<:lat2>?steps=true&geometries=polyline6"
  },
  "OverpassApi": "https://overpass-api.de/api/interpreter?data="
}
```

### 5. Запуск бази даних та OSRM сервера

```powershell
# Запуск OSRM сервера з PostgreSQL
cd OSRM-API
docker-compose up -d

# Застосування міграцій Entity Framework
cd ..\InclusiveCity
dotnet ef database update --project InclusiveCity.Persistence --startup-project InclusiveCity.API
```

### 6. Запуск проєкту

```powershell
# Backend API
cd InclusiveCity.API
dotnet run

# Frontend (в новому терміналі PowerShell)
cd ..\frontend
npm run dev

# Перевірка OSRM API (має бути запущений)
cd ..\OSRM-API
docker-compose ps
```

**Адреси після запуску:**

- Frontend: `http://localhost:5173`
- Backend API: `https://localhost:7133`
- Swagger документація: `https://localhost:7133/swagger`

## 🔌 API приклади

### 🏢 Отримання структур

**GET /api/v1/Structure**

**Параметри запиту:**

- `latitude`, `longitude` - координати пошуку
- `around` - радіус пошуку в метрах
- `amenity` - тип об'єкта (optional)
- `name` - назва об'єкта (optional)
- `isWheelChair` - фільтр за доступністю для візочників
- `shouldRetrieveRating`, `shouldRetrieveReviews`, `shouldGetImages` - чи включати рейтинги, відгуки, зображення

**Response:**

```json
{
  "elements": [
    {
      "id": 123456789,
      "type": "node",
      "lat": 49.8397,
      "lon": 24.0297,
      "tags": {
        "amenity": "library",
        "name": "Центральна бібліотека",
        "wheelchair": "yes",
        "addr:street": "вулиця Шевченка"
      },
      "rating": 4.5,
      "reviews": [],
      "imageUrls": [
        "https://storage.blob.core.windows.net/structure/image1.jpg"
      ]
    }
  ]
}
```

---

### 📍 Деталі конкретної структури

**GET /api/v1/Structure/structure-by-id**

**Параметри:**

- `osmId` - ID об'єкта з OpenStreetMap
- `type` - тип об'єкта ("node", "way", "relation")
- `shouldRetrieveRating`, `shouldRetrieveReviews`, `shouldGetImages` - додаткова інформація

**Response:**

```json
{
  "id": 123456789,
  "type": "node",
  "lat": 49.8397,
  "lon": 24.0297,
  "tags": {
    "amenity": "library",
    "name": "Центральна бібліотека",
    "wheelchair": "yes",
    "opening_hours": "Mo-Fr 09:00-18:00"
  },
  "rating": 4.5,
  "reviews": [
    {
      "id": 1,
      "osmId": 123456789,
      "osmType": "node",
      "comment": "Відмінний доступ для візочників",
      "photoUrl": "https://storage.blob.core.windows.net/reviews/photo1.jpg",
      "createdBy": "user-guid",
      "username": "Анна",
      "createdAt": "2024-01-15T10:30:00Z",
      "rate": 5.0
    }
  ],
  "imageUrls": ["https://storage.blob.core.windows.net/structure/image1.jpg"]
}
```

---

### 🛤️ Побудова маршруту

**GET /api/v1/Routing**

**Параметри:**

- `originLatitude`, `originLongitude` - початкова точка
- `destinationLatitude`, `destinationLongitude` - кінцева точка

**Response (OSRM формат):**

```json
{
  "code": "Ok",
  "routes": [
    {
      "weightName": "duration",
      "weight": 542.3,
      "duration": 542.3,
      "distance": 1247.8,
      "geometry": "encoded_polyline6_string",
      "legs": [
        {
          "duration": 542.3,
          "distance": 1247.8,
          "weight": 542.3,
          "summary": "вулиця Шевченка, вулиця Франка",
          "steps": [
            {
              "duration": 180.5,
              "distance": 200.0,
              "weight": 180.5,
              "name": "вулиця Шевченка",
              "mode": "walking",
              "instruction": "Рухайтеся на північ по вулиці Шевченка",
              "maneuver": {
                "bearingBefore": 0,
                "bearingAfter": 15,
                "location": [24.0297, 49.8397],
                "type": "depart"
              },
              "geometry": "step_polyline_string"
            }
          ]
        }
      ]
    }
  ],
  "waypoints": [
    {
      "hint": "waypoint_hint_string",
      "distance": 0.0,
      "name": "вулиця Шевченка",
      "location": [24.0297, 49.8397]
    },
    {
      "hint": "waypoint_hint_string",
      "distance": 0.0,
      "name": "вулиця Франка",
      "location": [24.0314, 49.8423]
    }
  ]
}
```

---

### 📝 Додавання відгуку

**POST /api/v1/Review**

**Request body:**

```json
{
  "osmId": 123456789,
  "osmType": "node",
  "comment": "Чудове місце з повним доступом для людей з інвалідністю",
  "imageBase64": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQ...",
  "createdBy": "user-guid-here",
  "username": "Анна",
  "rate": 5.0
}
```

**Response:** `200 OK` (порожня відповідь)

---

### 🗺️ Інклюзивна інфраструктура

**GET /api/v1/Structure/inclusive-infrastructure**

**Параметри:**

- `latitude`, `longitude`, `around` - область пошуку
- `toilets`, `busStops`, `kerbs`, `tactilePaving`, `ramps` - типи інфраструктури

**Response:**

```json
{
  "elements": [
    {
      "id": 987654321,
      "type": "node",
      "lat": 49.8405,
      "lon": 24.0301,
      "tags": {
        "amenity": "toilets",
        "wheelchair": "yes",
        "toilets:wheelchair": "yes"
      },
      "rating": 4.2,
      "reviews": [],
      "imageUrls": []
    }
  ]
}
```

---

## 🖱️ Інструкція для користувача

1. **Головна сторінка** — інтерактивна карта міста:

   - `🗺️ Карта` — перегляд міських об'єктів на карті
   - `🔍 Пошук` — пошук об'єктів за назвою або типом
   - `📍 Моє місцезнаходження` — визначення поточної позиції

2. **Робота з об'єктами**:

   - Клік на об'єкт відкриває детальну інформацію
   - `⭐ Оцінка доступності` — перегляд рейтингу доступності
   - `📝 Відгуки` — читання та додавання відгуків про доступність
   - `📸 Фото` — перегляд фотографій об'єкта

3. **Побудова маршруту**:
   - `🎯 Точка А` — вибір початкової точки
   - `🏁 Точка Б` — вибір кінцевої точки
   - `♿ Налаштування доступності` — вказання особливих потреб
   - `🛤️ Побудувати маршрут` — отримання інклюзивного маршруту

---

## 📷 Скриншоти / приклади роботи

- **Головна сторінка з картою** — інтерактивна карта з об'єктами
- **Деталі об'єкта** — інформація про об'єкт
- **Фільтрування об'єктів** — фільтри об'єктів за доступністю та категоріями
- **Побудова маршруту** — вибір точок та налаштування доступності
- **Інклюзивні об'єкти** — приклад відображення інклюзивних об'єктів на карті

---

## 🧪 Проблеми і рішення

| Проблема                                    | Рішення                                                         |
| ------------------------------------------- | --------------------------------------------------------------- |
| Помилка підключення до бази даних           | Перевірити рядок підключення та запуск PostgreSQL               |
| CORS помилка при запитах до API             | Увімкнути CORS middleware у `Program.cs`                        |
| Docker контейнери не запускаються           | Перевірити наявність Docker Desktop та доступні порти           |
| OSRM не будує маршрути                      | Перевірити завантаження OSM даних та запуск контейнера          |
| Azure Blob Storage недоступне               | Перевірити налаштування `BlobConnectionString`                  |
| Entity Framework міграції не застосовуються | Запустити `dotnet ef database update` з правильними параметрами |
| Frontend не підключається до API            | Перевірити URL API та налаштування CORS                         |

---

## 🧾 Використані джерела / література

- [React.js офіційна документація](https://reactjs.org/docs/)
- [ASP.NET Core документація](https://docs.microsoft.com/aspnet/core/)
- [Entity Framework Core гайд](https://docs.microsoft.com/ef/core/)
- [OSRM API документація](http://project-osrm.org/docs/v5.24.0/api/)
- [Leaflet.js для карт](https://leafletjs.com/reference.html)
- [Material-UI компоненти](https://mui.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Azure Blob Storage](https://docs.microsoft.com/azure/storage/blobs/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 💻 Результати

**Цілі проєкту:**

- ✅ Створення інклюзивного веб-застосунку для міської навігації
- ✅ Побудова доступних маршрутів з урахуванням потреб користувачів
- ✅ Інтеграція з картографічними сервісами та хмарними технологіями

**Технічні досягнення:**

- Frontend на React 18 + TypeScript з адаптивним Material UI дизайном
- Backend API на ASP.NET Core 8.0 з Clean Architecture
- Система маршрутизації OSRM для побудови доступних маршрутів
- Інтеграція з Azure Blob Storage для зберігання медіафайлів
- База даних PostgreSQL з Entity Framework Core

**Функціональні можливості:**

- Інтерактивна карта міста з позначенням доступних об'єктів
- Система відгуків та рейтингів доступності від користувачів
- Побудова оптимальних маршрутів з урахуванням особливих потреб

---

_© 2025 Inclusive City Project. Створено в навчальних цілях._

```

```
