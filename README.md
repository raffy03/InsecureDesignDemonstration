# 🛡️ Insecure Design – Awareness Demo (OWASP Top 10)

Dies ist eine einfache ASP.NET Core MVC-Anwendung zur Demonstration der Schwachstelle **„Insecure Design“**, basierend auf den OWASP Top 10.

## 🔍 Schwachstelle: Insecure Design

**Insecure Design** bezeichnet ein fehlerhaftes Systemdesign, das grundlegende Sicherheitsmechanismen ignoriert oder gar nicht vorsieht. Dadurch entstehen Schwachstellen, die sich **nicht durch einfache Input-Validierung oder Patches beheben lassen**, sondern ein **Neudenken der Architektur** erfordern.

> 👉 Sicherheitsprobleme durch Designfehler sind oft schwer zu erkennen – aber gefährlich in der Praxis.

---

## 🧪 Demo-Inhalte

Die Applikation demonstriert **zwei klassische Designfehler**:

### 1. 🔓 Profileinsicht ohne Zugriffskontrolle
- Jeder Benutzer kann auf beliebige Profile zugreifen (`/profile/1`, `/profile/2`)
- Es wird **nicht geprüft**, ob das aufgerufene Profil zum eingeloggten Benutzer gehört

### 2. 🚨 Rollenänderung ohne Berechtigung
- Jeder kann via `POST /admin/promote-user/{id}` Benutzer zu Admins befördern
- Kein Check auf Benutzerrolle oder Berechtigung → Missbrauchsgefahr

---

## 🚀 Projekt starten

### Voraussetzungen
- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- Visual Studio oder Visual Studio Code

### Ausführen
```bash
dotnet restore
dotnet run
