# 🛡️ Insecure Design – Awareness Demo (OWASP Top 10)

Dies ist eine einfache ASP.NET Core MVC-Anwendung zur Demonstration und Behebung der Schwachstelle **„Insecure Design“**, wie sie in den [OWASP Top 10](https://owasp.org/Top10/) gelistet ist.

---

## 🔍 Schwachstelle: Insecure Design

**Insecure Design** beschreibt Architekturfehler, bei denen grundlegende Sicherheitskonzepte wie Autorisierung, Rollenprüfung oder Zugriffsbeschränkungen fehlen – oft weil sie **nicht ins Design eingeplant** wurden.

> Diese Art von Schwachstelle entsteht **nicht durch technische Bugs**, sondern durch fehlende Sicherheitsüberlegungen bei der Planung.

---

## 🧪 Demo-Inhalte – UNSICHERE Version (`PROBLEM`)

### 1. 🔓 Profileinsicht ohne Zugriffskontrolle
- Jeder Benutzer kann `/profile/{id}` aufrufen (z. B. `/profile/2`)
- Es wird **nicht geprüft**, ob der Benutzer sein eigenes Profil sieht

### 2. 🚨 Rollenänderung ohne Berechtigung
- Jeder kann über `POST /admin/promote-user/{id}` Benutzer zu Admins machen
- Es gibt **keine Rollen- oder Authentifizierungsprüfung**

---

## ✅ FIXED – Sichere Variante (`FIXED`)

### 🔐 Lösung 1: Zugriffskontrolle für Profile
```csharp
var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
var profile = _context.Users.FirstOrDefault(u => u.Id == id && u.Id.ToString() == userId);
if (profile == null)
{
    return Forbid();
}
```

### 🔐 Lösung 2: Rollenprüfung bei Admin-Methoden
```csharp
if (!User.IsInRole("Admin"))
{
    return Forbid();
}
```
