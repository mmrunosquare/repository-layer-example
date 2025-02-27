# repository-layer-example
An example of how a new layer in our proyect would make our lives better

# 🚀 **How a Repository Layer Makes Our Lives Better**

Using a **Repository Layer** in our application architecture provides **separation of concerns**, improves **maintainability**, and makes **unit testing easier**. Here’s how it makes our lives better:

---

## 🔹 **1️⃣ Separation of Concerns**
### 🔴 **Without Repository Layer:**
The business logic (`Service Layer`) is tightly coupled to the database (`DbContext`).  

```csharp
public class UserService
{
    private readonly AppDbContext _context;
    
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
```
**Problem:**  
- The business logic directly depends on the database.
- If we change the database technology (e.g., switch from Entity Framework to Dapper), we need to **modify every service** that interacts with `DbContext`.

### ✅ **With Repository Layer:**
```csharp
public interface IUserRepository
{
    User GetUser(int id);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }
}
```
✅ **Benefit:**  
- The **business layer (`UserService`) is now independent** of the database.
- If we change the database implementation, we only modify `UserRepository`, **not the business logic**.

---

## 🔹 **2️⃣ Easier Unit Testing**
### 🔴 **Without Repository Layer:**
Testing `UserService` is difficult because it requires a **real database** or an in-memory `DbContext` instance.

### ✅ **With Repository Layer (Mocking)**
By injecting `IUserRepository`, we can **mock the database** using `Moq`:

```csharp
var mockRepo = new Mock<IUserRepository>();
mockRepo.Setup(repo => repo.GetUser(1)).Returns(new User { Id = 1, Name = "Byron" });

var service = new UserService(mockRepo.Object);
var user = service.GetUser(1);

Assert.Equal("Byron", user.Name);
```

✅ **Benefit:**  
- No need to connect to a **real database** for unit tests.  
- Faster test execution.  
- We can **simulate different database behaviors** without modifying the actual repository.

---

## 🔹 **3️⃣ Less Code Duplication (DRY - Don't Repeat Yourself)**
### 🔴 **Without Repository Layer:**
If every service directly interacts with `DbContext`, the query logic **gets repeated** across different services.

```csharp
public class UserService
{
    private readonly AppDbContext _context;
    
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}

public class OrderService
{
    private readonly AppDbContext _context;
    
    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public Order GetOrder(int id)
    {
        return _context.Orders.FirstOrDefault(o => o.Id == id);
    }
}
```

### ✅ **With Repository Layer:**
Now the **database access logic is centralized**, avoiding redundancy.

```csharp
public interface IUserRepository
{
    User GetUser(int id);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
```

Now, **both services can reuse the same repository** without repeating code.

---

## 🔹 **4️⃣ Flexible Database Implementation**
### 🔴 **Without Repository Layer:**
If we want to switch from **Entity Framework to Dapper**, we need to **rewrite the entire business layer**.

### ✅ **With Repository Layer:**
We **only change the repository implementation** and keep the business logic intact.

- **Entity Framework Implementation:**
```csharp
public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public EfUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
```

- **Dapper Implementation:**
```csharp
public class DapperUserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public DapperUserRepository(IDbConnection db)
    {
        _db = db;
    }

    public User GetUser(int id)
    {
        return _db.Query<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id }).FirstOrDefault();
    }
}
```
✅ **Benefit:**  
- Easily switch **between different ORMs or databases**.
- **Minimal impact** on business logic.

---

## 🔹 **5️⃣ Better Maintainability and Scalability**
- If a **new requirement** needs additional filtering for users (e.g., `GetActiveUsers()`), we only modify the repository, **not every service that queries users**.
- The repository can **handle caching** or **logging**, improving performance **without modifying business logic**.

---

## 🎯 **Conclusion: Why Repository Pattern Makes Life Easier**
| 🚀 Benefit | ✅ How It Helps |
|------------|----------------|
| **Separation of Concerns** | Keeps business logic independent from database logic. |
| **Easier Unit Testing** | Mocks repositories without requiring a real database. |
| **Avoids Code Duplication** | Centralizes database access logic in a single place. |
| **Flexible Database Implementation** | Easily switch between Entity Framework, Dapper, or MongoDB. |
| **Better Maintainability** | Changes in data access don't affect business logic. |

🚀 **Using the Repository Pattern improves maintainability, scalability, and testability!**  
Would you like to implement it in your project? 😃
