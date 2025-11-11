# QueueService - מערכת ניהול תורים

## סקירה כללית
שירות לניהול תורים (appointments) המיושם ב-.NET Core Web API עם MongoDB כבסיס נתונים.
השירות בנוי על פי תבנית CQRS (Command Query Responsibility Segregation) תוך שמירה על עקרונות SOLID.

## טכנולוגיות
- **.NET 8.0** - Framework הבסיסי
- **MongoDB 7.0** - בסיס נתונים NoSQL
- **MediatR** - יישום תבנית CQRS
- **Swagger/OpenAPI** - תיעוד API אוטומטי
- **ASP.NET Core** - Web API

## מבנה הפרויקט

```
QueueService/
├── Models/                      # מודלים של הישויות
│   └── Appointment.cs          # מודל התור
├── Features/                    # תכונות מאורגנות לפי CQRS
│   └── Appointments/
│       ├── Commands/           # פקודות (Create, Update, Delete)
│       ├── Queries/            # שאילתות (GetAll, GetById)
│       └── Handlers/           # מטפלים (Handlers) לפקודות ושאילתות
├── Repositories/                # שכבת גישה לנתונים
│   ├── IAppointmentRepository.cs
│   └── AppointmentRepository.cs
├── Configuration/               # הגדרות
│   └── MongoDbSettings.cs
└── Controllers/                 # Controllers
    └── AppointmentsController.cs
```

## API Endpoints

השירות מספק 5 endpoints עיקריים:

### 1. קבלת כל התורים
```http
GET /api/appointments
```

### 2. קבלת תור לפי ID
```http
GET /api/appointments/{id}
```

### 3. יצירת תור חדש
```http
POST /api/appointments
Content-Type: application/json

{
  "customerName": "שם הלקוח",
  "phoneNumber": "050-1234567",
  "appointmentDate": "2025-11-15T10:00:00",
  "serviceType": "סוג השירות",
  "notes": "הערות נוספות"
}
```

### 4. עדכון תור קיים
```http
PUT /api/appointments/{id}
Content-Type: application/json

{
  "customerName": "שם מעודכן",
  "phoneNumber": "050-1234567",
  "appointmentDate": "2025-11-15T14:00:00",
  "serviceType": "סוג השירות",
  "status": "Scheduled",
  "notes": "הערות מעודכנות"
}
```

### 5. מחיקת תור
```http
DELETE /api/appointments/{id}
```

## תבנית CQRS

הפרויקט מיישם תבנית CQRS באמצעות MediatR:

- **Commands (פקודות)**: פעולות שמשנות מצב (Create, Update, Delete)
- **Queries (שאילתות)**: פעולות קריאה בלבד (GetAll, GetById)
- **Handlers (מטפלים)**: לוגיקה עסקית המטפלת בפקודות ושאילתות

### יתרונות התבנית:
- הפרדה ברורה בין קריאה לכתיבה
- קוד מאורגן וקריא
- קל להרחבה ולתחזוקה
- תמיכה ב-Unit Testing

## עקרונות SOLID

הקוד נכתב בהתאם לעקרונות SOLID:
- **Single Responsibility**: כל מחלקה אחראית על דבר אחד בלבד
- **Open/Closed**: קל להרחבה ללא שינוי קוד קיים
- **Liskov Substitution**: שימוש ב-Interfaces (IAppointmentRepository)
- **Interface Segregation**: Interfaces ממוקדים ולא מנופחים
- **Dependency Inversion**: תלות ב-Abstractions דרך DI

## MongoDB

### הגדרות מקומיות
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "QueueServiceDb",
    "AppointmentsCollectionName": "appointments"
  }
}
```

### שימוש ב-MongoDB Atlas (Cloud)
לשימוש ב-MongoDB Atlas, עדכן את `appsettings.json`:
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://<username>:<password>@<cluster>.mongodb.net/?retryWrites=true&w=majority",
    "DatabaseName": "QueueServiceDb",
    "AppointmentsCollectionName": "appointments"
  }
}
```

## Swagger Documentation

ממשק Swagger זמין ב:
- **Local**: http://localhost:5000
- **Replit**: https://[your-repl-url].repl.co

הממשק מספק:
- תיעוד מלא של כל ה-API endpoints
- אפשרות לבצע בדיקות ישירות מהממשק
- סכמות של המודלים
- דוגמאות לשימוש

## הרצת הפרויקט

### ב-Replit
הפרויקט עובד אוטומטית עם 2 workflows:
1. **mongodb** - מריץ את MongoDB מקומי
2. **api** - מריץ את Web API על פורט 5000

### מקומית
```bash
# התקנת dependencies
dotnet restore

# הרצת MongoDB (בטרמינל נפרד)
./start-mongo.sh

# הרצת האפליקציה
dotnet run
```

## פיתוח נוסף

### הוספת פיצ'ר חדש
1. צור Command/Query חדש ב-`Features/Appointments/`
2. צור Handler מתאים ב-`Features/Appointments/Handlers/`
3. הוסף endpoint ל-Controller
4. הסבר עדכון ל-Swagger documentation

### הוספת Validation
ניתן להוסיף FluentValidation:
```bash
dotnet add package FluentValidation.AspNetCore
```

### יומנים (Logging)
השירות משתמש ב-ASP.NET Core logging מובנה.
ניתן להוסיף Serilog או NLog לצורך מתקדם יותר.

## בעיות נפוצות

### MongoDB לא מתחבר
- ודא ש-MongoDB רץ (workflow: mongodb)
- בדוק את ה-connection string ב-`appsettings.json`
- אם משתמש ב-Atlas, בדוק IP whitelist

### שגיאות CORS
ה-API מוגדר לאפשר CORS מכל מקור (פיתוח).
בסביבת ייצור, הגדר CORS מוגבל.

## שיפורים עתידיים
- [ ] הוספת Authentication & Authorization
- [ ] Validation עם FluentValidation
- [ ] Unit Tests & Integration Tests
- [ ] Caching layer (Redis/Memory)
- [ ] Rate Limiting
- [ ] Health Checks
- [ ] Docker support

## תמיכה
לשאלות או בעיות, פתח issue בGitHub.

---
**Last Updated**: November 11, 2025
**Version**: 1.0.0
