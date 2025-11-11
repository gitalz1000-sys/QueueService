# QueueService - מערכת ניהול תורים

מערכת מקצועית לניהול תורים (appointments) המיושמת ב-.NET Core Web API עם MongoDB.

## 🚀 התחלה מהירה

### הרצת הפרויקט ב-Replit
הפרויקט מוגדר להריץ אוטומטית. פשוט לחץ על **Run** והמערכת תתחיל לעבוד.

שני workflows רצים במקביל:
1. **MongoDB** - שרת מסד הנתונים המקומי
2. **API** - שרת ה-Web API על פורט 5000

### גישה ל-Swagger Documentation
פתח את הדפדפן והיכנס לכתובת:
```
https://[your-repl-url].repl.co
```
תראה ממשק Swagger אינטראקטיבי עם כל ה-API endpoints.

## 📋 API Endpoints

| שיטה | נתיב | תיאור |
|------|------|-------|
| GET | `/api/appointments` | קבלת כל התורים |
| GET | `/api/appointments/{id}` | קבלת תור ספציפי |
| POST | `/api/appointments` | יצירת תור חדש |
| PUT | `/api/appointments/{id}` | עדכון תור קיים |
| DELETE | `/api/appointments/{id}` | מחיקת תור |

## 🏗️ ארכיטקטורה

### תבנית CQRS (Command Query Responsibility Segregation)
המערכת מפרידה בין פעולות כתיבה (Commands) לפעולות קריאה (Queries):

- **Commands**: יצירה, עדכון, מחיקה
- **Queries**: קבלת נתונים
- **Handlers**: מבצעים את הלוגיקה העסקית

### עקרונות SOLID
הקוד נכתב בהתאם לעקרונות SOLID לקוד נקי ותחזוקתי:
- ✅ Single Responsibility - כל מחלקה עושה דבר אחד
- ✅ Open/Closed - קל להרחבה ללא שינוי
- ✅ Liskov Substitution - שימוש ב-Interfaces
- ✅ Interface Segregation - ממשקים ממוקדים
- ✅ Dependency Inversion - תלות ב-Abstractions

## 🛠️ טכנולוגיות

- **.NET 8.0** - Framework עדכני ויציב
- **MongoDB 7.0** - בסיס נתונים NoSQL
- **MediatR** - יישום CQRS pattern
- **Swagger/OpenAPI** - תיעוד אוטומטי ואינטראקטיבי
- **ASP.NET Core** - Web API framework

## 📁 מבנה הפרויקט

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

## 💡 דוגמאות שימוש

### יצירת תור חדש
```bash
POST /api/appointments
{
  "customerName": "יוסי כהן",
  "phoneNumber": "050-1234567",
  "appointmentDate": "2025-11-15T10:00:00",
  "serviceType": "תספורת",
  "notes": "מועדף בבוקר"
}
```

### עדכון תור
```bash
PUT /api/appointments/{id}
{
  "customerName": "יוסי כהן",
  "phoneNumber": "050-1234567",
  "appointmentDate": "2025-11-15T14:00:00",
  "serviceType": "תספורת",
  "status": "Confirmed",
  "notes": "שונה לשעה 14:00"
}
```

## 🔧 הרצה מקומית

```bash
# הפעל MongoDB
./start-mongo.sh

# הפעל את ה-API (בטרמינל אחר)
dotnet run
```

## 📚 תיעוד מפורט

לתיעוד מפורט, ראה [replit.md](./replit.md)

## 🔐 אבטחה

⚠️ **הערה חשובה**: המערכת הנוכחית מוגדרת לפיתוח.
לפני העברה לייצור (production), יש להוסיף:
- Authentication & Authorization
- Rate Limiting
- Input Validation
- CORS מוגבל לדומיינים ספציפיים

## 📝 רישיון

פרויקט חינמי לשימוש חינוכי ומסחרי.

---
**גרסה**: 1.0.0  
**תאריך**: נובמבר 2025
