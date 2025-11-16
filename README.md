# QueueService - מערכת ניהול תורים

מערכת מקצועית לניהול תורים (appointments) המיושמת ב-.NET Core Web API עם MongoDB.

##  התחלה מהירה

### הפעלה מהירה באמצעות Docker
הפרויקט מגיע עם קובץ docker-compose.yml שמאפשר להפעיל את המערכת בלחיצה אחת — ללא התקנת MongoDB וללא התקנת ‎.NET.
הפעלה:
```
docker compose up --build
```
לאחר שהמערכת עולה:

API זמין בכתובת:
http://localhost:5000/swagger

MongoDB פועל בכתובת:
mongodb://localhost:27017

##  API Endpoints

| שיטה | נתיב | תיאור |
|------|------|-------|
| GET | `/api/appointments` | קבלת כל התורים |
| GET | `/api/appointments/{id}` | קבלת תור ספציפי |
| POST | `/api/appointments` | יצירת תור חדש |
| PUT | `/api/appointments/{id}` | עדכון תור קיים |
| DELETE | `/api/appointments/{id}` | מחיקת תור |

## ארכיטקטורה

### תבנית CQRS (Command Query Responsibility Segregation)
המערכת מפרידה בין פעולות כתיבה (Commands) לפעולות קריאה (Queries):

- **Commands**: יצירה, עדכון, מחיקה
- **Queries**: קבלת נתונים
- **Handlers**: מבצעים את הלוגיקה העסקית

## טכנולוגיות

- **.NET 8.0** - Framework עדכני ויציב
- **MongoDB 7.0** - בסיס נתונים NoSQL
- **MediatR** - יישום CQRS pattern
- **Swagger/OpenAPI** - תיעוד אוטומטי ואינטראקטיבי
- **ASP.NET Core** - Web API framework

## מבנה הפרויקט

```
QueueService/
├── Models/                   # מודלים של הנתונים
│   └── Appointment.cs
├── Features/                 # תכונות לפי CQRS
│   └── Appointments/
│       ├── Commands/         # פקודות (Create, Update, Delete)
│       ├── Queries/          # שאילתות (GetAll, GetById)
│       └── Handlers/         # מטפלים עם הלוגיקה
├── Repositories/             # שכבת גישה לנתונים
│   ├── IAppointmentRepository.cs
│   └── AppointmentRepository.cs
├── Configuration/            # הגדרות
│   └── MongoDbSettings.cs
├── Controllers/              # API Controllers
│   └── AppointmentsController.cs
└── Program.cs               # נקודת כניסה ראשית
```

##  דוגמאות שימוש

### יצירת תור חדש
```bash
POST /api/appointments
{
  "nationalId": "123456789",
  "customerName": "יוסי כהן",
  "phoneNumber": "050-1234567",
  "serviceCategory": "Interior Ministry",
  "serviceType": "Renew Passport",
  "appointmentDate": "2025-11-15T10:00:00",
  "notes": "נא לתאם בבוקר"
}

```

### עדכון תור
```bash
PUT /api/appointments/{id}
{
  "nationalId": "123456789",
  "customerName": "יוסי כהן",
  "phoneNumber": "050-1234567",
  "appointmentDate": "2025-11-15T14:00:00",
  "serviceCategory": "Interior Ministry",
  "serviceType": "Renew Passport",
  "status": "Confirmed",
  "notes": "שנו לשעה 14:00"
}
```

##  אבטחה

 **הערה חשובה**: המערכת הנוכחית מוגדרת לפיתוח.
לפני העברה לייצור (production), יש להוסיף:
- Authentication & Authorization
- Rate Limiting
- Input Validation
- CORS מוגבל לדומיינים ספציפיים

## רישיון
הפרויקט פתוח לשימוש לימודי ומקצועי.
